using DevExpress.XtraLayout;
using SmartCommon.Interfaces;
using System;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using SmartCommon.Common;

namespace SmartCommon.Lookup
{
    public partial class frmPostCode : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetAddress(string code , string Name);
        public event SetAddress onSetAddress;

        string currentPage = "1";  //현재 페이지
        string countPerPage = "1000"; //1페이지당 출력 갯수
        //string confmKey = "U01TX0FVVEgyMDE4MDkyODE0NTIxMDEwODIwMjU="; //테스트 Key
        //string confmKey = "U01TX0FVVEgyMDE4MDkyODE1MDU0MzEwODIwMjc="; //테스트 Key
        string confmKey = "U01TX0FVVEgyMDE4MDUxNTEwMjExMjEwNzg3NjU="; //테스트 Key
       // string confmKey = "devU01TX0FVVEgyMDI0MDgxNjE2MjUwMzExNTAxNjU="; //테스트 Key
        
        string keyword = string.Empty;
        string apiurl = string.Empty;

        int selAptPoint = 0;


        ISetData iset;

        public frmPostCode()
        {
            InitializeComponent();
        }

        public frmPostCode(ISetData data)
        {
            InitializeComponent();
            iset = data;
        }


        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            try
            {
                //keyword = textSearch.Text.Trim();
                //apiurl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + currentPage + "&countPerPage=" + countPerPage + "&keyword=" + keyword + "&confmKey=" + confmKey;

                //WebClient wc = new WebClient();
                //XmlReader read = new XmlTextReader(wc.OpenRead(apiurl));
                //DataSet ds = new DataSet();
                //ds.ReadXml(read);
                //DataRow[] rows = ds.Tables[0].Select();
                //if (rows[0]["totalCount"].ToString() != "0")
                //{
                //    gcList.DataSource = ds.Tables[1];
                //}

                keyword = textSearch.Text.Trim();
                apiurl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + currentPage + "&countPerPage=" + countPerPage + "&keyword=" + keyword + "&confmKey=" + confmKey;

                WebClient wc = new WebClient();
                XmlReader read = new XmlTextReader(wc.OpenRead(apiurl));
                DataSet ds = new DataSet();
                ds.ReadXml(read);
                DataRow[] rows = ds.Tables[0].Select();
                if (rows[0]["totalCount"].ToString() != "0")
                    gcList.DataSource = ds.Tables[1];
                else
                {
                    MessageBox.Show(rows[0][4].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void textSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Return == e.KeyCode)
            {
                DisplayData();
            }

        }

        private void cmdChoice_Click(object sender, EventArgs e)
        {
            DoubleClickRow();
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            DoubleClickRow();
        }

        private void DoubleClickRow()
        {
            //string postCode = gvList.GetFocusedRowCellDisplayText("zipNo");
            //string address = gvList.GetFocusedRowCellDisplayText("roadAddr");
            //iset.SetData(postCode, address);
            //this.Close();
            try
            {
                //WrGlobal.Code =
                //WrGlobal.CodeTitle 

                selAptPoint = 0;
                string Building = gvList.GetFocusedRowCellDisplayText("bdNm")?.ToString() ?? "";
                string sendAddr = gvList.GetFocusedRowCellDisplayText("roadAddr")?.ToString() ?? "";
                string sendPost = gvList.GetFocusedRowCellDisplayText("zipNo")?.ToString() ?? "";


                onSetAddress(sendPost , sendAddr);

                if (Building.Length > 0 &&
                    Building.IndexOf("아파트") >= 0 ||
                    Building.IndexOf("맨션") >= 0 || Building.IndexOf("연립") >= 0 ||
                    Building.IndexOf("빌딩") >= 0 || Building.IndexOf("빌라") >= 0 ||
                    Building.IndexOf("오피스") >= 0 || Building.IndexOf("빌리지") >= 0 ||
                    Building.IndexOf("하우스") >= 0 || Building.IndexOf("빌") == Building.Length ||
                    Building.LastIndexOf("타워") + 1 == Building.Length - 1 ||
                    Building.LastIndexOf("타운") + 1 == Building.Length - 1 ||
                    Building.LastIndexOf("주택") + 1 == Building.Length - 1)
                {
                    selAptPoint = sendAddr.IndexOf("(") - 1;
                }

                if (selAptPoint > 0)
                {
                    gcList.Enabled = false;
                    
                    //txtApt.Text = "";
                    //txtApt.Focus();
                }
                else
                {
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}