using SmartMES.Common;
using System;
using System.Data;

using SmartCommon.Common;
namespace SmartMES.View.Monitoring.Machine
{
    public partial class ucTitle : DevExpress.XtraEditors.XtraUserControl
    {
        Database db = new Database();
        string query = "";

        public ucTitle()
        {
            InitializeComponent();
            Display();

        }

        public ucTitle(string code1, string code2)
        {
            InitializeComponent();
            Display();
        }


        private void Display()
        {
            query = "EXEC Monitoring_GetMachineCount";
           

            DataRowView item = db.GetDataRecord(query);
            if (item == null) return;

            int Normal = Convert.ToInt32(item["기본"]);
            int Work = Convert.ToInt32(item["가동"]);
            int NotWork = Convert.ToInt32(item["비가동"]);
            int Complte = Convert.ToInt32(item["완료"]);

            label13.Text = Normal.ToString();
            label14.Text = Work.ToString();
            //label15.Text = StatusCount2.ToString();
            //label16.Text = StatusCount3.ToString();
            label17.Text = NotWork.ToString();
            label18.Text = Complte.ToString();

            int Sum = Normal + Work + NotWork + Complte ;
            lblSUM.Text = Sum.ToString();

        }

   
    }
}
