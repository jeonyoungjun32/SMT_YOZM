using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MobileViewer
{
	public static class clsGrid
	{
        /// <summary>
        /// ProjectField 테이블 기반으로 일반 그리드 컬럼을 생성
        /// </summary>
        public static void CreateColumn(BootstrapGridView grid, string projectId)
        {
            grid.Columns.Clear();

            clsDatabase db = new clsDatabase();
            string query = "SELECT * from MB_ProjectField WHERE ProjectId = " + projectId + " ORDER BY DisplayOrder";

            DataView dv = db.GetDataView("ProjectField", query);

            foreach (DataRowView drv in dv)
            {
                ProjectField colDef = new ProjectField(drv);

                // 컬럼 객체 생성
                BootstrapGridViewDataColumn column = CreateGridColumn(colDef);

                // 밴드 로직 없이 그리드에 직접 컬럼을 추가
                grid.Columns.Add(column);
            }
        }

        /// <summary>
        /// ProjectField 테이블 기반으로 밴디드 그리드 컬럼을 생성
        /// </summary>
        public static void CreateBandedColumn(BootstrapGridView grid, string projectId)
        {
            grid.Columns.Clear();

            clsDatabase db = new clsDatabase();
            string query = "SELECT * from MB_ProjectField WHERE ProjectId = " + projectId + " ORDER BY DisplayOrder";

            DataView dv = db.GetDataView("ProjectField", query);

            Dictionary<string, BootstrapGridViewBandColumn> bandCache = new Dictionary<string, BootstrapGridViewBandColumn>();

            foreach (DataRowView drv in dv)
            {
                ProjectField colDef = new ProjectField(drv);

                BootstrapGridViewDataColumn column = CreateGridColumn(colDef);

                if (!string.IsNullOrEmpty(colDef.BandCaption))
                {
                    BootstrapGridViewBandColumn band;

                    // 캐시에 밴드가 없으면 새로 생성하고 그리드에 추가
                    if (!bandCache.TryGetValue(colDef.BandCaption, out band))
                    {
                        band = new BootstrapGridViewBandColumn { Caption = colDef.BandCaption };
                        bandCache[colDef.BandCaption] = band;
                        grid.Columns.Add(band);
                    }
                    // 해당 밴드에 데이터 컬럼 추가
                    band.Columns.Add(column);
                }
                else
                {
                    // 밴드가 없으면 그리드에 직접 추가
                    grid.Columns.Add(column);
                }
            }
        }

        // 컬럼 정보를 바탕으로 실제 GridViewColumn 객체를 생성하는 팩토리 메서드
        private static BootstrapGridViewDataColumn CreateGridColumn(ProjectField def)
        {
            BootstrapGridViewDataColumn column;

            // 컬럼 타입으로 분기
            if (def.ColumnType == "COMBO") //콤보
            {
                BootstrapGridViewComboBoxColumn comboCol = new BootstrapGridViewComboBoxColumn();
                // 콤보박스 관련 추가 속성 설정
                comboCol.PropertiesComboBox.ClientInstanceName = def.FieldName + "Combo";
                comboCol.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDownList;

                column = comboCol;
            }
            else if (def.ColumnType == "DATE") //날짜
            {
                BootstrapGridViewDateColumn dateCol = new BootstrapGridViewDateColumn();

                dateCol.PropertiesDateEdit.DisplayFormatString = "yyyy-MM-dd";
                dateCol.PropertiesDateEdit.EditFormat = EditFormat.Date;
                dateCol.PropertiesDateEdit.EditFormatString = "yyyy-MM-dd";
                dateCol.PropertiesDateEdit.TimeSectionProperties.Visible = false;

                column = dateCol;
            }
            else // 기본은 TextColumn
            {
                BootstrapGridViewTextColumn textCol = new BootstrapGridViewTextColumn();

                switch (def.FieldType.ToUpper())
                {
                    case "INT":
                        textCol.PropertiesEdit.DisplayFormatString = "{0:#,0}";
                        break;
                    case "FLOAT1":
                        textCol.PropertiesEdit.DisplayFormatString = "{0:#,0.0}";
                        break;
                    case "FLOAT2":
                        textCol.PropertiesEdit.DisplayFormatString = "{0:#,0.00}";
                        break;
                }
                column = textCol;
            }

            // 모든 컬럼 타입에 공통적인 속성 설정
            column.FieldName = def.FieldName;
            column.Caption = def.Caption;
            column.Width = new Unit(def.Width * 6);
            column.Visible = def.IsVisible;

            // 편집 가능 여부 설정
            if (def.IsEditable)
            {
                column.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            else
			{
                column.ReadOnly = true;
                column.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
			}

            //컬럼 고정
            if (def.Fixed == "LEFT") { column.FixedStyle = GridViewColumnFixedStyle.Left;}

            return column;
        }

        private class ProjectField
        {
            public string ColumnType { get; set; }
            public string FieldName { get; set; }
            public string Caption { get; set; }
            public int Width { get; set; }
            public bool IsVisible { get; set; }
            public bool IsEditable { get; set; }
            public string FieldType { get; set; }
            public string BandCaption { get; set; }
            public string Fixed { get; set; }

            public ProjectField(DataRowView drv)
			{
				int w = 0;
				Int32.TryParse(Convert.ToString(drv["ListWidth"]), out w);

				ColumnType = Convert.ToString(drv["ColumnType"]);
				FieldName = Convert.ToString(drv["FieldName"]);
				Caption = Convert.ToString(drv["ShortCaption"]);
				Width = w == 0 ? 10 : w;
				IsVisible = drv["VisibleYN"].ToString() == "Y";
				IsEditable = Convert.ToString(drv["Editable"]) != "0";
				FieldType = Convert.ToString(drv["FieldType"]);
				BandCaption = Convert.ToString(drv["BandCaption"]);
                Fixed = Convert.ToString(drv["Fixed"]);
			}
        }
    }
}
