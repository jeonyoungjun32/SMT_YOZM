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
    public class LSPlcDriver : IPlcDriver
    {
        private LSPLCVariable lsVariable1 = new LSPLCVariable();
        private IF_DgpCode mtDGPCode = new IF_DgpCode();

        private Thread readThread;
        private Thread dbThread;

        private bool isRunning = false;

        // 🔥 LOCK
        private readonly object readLock = new object();
        private readonly object writeLock = new object();

        private bool isReading = false;
        private bool isWriteBusy = false;

        private DateTime lastSendTime = DateTime.MinValue;
        private DateTime writeStartTime = DateTime.MinValue;
        private DateTime lastWriteTime = DateTime.MinValue;
        private DateTime lastReconnectAttempt = DateTime.MinValue;

        private List<string> lastRequestAddresses = null;

        private ConcurrentDictionary<string, int> latestData = new ConcurrentDictionary<string, int>();
        private ConcurrentDictionary<string, int> lastSavedData = new ConcurrentDictionary<string, int>();

        private List<List<string>> addressGroups;
        private Dictionary<string, string> addressToDgp = new Dictionary<string, string>();
        private int currentGroupIndex = 0;

        private List<string> pendingResetList = new List<string>();

        // 이력저장
        private DateTime lastHistorySaveTime = DateTime.Now;

        #region Start / Stop
        public void Start(string ip, int port, string ctCode)
        {
            mtDGPCode.CTCode = ctCode;

            LoadAddress();

            lsVariable1.socketRead = new LSReadSocket(ip, port);
            lsVariable1.socketWrite = new LSWriteSocket(ip, port);

            lsVariable1.socketRead.DataReceived += OnReadReceive;
            lsVariable1.socketWrite.DataReceived += OnWriteReceive;

            lsVariable1.socketRead.Init();
            lsVariable1.socketWrite.Init();

            lsVariable1.socketRead.Connect();
            lsVariable1.socketWrite.Connect();

            isRunning = true;

            readThread = new Thread(ReadLoop) { IsBackground = true };
            dbThread = new Thread(DbLoop) { IsBackground = true };

            readThread.Start();
            dbThread.Start();
        }

        public void Stop()
        {
            isRunning = false;

            readThread?.Join(500);
            dbThread?.Join(500);

            try { lsVariable1.socketRead?.Close(); } catch { }
            try { lsVariable1.socketWrite?.Close(); } catch { }
        }

        private void TryReconnect()
        {
            try
            {
                // 너무 자주 재연결 방지
                if ((DateTime.Now - lastReconnectAttempt).TotalSeconds < 3)
                    return;

                SMTFunction.Log("PLC", "재연결 시도");

                // 🔥 기존 소켓 정리
                try { lsVariable1.socketRead?.mSocket?.Shutdown(SocketShutdown.Both); } catch { }
                try { lsVariable1.socketRead?.mSocket?.Close(); } catch { }

                try { lsVariable1.socketWrite?.mSocket?.Shutdown(SocketShutdown.Both); } catch { }
                try { lsVariable1.socketWrite?.mSocket?.Close(); } catch { }

                // 🔥 재초기화
                lsVariable1.socketRead.Init();
                lsVariable1.socketWrite.Init();

                // 🔥 이벤트 재등록 (이거 핵심)
                lsVariable1.socketRead.DataReceived -= OnReadReceive;
                lsVariable1.socketRead.DataReceived += OnReadReceive;

                lsVariable1.socketWrite.DataReceived -= OnWriteReceive;
                lsVariable1.socketWrite.DataReceived += OnWriteReceive;

                // 🔥 재연결
                lsVariable1.socketRead.Connect();
                lsVariable1.socketWrite.Connect();

                lastReconnectAttempt = DateTime.Now;

                SMTFunction.Log("PLC", "재연결 성공");
            }
            catch (Exception ex)
            {
                SMTFunction.Log("PLC", "재연결 실패 - " + ex.Message);
            }
        }
        #endregion

        #region 주소
        private void LoadAddress()
        {
            //mtDGPCode.CTCode = "CT01";
            DataTable dt = mtDGPCode.Select_DGPCode();

            var list = new List<string>();
            addressToDgp.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                string addr = "%DD" + Convert.ToInt32(dr["MemoryCode"]) / 2;
                list.Add(addr);
                addressToDgp[addr] = dr["DgpCode"].ToString();
            }

            addressGroups = Split(list, 16);
        }

        private List<List<string>> Split(List<string> src, int size)
        {
            return src
                .Select((x, i) => new { x, i })
                .GroupBy(x => x.i / size)
                .Select(g => g.Select(v => v.x).ToList())
                .ToList();
        }
        #endregion

        #region Read Loop (기존 방식 + lock)
        private void ReadLoop()
        {
            while (isRunning)
            {
                try
                {
                    if (lsVariable1.socketRead?.mSocket == null ||
                        !lsVariable1.socketRead.mSocket.Connected)
                    {
                        TryReconnect();
                        Thread.Sleep(100);
                        continue;
                    }

                    lock (readLock)
                    {
                        if (isReading)
                        {
                            if ((DateTime.Now - lastSendTime).TotalMilliseconds > 1000)
                            {
                                SMTFunction.Log("PLC", "Read Timeout");
                                ResetRead();
                            }
                            continue;
                        }

                        if (addressGroups == null || addressGroups.Count == 0)
                            continue;

                        var group = addressGroups[currentGroupIndex];
                        lastRequestAddresses = group;

                        byte[] send = lsVariable1.LS_Read.SendRead_Each(
                            0,
                            SelectTYPE.Dword,
                            group.ToArray()
                        );

                        lsVariable1.socketRead.procSendPacket(send);

                        isReading = true;
                        lastSendTime = DateTime.Now;

                        currentGroupIndex++;
                        if (currentGroupIndex >= addressGroups.Count)
                            currentGroupIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    SMTFunction.Log("PLC", "ReadLoop - " + ex.Message);
                }

                Thread.Sleep(10);
            }
        }
        #endregion

        #region Read Receive (기존 유지 + lock)
        private void OnReadReceive(byte[] packet, int offset, int length)
        {
            try
            {
                if (!lsVariable1.socketRead.mSocket.Connected)
                {
                    ResetRead();
                    return;
                }

                var table = lsVariable1.LS_Read.procReceiveData(packet);

                if (table["ErrorState"] != "0000")
                {
                    ResetRead();
                    return;
                }

                int count = Convert.ToInt32(
                    SMTFunction.ReverseToSwap(table["Device_Count"]), 16);

                lock (readLock)
                {
                    if (lastRequestAddresses == null)
                    {
                        ResetRead();
                        return;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        int value = Convert.ToInt32(
                            SMTFunction.ReverseToSwap(table[$"Device_{i + 1}"]), 16);

                        string addr = lastRequestAddresses[i];
                        string dgp = addressToDgp[addr];

                        latestData[dgp] = value;
                    }

                    ResetRead();
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("PLC", "OnReadReceive - " + ex.Message);
                ResetRead();
            }
        }

        private void ResetRead()
        {
            lock (readLock)
            {
                isReading = false;
                lastRequestAddresses = null;
            }
        }
        #endregion

        #region DB Loop
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
                            SMTFunction.Log("PLC", "Write Timeout");
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
                    SMTFunction.Log("PLC", "DbLoop - " + ex.Message);
                }

                Thread.Sleep(100);
            }
        }
        #endregion

        #region Write (구버전 안정 방식)
        private void ProcessResetWrite()
        {
            try
            {
                if (lsVariable1.socketWrite?.mSocket == null || !lsVariable1.socketWrite.mSocket.Connected)
                {
                    TryReconnect();
                    return;
                }

                IF_DgpStatus status = new IF_DgpStatus();
                var dt = status.Select_IF_DGPStatus(mtDGPCode.CTCode);

                var writeAddresses = new List<string>();
                var writeValues = new List<string>();
                pendingResetList.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    if (row["CounterReset"].ToString() == "ON")
                    {
                        string dgp = row["DgpCode"].ToString();
                        int memory = Convert.ToInt32(row["Write_Address"]);

                        writeAddresses.Add("%DW" + memory);
                        writeValues.Add("10");
                        pendingResetList.Add(dgp);
                    }
                }

                if (writeAddresses.Count == 0)
                    return;

                byte[] sendData = lsVariable1.LS_Write.SendWrite_Each(
                    0,
                    SelectTYPE.Word,
                    writeAddresses.ToArray(),
                    writeValues.ToArray()
                );

                lsVariable1.socketWrite.procSendPacket(sendData);

                isWriteBusy = true;
                writeStartTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("PLC", "ProcessResetWrite - " + ex.Message);
            }
        }

        private void OnWriteReceive(byte[] packet, int offset, int length)
        {
            try
            {
                if (!lsVariable1.socketWrite.mSocket.Connected)
                {
                    ResetWrite();
                    return;
                }

                if (lsVariable1.LS_Write.procWriteBuffer(packet, offset, length))
                {
                    var getTable = lsVariable1.LS_Write.procReceiveData(lsVariable1.LS_Write.packet_Buff);

                    if (getTable["ErrorState"] == "0000")
                    {
                        foreach (var dgp in pendingResetList)
                        {
                            IF_DgpStatus status = new IF_DgpStatus();
                            status.DgpCode = dgp;
                            status.CounterReset = "OFF";
                            status.UpdateCounterReset();
                        }
                    }

                    ResetWrite();
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("PLC", "OnWriteReceive - " + ex.Message);
                ResetWrite();
            }
        }

        private void ResetWrite()
        {
            lock (writeLock)
            {
                if (lsVariable1.LS_Write.packet_Buff != null)
                {
                    Array.Clear(lsVariable1.LS_Write.packet_Buff, 0, lsVariable1.LS_Write.packet_Buff.Length);
                }

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
                if (snapshot == null || snapshot.Length == 0)
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
                        SMTFunction.Log("PLC", "SaveHistory Item - " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("PLC", "SaveHistory - " + ex.Message);
            }
        }
    }
}