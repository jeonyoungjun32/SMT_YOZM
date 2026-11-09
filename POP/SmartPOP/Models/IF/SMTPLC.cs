using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    /// <summary>
    /// 지시 : 명령어 선택
    /// </summary>
    public enum SelectCMD
    {
        Read = 1,
        Write = 2,
    }

    /// <summary>
    /// 데이터 타입
    /// </summary>
    public enum SelectTYPE
    {
        Bit = 1,
        Byte = 2,
        Word = 3,
        Dword = 4,
        Lword = 5
    }

    public class LS_XGT
    {
        #region Field 
        // Header
        private readonly byte[] COMPANY_ID = new byte[] { 0x4C, 0x53, 0x49, 0x53, 0x2D, 0x58, 0x47, 0x54 };  // 회사 아이디
        private readonly byte[] RESERVED_H = new byte[] { 0x00, 0x00 };                                      // 예약영역 
        private readonly byte[] PLC_INFO = new byte[] { 0x00, 0x00 };                                        // PLC 정보 
        private readonly byte CPU_INFO = 0xA0;                                                               // CPU 정보 
        private readonly byte SOURCE_OF_FRAME = 0x33;                                                        // 프레임 
        private readonly byte INVOKE_ID = 0x00;                                                              // 인보크 ID 
        private readonly byte LENGTH = 0x00;                                                                 // 프레임 전체 길이 
        private readonly byte MODULE_POSITION = 0x00;                                                        // 모듈 위치 
        private readonly byte BCC = 0x00;                                                                    // BCC - 기본 0x00으로 해도 통신 가능

        // Frame
        private readonly byte[] COMMAND = new byte[] { 0x00, 0x54, 0x58 };                                   // 명령어 일람 
        private readonly byte[] DATA_TYPE = new byte[]                                                       // 데이터 타입 
        {
            0x00, 0x00, 0x01, 0x02,
            0x03, 0x04, 0x14
        };
        private readonly byte[] RESERVED_F = new byte[] { 0x00, 0x00 };                                     // 예약영역 
        private readonly byte[] DEVICE_COUNT = new byte[]                                                   // 변수 개수      
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18,0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28,0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38,0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48,0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F
        };
        private readonly byte[] DEVICE_LENGTH = new byte[]                                                  // 변수 길이 
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18,0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28,0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38,0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48,0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F
        };
        private readonly byte[] VALUE_LENGTH = new byte[]                                                  // 데이터 길이
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18,0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28,0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38,0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48,0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F
        };

        // 패킷 헤더 사이즈 (Length 까지의 길이)
        public const int COMPANY_ID_SIZE = 8;
        public const int RESERVED_H_SIZE = 2;
        public const int PLC_INFO_SIZE = 2;
        public const int CPU_INFO_SIZE = 1;
        public const int SOURCE_OF_FRAME_SIZE = 1;
        public const int INVOKE_ID_SIZE = 2;
        public const int LENGTH_SIZE = 2;
        public const int MODULE_POSITION_SIZE = 1;
        public const int BCC_SIZE = 1;

        public const int COMMAND_SIZE = 2;
        public const int DATA_TYPE_SIZE = 2;
        public const int RESERVED_F_SIZE = 2;
        public const int DEVICE_COUNT_SIZE = 2;
        public const int DEVICE_LENGTH_SIZE = 2;
        public const int VALUE_COUNT_SIZE = 2;
        public const int VALUE_LENGTH_SIZE = 2;
        public const int ERROR_STATE_SIZE = 2;
        public const int ERROR_INFO_SIZE = 2;
        public static int[] SEND_DEVICE_SIZE = new int[16];
        public static int[] VALUE_SIZE = new int[16];
        public static int[] RECEIVE_DEVICE_SIZE = new int[16];

        // LS산전 - 고정된 헤더 사이즈
        private readonly int HEADER_SIZE = 20;

        // Processing Write Buffer 
        public byte[] packet_Buff;                   // 전역 버퍼
        private byte[] mBuffer = new byte[2048];     // 버퍼. 
        private int cPos = 0;                        // 타켓 위치.  
        private int posRead = 0;                     // 목표 위치. 
        private int mSize = 0;                       // 버퍼 사이즈. 
        private int reBytes;                         // 남은 바이트. 
        #endregion

        #region Constructor
        public LS_XGT()
        {
            packet_Buff = null;
        }
        #endregion

        #region Receive Method
        /// <summary>
        /// 비동기 식일때 팩킷길이 만큼 데이터 다들어왔는지 확인  
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool procWriteBuffer(byte[] packet, int offset, int transffered)
        {
            bool result = false;
            string subCompany_ID;

            reBytes = transffered;          // Receive로 읽어오게 될 바이트 수
            int src_position = offset;      // 원본 버퍼의 포지션값.                                 

            try
            {
                // 헤더 길이 체크 (Fixed 20 Byte)
                if (cPos < HEADER_SIZE)
                {
                    posRead = HEADER_SIZE;

                    // 헤더 사이즈 체크
                    if (!Read_Until(packet, ref src_position, offset, transffered))
                        return result;

                    // 헤더 완료
                    subCompany_ID = SMTFunction.ByteArrayToString(packet).Substring(0, COMPANY_ID_SIZE * 2);
                    if (SMTFunction.CharToString(subCompany_ID) != "LSIS-XGT")
                    {
                        SMTFunction.Log("procWriteBuffer - Header Size Error");
                        return result;
                    }
                    else
                    {
                        // 지시 사이즈 체크
                        mSize = procReInstructionSize(packet);
                        posRead = HEADER_SIZE + mSize;
                    }
                }
                // 전체 패킷들어 왔는지 검사 
                if (Read_Until(packet, ref src_position, offset, transffered))
                {
                    packet_Buff = new byte[src_position];

                    // Buffer Copy
                    Array.Copy(mBuffer, 0, packet_Buff, 0, packet_Buff.Length);
                    result = true;

                    // Clear
                    Array.Clear(mBuffer, 0, mBuffer.Length);
                    cPos = 0;
                    mSize = 0;

                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("procWriteBuffer - " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 목표지점으로 설정된 위치까지의 바이트를 원본 버퍼로부터 복사한다.  
        /// 데이터가 모자랄 경우 현재 남은 바이트 까지만 복사한다.  
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="src_position"></param>
        /// <param name="offset"></param>
        /// <param name="transffered"></param>
        /// <returns></returns>
        private bool Read_Until(byte[] packet, ref int src_position, int offset, int transffered)
        {
            // 들어온 데이터 만큼 다 읽은 상태이므로 더이상 읽을 데이터가 없다. 
            if (cPos >= offset + transffered)
                return false;

            // 패킷 사이즈
            int copy_size = posRead - cPos;

            // 남은 데이터가 더 적다면 가능한 만큼만 복사한다.  
            if (reBytes < copy_size)
                copy_size = reBytes;

            // 버퍼에 복사.  
            if (packet != null)
                Array.Copy(packet, src_position, mBuffer, cPos, copy_size);

            src_position += copy_size;    // 원본 버퍼 포지션 이동.  
            cPos += copy_size;            // 타겟 버퍼 포지션 이동.  
            reBytes -= copy_size;         // 남은 패킷 바이트 수.  

            // 목표지점에 도달 못했으면 false  
            if (cPos < posRead)
                return false;

            return true;
        }

        /// <summary>
        /// 헤더에서 지시 사이즈를 구한다.  
        /// </summary>
        /// <returns></returns>
        private int procReInstructionSize(byte[] getSize)
        {
            int result = 0;

            try
            {
                // 10진수 가져오기
                result = Buffer.GetByte(getSize, 16);
            }
            catch (Exception ex)
            {
                SMTFunction.Log("get_Instruction_Size - " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Receive Data Processing
        /// </summary>
        /// <param name="getData"></param>
        /// <param name="nbytes"></param>
        /// <returns></returns>
        public Dictionary<string, string> procReceiveData(byte[] getData)
        {
            var result = new Dictionary<string, string>();
            string hexData, strSwap, ErrorCheck;
            byte[] getBytes;
            string strCmd, strType;
            int getCount, getLength, nLength = 0;

            try
            {
                hexData = SMTFunction.ByteArrayToString(getData);

                #region 헤더
                result.Add("Company_ID", hexData.Substring(nLength, COMPANY_ID_SIZE * 2));
                nLength += COMPANY_ID_SIZE * 2;

                result.Add("Reserved_H", hexData.Substring(nLength, RESERVED_H_SIZE * 2));
                nLength += RESERVED_H_SIZE * 2;

                result.Add("PLC_Info", hexData.Substring(nLength, PLC_INFO_SIZE * 2));
                nLength += PLC_INFO_SIZE * 2;

                result.Add("CPU_Info", hexData.Substring(nLength, CPU_INFO_SIZE * 2));
                nLength += CPU_INFO_SIZE * 2;

                result.Add("Source_of_Frame", hexData.Substring(nLength, SOURCE_OF_FRAME_SIZE * 2));
                nLength += SOURCE_OF_FRAME_SIZE * 2;

                result.Add("Invoke_ID", hexData.Substring(nLength, INVOKE_ID_SIZE * 2));
                nLength += INVOKE_ID_SIZE * 2;

                result.Add("Lenth", hexData.Substring(nLength, LENGTH_SIZE * 2));
                nLength += LENGTH_SIZE * 2;

                result.Add("Position", hexData.Substring(nLength, MODULE_POSITION_SIZE * 2));
                nLength += MODULE_POSITION_SIZE * 2;

                result.Add("BCC", hexData.Substring(nLength, BCC_SIZE * 2));
                nLength += BCC_SIZE * 2;
                #endregion

                #region 지시
                strCmd = hexData.Substring(nLength, COMMAND_SIZE * 2);
                result.Add("Command", hexData.Substring(nLength, COMMAND_SIZE * 2));
                nLength += COMMAND_SIZE * 2;

                strType = hexData.Substring(nLength, DATA_TYPE_SIZE * 2);
                result.Add("Data_Type", hexData.Substring(nLength, DATA_TYPE_SIZE * 2));
                nLength += DATA_TYPE_SIZE * 2;

                result.Add("Reserved_F", hexData.Substring(nLength, RESERVED_F_SIZE * 2));
                nLength += RESERVED_F_SIZE * 2;

                // Error 체크
                ErrorCheck = hexData.Substring(nLength, ERROR_STATE_SIZE * 2);
                result.Add("ErrorState", ErrorCheck);
                nLength += ERROR_STATE_SIZE * 2;

                // ACK 
                if (ErrorCheck == "0000")
                {
                    // Data Count 16진수 -> 10진수 변환
                    getBytes = SMTFunction.StringToByteArray(hexData.Substring(nLength, DEVICE_COUNT_SIZE));
                    getCount = Buffer.GetByte(getBytes, 0);

                    // Count Swap
                    strSwap = SMTFunction.ReverseToSwap(hexData.Substring(nLength, DEVICE_COUNT_SIZE * 2));
                    result.Add("Device_Count_Swap", strSwap);

                    // Count
                    result.Add("Device_Count", hexData.Substring(nLength, DEVICE_COUNT_SIZE * 2));
                    nLength += DEVICE_COUNT_SIZE * 2;

                    switch (strCmd)
                    {
                        case "5500": // 읽기
                            for (int i = 0; i < getCount; i++)
                            {
                                // 각 데이터의 길이 10진수 변환
                                getBytes = SMTFunction.StringToByteArray(hexData.Substring(nLength, DEVICE_LENGTH_SIZE));
                                getLength = Buffer.GetByte(getBytes, 0);

                                // 각 데이터의 길이 저장
                                result.Add(String.Format("Device_Length_{0}", i + 1), hexData.Substring(nLength, DEVICE_LENGTH_SIZE * 2));
                                nLength += DEVICE_LENGTH_SIZE * 2;

                                // 각 데이터 저장
                                RECEIVE_DEVICE_SIZE[i] = getLength;
                                result.Add(String.Format("Device_{0}", i + 1), hexData.Substring(nLength, RECEIVE_DEVICE_SIZE[i] * 2));
                                nLength += RECEIVE_DEVICE_SIZE[i] * 2;
                            }
                            break;
                        case "5900":
                            // 헤더 + 명령어 + 데이터타입 + 예약영역 + 에러 상태 + 블록수
                            // 즉, 디바이스 카운터만 받으면 정상
                            break;
                    }
                }
                else
                {
                    // NAK
                    if (hexData.Length > nLength)
                    {
                        strSwap = hexData.Substring(nLength, ERROR_INFO_SIZE * 2);
                        result.Add("Error_Info", strSwap);
                        nLength += ERROR_INFO_SIZE * 2;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                SMTFunction.Log("procReceiveData - " + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Error Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string ErrorCode(string code)
        {
            string result;

            switch (code)
            {
                case "0100":
                    result = "개별 읽기/쓰기 요청시 블록 수가 16 보다 큼";
                    break;
                case "0200":
                    result = "X,B,W,D,L 이 아닌 데이터 타입을 수신했음";
                    break;
                case "0300":
                    result = "서비스 되지 않는 디바이스를 요구한 경우(XGK: P, M, L, K, R…, XGI: I, Q, M….)";
                    break;
                case "0400":
                    result = "각 디바이스별 지원하는 영역을 초과해서 요구한 경우";
                    break;
                case "0500":
                    result = "한번에 최대 1400byes 까지 읽거나 쓸 수 있는데 초과해서 요청한 경우 (개별 블록 사이즈)";
                    break;
                case "0600":
                    result = "한번에 최대 1400byes 까지 읽거나 쓸 수 있는데 초과해서 요청한 경우 (블록별 총 사이즈)";
                    break;
                case "7500":
                    result = "전용 서비스에서 프레임 헤더의 선두 부분이 잘못된 경우(‘LSIS-GLOFA’)";
                    break;
                case "7600":
                    result = "전용 서비스에서 프레임 헤더의 Length가 잘못된 경우";
                    break;
                case "7700":
                    result = "전용 서비스에서 프레임 헤더의 Checksum이 잘못된 경우";
                    break;
                case "7800":
                    result = "전용 서비스에서 명령어가 잘못된 경우";
                    break;
                default:
                    result = "PLC 에러 발생";
                    break;
            }

            return result;
        }
        #endregion

        #region Send Method
        /// <summary>
        /// 개별 읽기
        /// </summary>
        /// <param name="invoke"></param>
        /// <param name="cmd"></param>
        /// <param name="type"></param>
        /// <param name="strDevice"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        /// 
        public byte[] SendRead_Each(int invoke, SelectTYPE type, string[] strDevice)
        {
            int count = HEADER_SIZE;
            int nInvoke = invoke;
            int nType = (int)type;

            #region 데이터 개수, 길이 추출
            int TotalFormatSize, InstructionSize, DeviceSize = 0;
            int indiCount = 0;
            int[] DeviceLength = null;
            string[] DeviceAddress = null;

            if (strDevice != null)
            {
                DeviceLength = new int[strDevice.Length];
                DeviceAddress = new string[strDevice.Length];
            }

            foreach (string Individual in strDevice)
            {
                DeviceLength[indiCount] = Individual.Length;
                DeviceAddress[indiCount] = Individual;
                indiCount++;

                DeviceSize += Individual.Length;
            }

            InstructionSize = 8 + (strDevice.Length * 2) + DeviceSize;  // 고정 + (변동 * 2 Byte) + 변동
            TotalFormatSize = HEADER_SIZE + InstructionSize;    // 헤어사이즈 + 지시사이즈
            #endregion

            byte[] result = new byte[TotalFormatSize];

            // 전체 데이터 생성 시작
            try
            {
                // Get Header Method
                byte[] Descopy;
                Descopy = SetHeaderFormat(nInvoke, InstructionSize);
                Array.Copy(Descopy, 0, result, 0, Descopy.Length);

                // Command 2 byte
                result[count++] = COMMAND[1];
                result[count++] = COMMAND[0];

                // Data Type 2 byte
                result[count++] = DATA_TYPE[nType];
                result[count++] = DATA_TYPE[0];

                // Reserve 
                for (int i = 0; i < RESERVED_F.Length; i++)
                    result[count++] = RESERVED_F[i];

                // 변수 개수
                result[count++] = DEVICE_COUNT[strDevice.Length];
                result[count++] = DEVICE_COUNT[0];

                for (int i = 0; i < strDevice.Length; i++)
                {
                    byte[] data;

                    // 변수 길이
                    result[count++] = DEVICE_LENGTH[DeviceLength[i]];
                    result[count++] = DEVICE_LENGTH[0];

                    // 변수
                    data = SMTFunction.TextToByteArray(DeviceAddress[i]);
                    for (int j = 0; j < data.Length; j++)
                        result[count++] = data[j];
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("SendRead_Each - " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 연속 읽기
        /// </summary>
        /// <param name="invoke"></param>
        /// <param name="strDevice"></param>
        /// <returns></returns>
        public byte[] SendRead_Continue(int invoke, string strDevice, short dataCount)
        {
            int count = HEADER_SIZE;
            int nInvoke = invoke;
            int TotalFormatSize, InstructionSize;

            InstructionSize = 12 + strDevice.Length;      // 명령어(2) + 데이터 타입(2) + 예약 영역(2) + 변수 개수(2) + 변수명 길이(2) + 직접변수 + 데이터 개수(2)
            TotalFormatSize = HEADER_SIZE + InstructionSize;    // 헤어사이즈 + 지시사이즈

            byte[] result = new byte[TotalFormatSize];

            // 전체 데이터 생성 시작
            try
            {
                // Get Header Method
                byte[] Descopy;
                Descopy = SetHeaderFormat(nInvoke, InstructionSize);
                Array.Copy(Descopy, 0, result, 0, Descopy.Length);

                // Command 2 byte
                result[count++] = COMMAND[1];
                result[count++] = COMMAND[0];

                // 연속 읽기는 데이터 타입 0x14 고정
                result[count++] = DATA_TYPE[6];
                result[count++] = DATA_TYPE[0];

                // Reserve 
                for (int i = 0; i < RESERVED_F.Length; i++)
                    result[count++] = RESERVED_F[i];

                // 연속 읽기는 변수 개수 0x01 고정
                result[count++] = DEVICE_COUNT[1];
                result[count++] = DEVICE_COUNT[0];

                // 변수명 길이
                result[count++] = DEVICE_LENGTH[strDevice.Length];
                result[count++] = DEVICE_LENGTH[0];

                // 직접 변수 바이트로 변환 -> 이동
                byte[] lengthData, countData;

                // 변수
                lengthData = Encoding.UTF8.GetBytes(strDevice);
                for (int j = 0; j < lengthData.Length; j++)
                    result[count++] = lengthData[j];

                // 2byte short -> byte 변환 
                countData = BitConverter.GetBytes((short)(dataCount * 2));
                for (int i = 0; i < countData.Length; i++)
                {
                    result[count++] = countData[i];
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("SendRead_Continue - " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 개별 쓰기
        /// </summary>
        /// <param name="invoke"></param>
        /// <param name="cmd"></param>
        /// <param name="type"></param>
        /// <param name="strDevice"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public byte[] SendWrite_Each(int invoke, SelectTYPE type, string[] strDevice, string[] strValue)
        {
            int count = HEADER_SIZE;
            int nInvoke = invoke;
            int nType = (int)type;

            #region 데이터 개수, 길이 추출
            int TotalFormatSize, InstructionSize, DeviceSize = 0, ValueSize = 0;
            int indiCount = 0;
            int[] DeviceLength = null;
            string[] DeviceAddress = null;
            int[] ValueLength = null;
            string[] ValueData = null;

            if (strDevice != null)
            {
                DeviceLength = new int[strDevice.Length];
                DeviceAddress = new string[strDevice.Length];
            }

            if (strValue != null)
            {
                ValueLength = new int[strValue.Length];
                ValueData = new string[strValue.Length];
            }

            // 지시 데이터 사이즈 구한 이후 헤더 포맷 사이즈 입력
            foreach (string Individual in strDevice)
            {
                DeviceLength[indiCount] = Individual.Length;
                DeviceAddress[indiCount] = Individual;
                indiCount++;

                DeviceSize += Individual.Length;
            }
            indiCount = 0;

            foreach (string Individual in strValue)
            {
                ValueLength[indiCount] = Individual.Length;
                ValueData[indiCount] = Individual;
                indiCount++;

                ValueSize += Individual.Length;
            }

            InstructionSize = 8 + (strDevice.Length * 2) + DeviceSize + (strValue.Length * 2) + ValueSize;  // 고정 + (변수길이 * 2) + 변수명 + (데이터 크기 * 2) + 데이터명
            TotalFormatSize = HEADER_SIZE + InstructionSize;    // 헤어사이즈 + 지시사이즈
            #endregion

            byte[] result = new byte[TotalFormatSize];

            // 전체 데이터 생성 시작
            try
            {
                // Get Header Method
                byte[] Descopy;
                Descopy = SetHeaderFormat(nInvoke, InstructionSize);
                Array.Copy(Descopy, 0, result, 0, Descopy.Length);

                // Command 2 byte
                result[count++] = COMMAND[2];
                result[count++] = COMMAND[0];

                // Data Type 2 byte
                result[count++] = DATA_TYPE[nType];
                result[count++] = DATA_TYPE[0];

                // Reserve 
                for (int i = 0; i < RESERVED_F.Length; i++)
                    result[count++] = RESERVED_F[i];

                // 변수 개수
                result[count++] = DEVICE_COUNT[strDevice.Length];
                result[count++] = DEVICE_COUNT[0];

                for (int i = 0; i < strDevice.Length; i++)
                {
                    byte[] data;

                    // 변수 길이
                    result[count++] = DEVICE_LENGTH[DeviceLength[i]];
                    result[count++] = DEVICE_LENGTH[0];

                    // 변수
                    data = SMTFunction.TextToByteArray(DeviceAddress[i]);
                    for (int j = 0; j < data.Length; j++)
                        result[count++] = data[j];
                }

                for (int i = 0; i < strValue.Length; i++)
                {
                    byte[] data;

                    // 데이터 길이
                    result[count++] = VALUE_LENGTH[ValueLength[i]];
                    result[count++] = VALUE_LENGTH[0];

                    // 데이터
                    ushort convertShort = Convert.ToUInt16(ValueData[i]);
                    data = BitConverter.GetBytes(convertShort);

                    for (int j = 0; j < data.Length; j++)
                        result[count++] = data[j];
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("SendWrite_Each - " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Application Header Format
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private byte[] SetHeaderFormat(int invoke, int size)
        {
            int nCount = 0;
            byte[] result = new byte[HEADER_SIZE];

            try
            {
                // Company ID 8 byte
                for (int i = 0; i < COMPANY_ID.Length; i++)
                    result[nCount++] = COMPANY_ID[i];

                // Reserved 2 byte
                for (int i = 0; i < RESERVED_H.Length; i++)
                    result[nCount++] = RESERVED_H[i];

                // PLC Info 2 byte
                for (int i = 0; i < PLC_INFO.Length; i++)
                    result[nCount++] = PLC_INFO[i];

                // CPU Info 1 byte
                result[nCount++] = CPU_INFO;

                // Source of frame 1 byte
                result[nCount++] = SOURCE_OF_FRAME;

                // Invoke ID 2 byte
                result[nCount++] = (byte)invoke;
                result[nCount++] = INVOKE_ID;

                // length 2 byte
                result[nCount++] = (byte)size;
                result[nCount++] = LENGTH;

                // Module Position 1 byte
                result[nCount++] = MODULE_POSITION;

                // BCC 1 byte
                result[nCount++] = BCC;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("SetHeaderFormat - " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Send Data Processing
        /// </summary>
        /// <param name="getData"></param>
        /// <param name="nbytes"></param>
        /// <returns></returns>
        public Dictionary<string, string> procSendData(byte[] getData)
        {
            var result = new Dictionary<string, string>();

            string hexData;
            string strDataType;
            string strCmd;
            byte[] getBytes;
            int getLength, getCount, nLength = 0;

            try
            {
                hexData = SMTFunction.ByteArrayToString(getData);

                #region 헤더
                result.Add("Company_ID", hexData.Substring(nLength, COMPANY_ID_SIZE * 2));
                nLength += COMPANY_ID_SIZE * 2;

                result.Add("Reserved_H", hexData.Substring(nLength, RESERVED_H_SIZE * 2));
                nLength += RESERVED_H_SIZE * 2;

                result.Add("PLC_Info", hexData.Substring(nLength, PLC_INFO_SIZE * 2));
                nLength += PLC_INFO_SIZE * 2;

                result.Add("CPU_Info", hexData.Substring(nLength, CPU_INFO_SIZE * 2));
                nLength += CPU_INFO_SIZE * 2;

                result.Add("Source_of_Frame", hexData.Substring(nLength, SOURCE_OF_FRAME_SIZE * 2));
                nLength += SOURCE_OF_FRAME_SIZE * 2;

                result.Add("Invoke_ID", hexData.Substring(nLength, INVOKE_ID_SIZE * 2));
                nLength += INVOKE_ID_SIZE * 2;

                result.Add("Lenth", hexData.Substring(nLength, LENGTH_SIZE * 2));
                nLength += LENGTH_SIZE * 2;

                result.Add("Position", hexData.Substring(nLength, MODULE_POSITION_SIZE * 2));
                nLength += MODULE_POSITION_SIZE * 2;

                result.Add("BCC", hexData.Substring(nLength, BCC_SIZE * 2));
                nLength += BCC_SIZE * 2;
                #endregion

                #region 지시
                strCmd = hexData.Substring(nLength, COMMAND_SIZE * 2);
                result.Add("Command", hexData.Substring(nLength, COMMAND_SIZE * 2));
                nLength += COMMAND_SIZE * 2;

                strDataType = hexData.Substring(nLength, DATA_TYPE_SIZE * 2);
                result.Add("Data_Type", hexData.Substring(nLength, DATA_TYPE_SIZE * 2));
                nLength += DATA_TYPE_SIZE * 2;

                result.Add("Reserved_F", hexData.Substring(nLength, RESERVED_F_SIZE * 2));
                nLength += RESERVED_F_SIZE * 2;

                // Device Count 16진수 -> 10진수 변환
                getBytes = SMTFunction.StringToByteArray(hexData.Substring(nLength, DEVICE_COUNT_SIZE));
                getCount = Buffer.GetByte(getBytes, 0);

                result.Add("Device_Count", hexData.Substring(nLength, DEVICE_COUNT_SIZE * 2));
                nLength += DEVICE_COUNT_SIZE * 2;

                switch (strCmd)
                {
                    case "5400": // 읽기
                        for (int i = 0; i < getCount; i++)
                        {
                            // 각 데이터의 길이 10진수 변환
                            getBytes = SMTFunction.StringToByteArray(hexData.Substring(nLength, DEVICE_LENGTH_SIZE));
                            getLength = Buffer.GetByte(getBytes, 0);

                            // 각 데이터의 길이 저장
                            result.Add(String.Format("Device_Length_{0}", i + 1), hexData.Substring(nLength, DEVICE_LENGTH_SIZE * 2));
                            nLength += DEVICE_LENGTH_SIZE * 2;

                            // 각 데이터의 길이 만큼 데이터 저장
                            SEND_DEVICE_SIZE[i] = getLength;
                            result.Add(String.Format("Device_{0}", i + 1), hexData.Substring(nLength, SEND_DEVICE_SIZE[i] * 2));
                            nLength += SEND_DEVICE_SIZE[i] * 2;
                        }
                        break;
                    case "5800": // 개별 쓰기
                        for (int i = 0; i < getCount; i++)
                        {
                            // 각 데이터의 길이 10진수 변환
                            getBytes = SMTFunction.StringToByteArray(hexData.Substring(nLength, DEVICE_LENGTH_SIZE));
                            getLength = Buffer.GetByte(getBytes, 0);

                            // 각 데이터의 길이 저장
                            result.Add(String.Format("Device_Length_{0}", i + 1), hexData.Substring(nLength, DEVICE_LENGTH_SIZE * 2));
                            nLength += DEVICE_LENGTH_SIZE * 2;

                            // 각 데이터의 길이 만큼 데이터 저장
                            SEND_DEVICE_SIZE[i] = getLength;
                            result.Add(String.Format("Device_{0}", i + 1), hexData.Substring(nLength, SEND_DEVICE_SIZE[i] * 2));
                            nLength += SEND_DEVICE_SIZE[i] * 2;
                        }
                        for (int i = 0; i < getCount; i++)
                        {
                            // 각 데이터의 길이 10진수 변환
                            getBytes = SMTFunction.StringToByteArray(hexData.Substring(nLength, VALUE_LENGTH_SIZE));
                            getLength = Buffer.GetByte(getBytes, 0);

                            // 각 데이터의 길이 저장
                            result.Add(String.Format("Value_Length_{0}", i + 1), hexData.Substring(nLength, VALUE_LENGTH_SIZE * 2));
                            nLength += VALUE_LENGTH_SIZE * 2;

                            // 각 데이터의 길이 만큼 데이터 저장
                            VALUE_SIZE[i] = getLength;
                            result.Add(String.Format("Value_{0}", i + 1), hexData.Substring(nLength, VALUE_SIZE[i] * 2));
                            nLength += VALUE_SIZE[i] * 2;
                        }
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                SMTFunction.Log("procSendData - " + ex.Message);
            }
            return result;
        }
        #endregion
    }

    class KEYENCE_KV1000
    {
        #region Field 
        // Header
        private readonly byte CR = 0x0D;
        private readonly byte LF = 0x0A;
        private readonly byte SPACE = 0x20;
        private readonly byte[] START_CMD = new byte[] { 0x43, 0x52 };
        private readonly byte[] END_CMD = new byte[] { 0x43, 0x51 };
        private readonly byte[] COMMAND_RD = new byte[] { 0x52, 0x44 };
        private readonly byte[] COMMAND_RDS = new byte[] { 0x52, 0x44, 0x53 };

        private readonly int SEND_START_SIZE = 3;
        private readonly int SEND_END_SIZE = 3;
        private readonly int SEND_CR_SIZE = 1;
        private readonly int SEND_SPACE_SIZE = 1;
        private readonly int SEND_COMMAND_RD_SIZE = 2;
        private readonly int SEND_COMMAND_RDS_SIZE = 3;
        private readonly int SEND_DM_SIZE = 2;
        private readonly int SEND_COUNT_SIZE = 1;

        public byte[] packet_Buff;                   // 전역 버퍼
        private byte[] buffer = new byte[2048];     // 버퍼. 
        private int recvbytes;                         // 남은 바이트. 
        #endregion

        #region Constructor
        public KEYENCE_KV1000()
        {
            packet_Buff = null;
        }
        #endregion

        #region Receive Method
        public bool procReadBuffer(byte[] packet, int offset, int transffered)
        {
            bool result = false;
            int CRpos = -1;
            int LFpos = -1;
            int iPos = 0;

            try
            {
                // Length, Packet 지역 버퍼에 저장
                int iRecvCnt = transffered;
                byte[] bRecv = new byte[iRecvCnt];

                for (int i = 0; i < iRecvCnt; i++)
                {
                    bRecv[i] = packet[i];
                }

                // 전역 버퍼에 저장
                Array.Copy(bRecv, 0, buffer, recvbytes, iRecvCnt);
                recvbytes += iRecvCnt;

                // 패킷 처리
                while (true)
                {
                    // 실제 카운터가 패킷 보다 크면 빠져 나가기
                    if (iPos >= recvbytes)
                    {
                        break;
                    }

                    // CR Check
                    if (buffer[iPos] == CR)
                    {
                        CRpos = iPos;
                    }

                    // LF Check
                    if (buffer[iPos] == LF)
                    {
                        LFpos = iPos;
                    }

                    if (CRpos > -1 && LFpos > -1)
                    {
                        // 패킷 완료
                        if (CRpos < LFpos)
                        {
                            // 버퍼 초기화 및 복사
                            int totalLength = LFpos + 1;
                            packet_Buff = new byte[totalLength];

                            Array.Copy(buffer, 0, packet_Buff, 0, totalLength);
                            Array.Clear(buffer, 0, buffer.Length);

                            recvbytes = 0;
                            iPos = 0;
                            result = true;
                            break;
                        }
                        else
                        {
                            LFpos = -1;
                        }
                    }
                    iPos++;
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("procReadBuffer - " + ex.Message);
                recvbytes = 0;
                Array.Clear(buffer, 0, buffer.Length);
            }

            return result;
        }

        public Dictionary<string, string> procReceiveData(byte[] getData)
        {
            var result = new Dictionary<string, string>();
            string hexData, realData;
            string CR = "0d", LF = "0a";
            int DATA_SIZE, DATA_COUNT = 0;

            try
            {
                hexData = SMTFunction.ByteArrayToString(getData);

                if (hexData.Contains(CR) && hexData.Contains(LF))
                {
                    DATA_SIZE = hexData.Length - (CR.Length + LF.Length);
                    realData = hexData.Substring(0, DATA_SIZE);
                    string[] dataSegmentation = realData.Split(new string[] { "20" }, StringSplitOptions.None);

                    foreach (string data in dataSegmentation)
                    {
                        result.Add(string.Format("DATA_{0}", DATA_COUNT), data);
                        DATA_COUNT++;
                    }
                    result.Add("DATA_COUNT", dataSegmentation.Length.ToString());
                }
            }
            catch (Exception ex)
            {
                SMTFunction.Log("procReceiveData - " + ex.Message);
            }
            return result;
        }

        public string ErrorCode(string code)
        {
            string result;

            switch (code)
            {
                case "0100":
                    result = "개별 읽기/쓰기 요청시 블록 수가 16 보다 큼";
                    break;
                case "0200":
                    result = "X,B,W,D,L 이 아닌 데이터 타입을 수신했음";
                    break;
                default:
                    result = "PLC 에러 발생";
                    break;
            }

            return result;
        }
        #endregion

        #region Send Method
        public byte[] SendStart_Cmd()
        {
            int count = 0;
            byte[] result = new byte[SEND_START_SIZE];

            try
            {
                result[count++] = START_CMD[0];
                result[count++] = START_CMD[1];
                result[count++] = CR;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("StartCommand - " + ex.Message);
            }

            return result;
        }

        public byte[] SendEnd_Cmd()
        {
            int count = 0;
            byte[] result = new byte[SEND_END_SIZE];

            try
            {
                result[count++] = END_CMD[0];
                result[count++] = END_CMD[1];
                result[count++] = CR;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("EndCommand - " + ex.Message);
            }

            return result;
        }

        public byte[] SendRead_Each(string device, string deviceNo)
        {
            int count = 0;
            byte[] result;
            byte[] Dev, DevNo;

            Dev = Encoding.UTF8.GetBytes(device);
            DevNo = Encoding.UTF8.GetBytes(deviceNo);
            result = new byte[SEND_COMMAND_RD_SIZE + SEND_SPACE_SIZE + Dev.Length + DevNo.Length + SEND_CR_SIZE];

            try
            {
                // Comman 2 byte
                result[count++] = COMMAND_RD[0];
                result[count++] = COMMAND_RD[1];

                // Space 1 byte
                result[count++] = SPACE;

                // Device
                for (int i = 0; i < Dev.Length; i++)
                    result[count++] = Dev[i];

                // Device No
                for (int i = 0; i < DevNo.Length; i++)
                    result[count++] = DevNo[i];

                // CR
                result[count++] = CR;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("SendRead_Each - " + ex.Message);
            }

            return result;
        }

        public byte[] SendRead_Continue(string device, string deviceNo, string deviceCount)
        {
            int count = 0;
            byte[] result;
            byte[] Dev, DevNo, DeCount;

            Dev = Encoding.UTF8.GetBytes(device);
            DevNo = Encoding.UTF8.GetBytes(deviceNo);
            DeCount = Encoding.UTF8.GetBytes(deviceCount);
            result = new byte[SEND_COMMAND_RDS_SIZE + SEND_SPACE_SIZE + Dev.Length + DevNo.Length + SEND_SPACE_SIZE + DeCount.Length + SEND_CR_SIZE];

            try
            {
                // Comman 3 byte
                result[count++] = COMMAND_RDS[0];
                result[count++] = COMMAND_RDS[1];
                result[count++] = COMMAND_RDS[2];

                // Space 1 byte
                result[count++] = SPACE;

                // Device
                for (int i = 0; i < Dev.Length; i++)
                    result[count++] = Dev[i];

                // Device No
                for (int i = 0; i < DevNo.Length; i++)
                    result[count++] = DevNo[i];

                // Space 1 byte
                result[count++] = SPACE;

                // Number of Reads
                for (int i = 0; i < DeCount.Length; i++)
                    result[count++] = DeCount[i];

                // CR
                result[count++] = CR;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("SendRead_Each - " + ex.Message);
            }

            return result;
        }

        public Dictionary<string, string> procSendStartData(byte[] getData)
        {
            var result = new Dictionary<string, string>();
            string hexData;
            int nLength = 0;

            try
            {
                hexData = SMTFunction.ByteArrayToString(getData);

                result.Add("START", hexData.Substring(nLength, START_CMD.Length * 2));
                nLength += START_CMD.Length * 2;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("procSendStartData - " + ex.Message);
            }
            return result;
        }

        public Dictionary<string, string> procSendEndData(byte[] getData)
        {
            var result = new Dictionary<string, string>();
            string hexData;
            int nLength = 0;

            try
            {
                hexData = SMTFunction.ByteArrayToString(getData);

                result.Add("END", hexData.Substring(nLength, END_CMD.Length * 2));
                nLength += END_CMD.Length * 2;
            }
            catch (Exception ex)
            {
                SMTFunction.Log("procSendEndData - " + ex.Message);
            }
            return result;
        }

        public Dictionary<string, string> procSendEachData(byte[] getData)
        {
            var result = new Dictionary<string, string>();

            string hexData;
            string data = "";
            int getLength, nLength = 0;

            try
            {
                hexData = SMTFunction.ByteArrayToString(getData);

                result.Add("COMMAND_TYPE", hexData.Substring(nLength, COMMAND_RD.Length * 2));
                nLength += COMMAND_RD.Length * 2;

                result.Add("SPACE", hexData.Substring(nLength, SEND_SPACE_SIZE * 2));
                nLength += SEND_SPACE_SIZE * 2;

                result.Add("DEVICE", hexData.Substring(nLength, SEND_DM_SIZE * 2));
                nLength += SEND_DM_SIZE * 2;

                getLength = hexData.Length - (SEND_CR_SIZE * 2);
                for (int i = nLength; i < getLength; i++)
                {
                    data += hexData[i];
                }
                result.Add("DEVICE_NO", data);
                nLength += getLength * 2;

            }
            catch (Exception ex)
            {
                SMTFunction.Log("procSendEachData - " + ex.Message);
            }
            return result;
        }

        public Dictionary<string, string> procSendContinueData(byte[] getData)
        {
            var result = new Dictionary<string, string>();

            string hexData;
            string data = "";
            int getLength, nLength = 0;

            try
            {
                hexData = SMTFunction.ByteArrayToString(getData);

                result.Add("COMMAND_TYPE", hexData.Substring(nLength, COMMAND_RDS.Length * 2));
                nLength += COMMAND_RDS.Length * 2;

                result.Add("SPACE_1", hexData.Substring(nLength, SEND_SPACE_SIZE * 2));
                nLength += SEND_SPACE_SIZE * 2;

                result.Add("DEVICE", hexData.Substring(nLength, SEND_DM_SIZE * 2));
                nLength += SEND_DM_SIZE * 2;

                getLength = hexData.Length - (SEND_SPACE_SIZE * 2) - (SEND_COUNT_SIZE * 2) - (SEND_CR_SIZE * 2);
                for (int i = nLength; i < getLength; i++)
                {
                    data += hexData[i];
                }
                result.Add("DEVICE_NO", data);
                nLength += data.Length;

                result.Add("SPACE_2", hexData.Substring(nLength, SEND_SPACE_SIZE * 2));
                nLength += SEND_SPACE_SIZE * 2;

                result.Add("DEVICE_COUNT", hexData.Substring(nLength, SEND_COUNT_SIZE * 2));
                nLength += SEND_COUNT_SIZE * 2;

            }
            catch (Exception ex)
            {
                SMTFunction.Log("procSendContinueData - " + ex.Message);
            }
            return result;
        }
        #endregion
    }
}
