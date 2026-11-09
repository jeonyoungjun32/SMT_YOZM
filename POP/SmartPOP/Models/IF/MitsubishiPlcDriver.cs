using SmartPOP.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace SmartPOP.Models.IF
{
    public class MitsubishiPlcDriver : IPlcDriver
    {
        private TcpClient client;
        private NetworkStream stream;

        private Thread readThread;
        private Thread dbThread;

        private bool isRunning = false;
        private bool isConnected = false;

        private ConcurrentDictionary<string, int> latestData = new ConcurrentDictionary<string, int>();
        private ConcurrentDictionary<string, int> lastSavedData = new ConcurrentDictionary<string, int>();

        private Dictionary<string, string> addressToDgp = new Dictionary<string, string>();
        private List<string> addressList;

        private IF_DgpCode mtDGPCode = new IF_DgpCode();

        private string ip;
        private int port;

        // 🔥 동기화
        private object streamLock = new object();

        // 🔥 Write 상태 (LS 스타일)
        private bool isWriteBusy = false;
        private DateTime writeStartTime = DateTime.MinValue;
        private DateTime lastWriteTime = DateTime.MinValue;

        private List<string> pendingResetList = new List<string>();
        private object writeLock = new object();

        // 이력저장
        private DateTime lastHistorySaveTime = DateTime.Now;

        #region Start / Stop
        public void Start(string ip, int port, string ctCode)
        {
            this.ip = ip;
            this.port = port;

            mtDGPCode.CTCode = ctCode;

            LoadAddress();

            if (!TryConnect())
            {
                SMTFunction.Log("MIT", "초기 연결 실패 - Start 중단");
                return;
            }

            isRunning = true;

            readThread = new Thread(ReadLoop) { IsBackground = true };
            dbThread = new Thread(DbLoop) { IsBackground = true };

            readThread.Start();
            dbThread.Start();
        }

        public void Stop()
        {
            isRunning = false;

            Disconnect();

            readThread?.Join(1000);
            dbThread?.Join(1000);
        }
        #endregion

        #region Connect
        private bool TryConnect()
        {
            try
            {
                Disconnect();

                client = new TcpClient();
                client.ReceiveTimeout = 3000;
                client.SendTimeout = 3000;

                client.Connect(ip, port);
                stream = client.GetStream();

                isConnected = true;

                lastHistorySaveTime = DateTime.Now;

                SMTFunction.Log("MIT", "Connect 성공");
                return true;
            }
            catch (Exception ex)
            {
                isConnected = false;

                SMTFunction.Log("MIT", "Connect 실패 - " + ex.Message);
                return false;
            }
        }

        private void Disconnect()
        {
            try
            {
                stream?.Close();
                client?.Close();
            }
            catch { }
            finally
            {
                isConnected = false;

                stream = null;
                client = null;
            }
        }
        #endregion

        #region 주소
        private void LoadAddress()
        {
            //mtDGPCode.CTCode = "CT02";
            DataTable dt = mtDGPCode.Select_DGPCode();

            addressList = new List<string>();
            addressToDgp.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                string addr = dr["MemoryCode"].ToString();
                addressList.Add(addr);
                addressToDgp[addr] = dr["DgpCode"].ToString();
            }
        }
        #endregion

        #region Read Loop
        private void ReadLoop()
        {
            while (isRunning)
            {
                try
                {
                    if (client == null || !client.Connected || stream == null)
                    {
                        if (!TryConnect())
                        {
                            Thread.Sleep(2000);
                            continue;
                        }
                    }

                    foreach (var addr in addressList)
                    {
                        int value = ReadMitsubishi(addr);

                        if (addressToDgp.TryGetValue(addr, out string dgp))
                        {
                            latestData[dgp] = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SMTFunction.Log("MIT", "ReadLoop - " + ex.Message);
                    Disconnect();
                    Thread.Sleep(1000);
                }

                Thread.Sleep(50);
            }
        }
        #endregion

        #region Read
        private int ReadMitsubishi(string addr)
        {
            if (stream == null) return 0;

            try
            {
                int address = int.Parse(addr);

                byte[] cmd =
                {
                    0x50,0x00,
                    0x00,0xFF,
                    0xFF,0x03,
                    0x00,
                    0x0C,0x00,
                    0x10,0x00,
                    0x01,0x04,
                    0x00,0x00,
                    (byte)(address & 0xFF),
                    (byte)((address>>8)&0xFF),
                    (byte)((address>>16)&0xFF),
                    0xA8,
                    0x01,0x00
                };

                lock (streamLock)
                {
                    stream.Write(cmd, 0, cmd.Length);

                    byte[] buffer = new byte[256];
                    int len = stream.Read(buffer, 0, buffer.Length);

                    if (len < 13) return 0;

                    return buffer[11] + buffer[12] * 256;
                }
            }
            catch (Exception ex)
            {
                isConnected = false;

                SMTFunction.Log("MIT", "Read 실패 - " + ex.Message);
                Disconnect();
                return 0;
            }
        }
        #endregion

        #region DB Loop + Write (LS 방식)
        private void DbLoop()
        {
            while (isRunning)
            {
                try
                {
                    var snapshot = latestData.ToArray();
                    List<PlcData> changed = new List<PlcData>();

                    foreach (var item in snapshot)
                    {
                        if (!lastSavedData.TryGetValue(item.Key, out int old) || old != item.Value)
                        {
                            changed.Add(new PlcData
                            {
                                DgpCode = item.Key,
                                Value = item.Value
                            });

                            lastSavedData[item.Key] = item.Value;
                        }
                    }

                    if (changed.Count > 0)
                        SaveBatch(changed);

                    if ((DateTime.Now - lastHistorySaveTime).TotalMinutes >= 5)
                    {
                        SaveHistory(snapshot);

                        lastHistorySaveTime = DateTime.Now;
                    }

                    // 🔥 Write Timeout
                    lock (writeLock)
                    {
                        if (isWriteBusy && (DateTime.Now - writeStartTime).TotalSeconds > 2)
                        {
                            SMTFunction.Log("MIT", "Write Timeout");
                            ResetWrite();
                        }
                    }

                    // 🔥 Write 실행
                    lock (writeLock)
                    {
                        if (!isWriteBusy && (DateTime.Now - lastWriteTime).TotalSeconds > 2)
                        {
                            ProcessResetWrite();
                            lastWriteTime = DateTime.Now;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SMTFunction.Log("MIT", "DbLoop - " + ex.Message);
                }

                Thread.Sleep(100);
            }
        }
        #endregion

        #region Write (LS 스타일)
        private void ProcessResetWrite()
        {
            try
            {
                if (stream == null) return;

                IF_DgpStatus status = new IF_DgpStatus();
                var dt = status.Select_IF_DGPStatus(mtDGPCode.CTCode);

                var writeList = new List<(string addr, int value)>();
                pendingResetList.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    if (row["CounterReset"].ToString() == "ON")
                    {
                        string dgp = row["DgpCode"].ToString();
                        string addr = row["Write_Address"].ToString();

                        writeList.Add((addr, 10));
                        pendingResetList.Add(dgp);
                    }
                }

                if (writeList.Count == 0)
                    return;

                lock (streamLock)
                {
                    foreach (var item in writeList)
                    {
                        WriteMitsubishiRaw(item.addr, item.value);
                    }
                }

                // 🔥 Write 성공 → DB OFF
                foreach (var dgp in pendingResetList)
                {
                    IF_DgpStatus s = new IF_DgpStatus();
                    s.DgpCode = dgp;
                    s.CounterReset = "OFF";
                    s.UpdateCounterReset();
                }

                isWriteBusy = true;
                writeStartTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("MIT", "ProcessResetWrite - " + ex.Message);
            }
        }

        private void WriteMitsubishiRaw(string addr, int value)
        {
            int address = int.Parse(addr);

            byte[] cmd =
            {
                0x50,0x00,
                0x00,0xFF,
                0xFF,0x03,
                0x00,
                0x0E,0x00,
                0x10,0x00,
                0x01,0x14,
                0x00,0x00,
                (byte)(address & 0xFF),
                (byte)((address>>8)&0xFF),
                (byte)((address>>16)&0xFF),
                0xA8,
                0x01,0x00,
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF)
            };

            stream.Write(cmd, 0, cmd.Length);

            byte[] buffer = new byte[256];
            int len = stream.Read(buffer, 0, buffer.Length);

            if (len < 11)
                throw new Exception("Write 응답 오류");
        }

        private void ResetWrite()
        {
            lock (writeLock)
            {
                isWriteBusy = false;
                pendingResetList.Clear();
            }
        }
        #endregion

        private void SaveBatch(List<PlcData> list)
        {
            foreach (var item in list)
            {
                IF_DgpCounter c = new IF_DgpCounter
                {
                    DgpCode = item.DgpCode,
                    Counter = item.Value
                };

                c.Update_DGPCounter();
            }
        }

        private void SaveHistory(KeyValuePair<string, int>[] snapshot)
        {
            try
            {
                if (!isConnected)
                {
                    return;
                }

                foreach (var item in snapshot)
                {
                    try
                    {
                        IF_DgpHistory history = new IF_DgpHistory();

                        history.DgpCode = item.Key;
                        history.Gathering_Count = item.Value;
                        history.Gathering_Qty = item.Value;
                        history.DgpTime = DateTime.Now.ToString("yyyyMMdd HH:mm");

                        history.Insert_DgpHistory();
                    }
                    catch (Exception ex)
                    {
                        SMTFunction.Log("MIT", "SaveHistory Item - " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("MIT", "SaveHistory - " + ex.Message);
            }
        }
    }
}