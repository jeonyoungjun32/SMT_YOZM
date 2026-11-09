using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SmartPOP.Models.IF
{
    public delegate void SocketReceivedHandler(byte[] packet, int offset, int length);

    public class SMTSocket
    {
        #region Field 
        protected byte[] sockBuffer = new byte[1024];

        protected bool _isDisposed = false;

        public struct PACKET_BUFFER
        {
            public byte[] packet;
            public int offset;
            public int length;
        }

        public PACKET_BUFFER recvBuf = new PACKET_BUFFER();
        protected PACKET_BUFFER sendBuf = new PACKET_BUFFER();

        public Socket mSocket;
        protected string svrIP = "";
        protected int svrPort = 0;
        public bool mConnect = false;
        public bool mClose = false;

        public Thread t_Process = null;
        #endregion

        #region Constructor
        public SMTSocket(string getIP, int getPort)
        {
            recvBuf.packet = new byte[1024];
            sendBuf.packet = new byte[1024];

            svrIP = getIP;
            svrPort = getPort;
        }
        #endregion

        #region Init
        public void Init()
        {
            if (mClose) return;

            recvBuf.length = 0;
            recvBuf.packet.Initialize();

            sendBuf.length = 0;
            sendBuf.packet.Initialize();

            mConnect = false;

            try { mSocket?.Dispose(); } catch { }

            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        #endregion

        #region Connect
        public void Connect()
        {
            if (mClose) return;

            try
            {
                var ep = new IPEndPoint(IPAddress.Parse(svrIP), svrPort);
                var args = new SocketAsyncEventArgs();
                args.RemoteEndPoint = ep;
                args.Completed += Connect_Completed;

                mSocket.ConnectAsync(args);
            }
            catch (SocketException se)
            {
                mConnect = false;
                SMTFunction.Log("Connect - " + se.Message);
            }
        }
        #endregion

        #region Connect_Completed
        protected virtual void Connect_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                if (_isDisposed) return;
                if (mSocket == null || !mSocket.Connected) return;
                if (sender != mSocket) return;

                var args = new SocketAsyncEventArgs();
                args.SetBuffer(sockBuffer, 0, 1024);
                args.Completed += Receive_Completed;

                try { mSocket.ReceiveAsync(args); }
                catch (ObjectDisposedException) { return; }

                mConnect = true;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("Connect_Completed - " + ex.Message);
                Init();
            }
        }
        #endregion

        #region Close
        public void Close()
        {
            try
            {
                _isDisposed = true;

                if (mSocket == null) return;

                try { mSocket.Shutdown(SocketShutdown.Both); } catch { }
                try { mSocket.Close(); } catch { }
                try { mSocket.Dispose(); } catch { }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("Close - " + ex.Message);
            }
        }
        #endregion

        #region Receive
        protected virtual void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                if (_isDisposed) return;
                if (mSocket == null || !mSocket.Connected) return;
                if (sender != mSocket) return;

                if (e.BytesTransferred > 0)
                {
                    recvBuf.length = e.BytesTransferred;
                    recvBuf.offset = e.Offset;
                    Array.Copy(e.Buffer, e.Offset, recvBuf.packet, 0, e.BytesTransferred);
                }
                else
                {
                    Init();
                    return;
                }

                try { mSocket.ReceiveAsync(e); }
                catch (ObjectDisposedException) { return; }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("Receive_Completed - " + ex.Message);
                Init();
            }
        }
        #endregion

        #region Send
        public void procSendPacket(byte[] data)
        {
            try
            {
                if (mSocket == null || !mSocket.Connected) return;

                var args = new SocketAsyncEventArgs();
                args.SetBuffer(data, 0, data.Length);
                args.Completed += (s, e) => e.Dispose();

                mSocket.SendAsync(args);
            }
            catch (Exception ex)
            {
                SMTFunction.Log("Send - " + ex.Message);
                Init();
            }
        }
        #endregion
    }

    // ================= READ =================
    public class LSReadSocket : SMTSocket
    {
        public event SocketReceivedHandler DataReceived;

        public LSReadSocket(string ip, int port) : base(ip, port) { }

        protected override void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                if (_isDisposed) return;
                if (mSocket == null || !mSocket.Connected) return;
                if (sender != mSocket) return;

                if (e.BytesTransferred > 0)
                {
                    DataReceived?.Invoke(e.Buffer, e.Offset, e.BytesTransferred);
                }
                else
                {
                    Init();
                    return;
                }

                try { mSocket.ReceiveAsync(e); }
                catch (ObjectDisposedException) { return; }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("LSRead Receive - " + ex.Message);
                Init();
            }
        }
    }

    // ================= WRITE =================
    public class LSWriteSocket : SMTSocket
    {
        public event SocketReceivedHandler DataReceived;

        public LSWriteSocket(string ip, int port) : base(ip, port) { }

        protected override void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                if (_isDisposed) return;
                if (mSocket == null || !mSocket.Connected) return;
                if (sender != mSocket) return;

                if (e.BytesTransferred > 0)
                {
                    DataReceived?.Invoke(e.Buffer, e.Offset, e.BytesTransferred);
                }
                else
                {
                    Init();
                    return;
                }

                try { mSocket.ReceiveAsync(e); }
                catch (ObjectDisposedException) { return; }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("LSWrite Receive - " + ex.Message);
                Init();
            }
        }
    }
}