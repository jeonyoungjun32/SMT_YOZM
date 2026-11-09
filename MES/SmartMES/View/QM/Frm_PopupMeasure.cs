using System;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.Win32;

namespace SmartMES.View.QM
{
    public partial class Frm_PopupMeasure : DevExpress.XtraEditors.XtraForm
    {
        private string receivedDataBuffer = string.Empty;
        public Frm_PopupMeasure()
        {
            InitializeComponent();
        }

        private void Frm_PopupMeasure_Load(object sender, EventArgs e)
        {
            SerialPort Port = new SerialPort();
            string[] Ports = SerialPort.GetPortNames();
            lb_Port.Items.AddRange(Ports);

            ReadRegValue();

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        public void ReadRegValue()
        {
            string server = null;
            string db = null;
            string id = null;
            string pwd = null;
            string pda = null;

            try
            {
                RegistryKey reg = Registry.CurrentUser;

                reg = reg.OpenSubKey(@"SOFTWARE\SMARTMES\MEASURE", true);

                cb_Port.Text = (string)reg.GetValue("PortName");
                cb_BPS.Text = (string)reg.GetValue("BaudRate");
                cb_DB.Text = (string)reg.GetValue("DataBits");
                cb_Prt.Text = (string)reg.GetValue("Parity");
                cb_SB.Text = (string)reg.GetValue("StopBits");
                cb_Fc.Text = (string)reg.GetValue("FlowCont");

                reg.Close();

            }
            catch (Exception ex)
            {
                try
                {
                    RegistryKey reg = Registry.CurrentUser;
                    reg = reg.CreateSubKey("SOFTWARE").CreateSubKey("SMARTMES").CreateSubKey("MEASURE");

                    reg.SetValue("PortName", "COM1");
                    reg.SetValue("BaudRate", "2400");
                    reg.SetValue("DataBits", "8");
                    reg.SetValue("Parity", "None");
                    reg.SetValue("StopBits", "1");
                    reg.SetValue("FlowCont", "None");

                    cb_Port.Text = "COM1";
                    cb_BPS.Text = "2400";
                    cb_DB.Text = "8";
                    cb_Prt.Text = "None";
                    cb_SB.Text = "1";
                    cb_Fc.Text = "None";

                }
                catch (Exception ex2)
                {
                }

            }
        }

        private void Port_OPEN()
        {
            try
            {
                if (serialPort1.IsOpen == false)
                {
                    serialPort1.PortName = cb_Port.Text;
                    serialPort1.BaudRate = Convert.ToInt32(cb_BPS.Text);
                    serialPort1.DataBits = Convert.ToInt32(cb_DB.Text);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cb_SB.Text);
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cb_Prt.Text);

                    //221214 안창호 LZ-373 데이터 받기가 핸드쉐이크 RTS 추가해야 받아와짐
                    serialPort1.Handshake = Handshake.RequestToSend;

                    //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    serialPort1.ReadTimeout = 100;
                    serialPort1.WriteTimeout = 100;
                    serialPort1.Open();

                    //cnt_state = true;

                    txtStatus.Text = "새로 연결됨";

                    WriteRegValue();
                }
                else
                {
                    txtStatus.Text = "연결되어 있습니다.";
                }

            }
            catch (Exception)
            {
                txtStatus.Text = "연결할수없습니다.";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }


        private void lb_Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_Port.SelectedIndex >= 0)
            {
                cb_Port.Text = Convert.ToString(lb_Port.SelectedItem);
            }
        }

        private void btn_discnt_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen == true)
                {
                    serialPort1.Close();

                    //cnt_state = false;

                    txtStatus.Text = "해제되었습니다.";
                }
                else
                {
                    txtStatus.Text = "해제되어 있습니다 .";
                }
            }
            catch (Exception)
            {
                txtStatus.Text = "해제할수없습니다.";
            }
        }

        private void btn_cnt_Click(object sender, EventArgs e)
        {
            Port_OPEN();
            if(txtStatus.Text == "새로 연결됨")
            {
                MessageBox.Show("연결되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else if (txtStatus.Text == "연결할수없습니다.")
            {
                MessageBox.Show("연결할 수 없습니다. \n포트를 다시 설정하세요.");
            }
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            WriteRegValue();
        }


        public void WriteRegValue()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser;
                reg = reg.OpenSubKey(@"SOFTWARE\SMARTMES\MEASURE", true);

                reg.SetValue("PortName", cb_Port.Text.Trim());
                reg.SetValue("BaudRate", cb_BPS.Text.Trim());
                reg.SetValue("DataBits", cb_DB.Text.Trim());
                reg.SetValue("Parity", cb_Prt.Text.Trim());
                reg.SetValue("StopBits", cb_SB.Text.Trim());
                reg.SetValue("FlowCont", cb_Fc.Text.Trim());

                reg.Close();

            }
            catch (Exception ex)
            {
                try
                {
                    RegistryKey reg = Registry.CurrentUser;
                    reg = reg.CreateSubKey("SOFTWARE").CreateSubKey("SMARTMES").CreateSubKey("MEASURE");

                    reg.SetValue("PortName", "COM1");
                    reg.SetValue("BaudRate", "2400");
                    reg.SetValue("DataBits", "8");
                    reg.SetValue("Parity", "None");
                    reg.SetValue("StopBits", "1");
                    reg.SetValue("FlowCont", "None");
                }
                catch (Exception ex2)
                {
                }

            }
        }

        private void cmd_End_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //this.Invoke(new EventHandler(MySerialReceived));
            //SerialPort sp = (SerialPort)sender;
            //string data = sp.ReadExisting();

            //memoEdit1.Invoke((MethodInvoker)delegate
            //{
            //    memoEdit1.Text += "수신된 데이터 : \n" + data;
            //});

            memoEdit1.Invoke((MethodInvoker)delegate
            {
                memoEdit1.Text = string.Empty;
            });

            Delay(1500);

            receivedDataBuffer += serialPort1.ReadExisting();

            // '\n'를 기준으로 데이터를 분리
            string[] lines = receivedDataBuffer.Split('\n');


            // 마지막 줄을 제외한 나머지 줄 처리
            for (int i = 0; i < lines.Length - 1; i++)
            {
                string line = lines[i];
                ProcessReceivedData(line);
            }

            // 버퍼에 남아있는 마지막 줄을 다음 데이터와 결합
            receivedDataBuffer = lines[lines.Length - 1];
        }

        private void ProcessReceivedData(string data)
        {
            memoEdit1.Invoke((MethodInvoker)delegate
            {
                memoEdit1.Text += data + "\n";
            });
        }

        private static DateTime Delay(int MS)
        {
            // Thread 와 Timer보다 효율 적으로 사용할 수 있음.
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
    }
}