using DevExpress.DashboardCommon;
using DevExpress.DataAccess;
using SmartCommon.Common;
using SmartMES.View.QM.Dash.DsKPIDATATableAdapters;
//using SMT_MES.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.View.QM.Dash
{
    public partial class DashKPI : DevExpress.DashboardCommon.Dashboard
    {
        public DashKPI()
        {

            InitializeComponent();

            불량률TableAdapter1.Connection.ConnectionString = $@"Data Source=smtv.iptime.org,2433;Initial Catalog=KPIDATA;User ID=smtv;Password=#smtv@9150$";
            제조리드타임TableAdapter1.Connection.ConnectionString = $@"Data Source=smtv.iptime.org,2433;Initial Catalog=KPIDATA;User ID=smtv;Password=#smtv@9150$";
            출하리드타임TableAdapter1.Connection.ConnectionString = $@"Data Source=smtv.iptime.org,2433;Initial Catalog=KPIDATA;User ID=smtv;Password=#smtv@9150$";

            //납기리드타임TableAdapter1.Connection.ConnectionString = $@"Data Srouce=smtv.iptime.org,2433;Initial Catalog=KDIDATA;User ID=smtv;Password=#smtv@9150$";

            string dbName = Database.DbName.ToUpper();

            if (dbName == "JIWOO")
            {
                //수주 - 출하 리드타임 미사용
                this.Items.Remove("GcSLeadTime");
                this.Items.Remove("ChartSLeadTime");
                this.Groups.Remove("GroupSLeadTime");
            }
            else if (dbName == "IWAUTO")
            {
                //제조 리드타임 미사용
                this.Items.Remove("GcMLeadTime");
                this.Items.Remove("ChartMLeadTime");
                this.Groups.Remove("GroupMLeadTime");
            }
            else if (dbName == "HANSUNG")
            {
                //제조 리드타임 미사용
                this.Items.Remove("GcMLeadTime");
                this.Items.Remove("ChartMLeadTime");
                this.Groups.Remove("GroupMLeadTime");

                //불량률 대신 PPM 사용
                GcFault.Columns[4].Name = "공정불량율(PPM)";
                /*
                //불량률 대신 PPM 사용
                Measure measure = new Measure();
                measure.DataMember = "불량률";
                measure.Name = "PPM";
                measure.NumericFormat.CustomFormatString = "";
                measure.NumericFormat.IncludeGroupSeparator = true;                   //구분자(,)
                measure.NumericFormat.FormatType =DataItemNumericFormatType.Number;
                measure.NumericFormat.Unit = DataItemNumericUnit.Ones;                //표기 단위
                measure.NumericFormat.Precision = 1;                                  //소수점

                GridMeasureColumn col = new GridMeasureColumn();
                col.Name = "PPM";
                col.Weight = 74.999999999999986D;
                col.WidthType = GridColumnFixedWidthType.Weight;
                col.AddDataItem("Measure", measure);

                GcFault.Columns.Add(col);*/
            }
            else if (dbName == "DEVELOP")
            {
                //제조 리드타임 미사용
                this.Items.Remove("chartDashboardItem2");
                this.Items.Remove("GcFault");
                this.Groups.Remove("dashboardItemGroup1");
                GcSLeadTime.Name = "납기 단축 집계";
                GroupSLeadTime.Name = "납기 리드 타임";
                ChartSLeadTime.Name = "납기 리드 타임";
            }
            else if (dbName == "Sambo")
            {
                //제조 리드타임 미사용
                this.Items.Remove("GcMLeadTime");
                this.Items.Remove("ChartMLeadTime");
                this.Groups.Remove("GroupMLeadTime");

                //수주 - 출하 리드타임 미사용
                //this.Items.Remove("GcSLeadTime");
                //this.Items.Remove("ChartSLeadTime");
                //this.Groups.Remove("GroupSLeadTime");
            }
            else if (dbName == "Hosung")
            {
                //제조 리드타임 미사용
                this.Items.Remove("GcMLeadTime");
                this.Items.Remove("ChartMLeadTime");
                this.Groups.Remove("GroupMLeadTime");
            }
            else if (dbName == "DOOIL" || dbName == "DOOIL_G")
            {
                GroupMLeadTime.Name = "생산실적";
                GcMLeadTime.ShowCaption = false;
                ChartMLeadTime.ShowCaption = false;
                GcMLeadTime.Columns[1].Name = "월간 생산시간(HR)";
                GcMLeadTime.Columns[2].Name = "월간 생산수량(EA)";
                GcMLeadTime.Columns[3].Name = "월간 시간당 생산량(EA)";

                GcSLeadTime.ShowCaption = false;
                ChartSLeadTime.ShowCaption = false;
                ChartMLeadTime.Legend.Visible = false;

                this.Items.Remove("GcSLeadTime");
                this.Items.Remove("ChartSLeadTime");
                this.Groups.Remove("GroupSLeadTime");
            }
            else if (dbName == "RNDZIPPER")
            {

                GcFault.Columns[2].Name = "생산수량(M)";
                GcFault.Columns[3].Name = "불량수량(M)";


                GroupMLeadTime.Name = "생산량";
                GcMLeadTime.ShowCaption = false;
                ChartMLeadTime.ShowCaption = false;
                ChartMLeadTime.Legend.Visible = false;
                GcMLeadTime.Columns[1].Name = "염색 공정 작업시간";
                GcMLeadTime.Columns[2].Name = "염색공정 총 생산수량";
                GcMLeadTime.Columns[3].Name = "시간당 생산량";

                GcMLeadTime.ShowCaption = false;
                ChartMLeadTime.ShowCaption = false;

                //납기리드타임 미사용
                this.Items.Remove("GcSLeadTime");
                this.Items.Remove("ChartSLeadTime");
                this.Groups.Remove("GroupSLeadTime");
            }
            else if (dbName == "GJY")
            {
                GroupMLeadTime.Name = "생산량";
                GcMLeadTime.ShowCaption = false;
                ChartMLeadTime.ShowCaption = false;
                ChartMLeadTime.Legend.Visible = false;
                GcMLeadTime.Columns[1].Name = "투입시간(HR)";
                GcMLeadTime.Columns[2].Name = "총 생산수량(EA)";
                GcMLeadTime.Columns[3].Name = "시간당 생산량(EA/HR)";

                GcMLeadTime.ShowCaption = false;
                ChartMLeadTime.ShowCaption = false;

                //납기리드타임 미사용
                this.Items.Remove("GcSLeadTime");
                this.Items.Remove("ChartSLeadTime");
                this.Groups.Remove("GroupSLeadTime");
            }
            else if (dbName == "KNP")
            {
                GroupMLeadTime.Name = "작업공수";
                GcMLeadTime.ShowCaption = false;
                ChartMLeadTime.ShowCaption = false;
                ChartMLeadTime.Legend.Visible = false;

                GcMLeadTime.Columns[1].Name = "작업지시(건수)";
                GcMLeadTime.Columns[2].Name = "제조리드타임(일수)";
                GcMLeadTime.Columns[3].Name = "평균제조리드타임";


                GroupSLeadTime.Name = "수주출하리드타임";
                GcSLeadTime.ShowCaption = false;
                ChartSLeadTime.ShowCaption = false;
                ChartSLeadTime.Legend.Visible = false;

                GcSLeadTime.Columns[1].Name = "수주건수";
                GcSLeadTime.Columns[2].Name = "수주출하리드타임(일수)";
                GcSLeadTime.Columns[3].Name = "평균수주출하리드타임";

                this.Items.Remove("GcFault");
                this.Items.Remove("chartDashboardItem2");
                this.Groups.Remove("dashboardItemGroup1");
            }
            else if (dbName == "UAGHITEC" || dbName == "UAGHITEC_G")
            {
                GroupMLeadTime.Name = "시간당 생산량";
                GcMLeadTime.ShowCaption = false;
                ChartMLeadTime.ShowCaption = false;
                ChartMLeadTime.Legend.Visible = false;

                GcMLeadTime.Columns[1].Name = "생산시간(HR)";
                GcMLeadTime.Columns[2].Name = "생산수량(EA)";
                GcMLeadTime.Columns[3].Name = "UPH";

                dashboardItemGroup1.Name = "납기준수율";
                chartDashboardItem2.Name = "고객사별 수주(발주) 현황";
                GcFault.Name = "납기준수율";
                GcFault.Columns[1].Name = "고객사";
                GcFault.Columns[2].Name = "납품건수";
                GcFault.Columns[3].Name = "납기준수";
                GcFault.Columns[4].Name = "납기준수율";

                GcSLeadTime.ShowCaption = false;
                ChartSLeadTime.ShowCaption = false;
                ChartMLeadTime.Legend.Visible = false;

                this.Items.Remove("GcSLeadTime");
                this.Items.Remove("ChartSLeadTime");
                this.Groups.Remove("GroupSLeadTime");
            }

            불량률TableAdapter1.FillByDB(dsKPIDATA1.불량률, dbName);
            제조리드타임TableAdapter1.FillByDB(dsKPIDATA2.제조리드타임, dbName);
            출하리드타임TableAdapter1.FillByDB(dsKPIDATA3.출하리드타임, dbName);
            //납기리드타임TableAdapter1.FillByDB(dsKPIDATA4.납기리드타임, dbName);
        }
    }
}
