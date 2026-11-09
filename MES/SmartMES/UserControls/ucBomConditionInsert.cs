using DevExpress.XtraLayout;
using SmartCommon.Common;
using System.Data;

namespace SmartMES.UserControls
{
	public partial class ucBomConditionInsert : DevExpress.XtraEditors.XtraUserControl
    {
        Database db = new Database();
        string query = "";

        public ucBomConditionInsert()
        {
            InitializeComponent();
        }

        public void SetCondition(string processCode, string lineCode)
        {
            //this.layoutControl1.Controls.Clear();

            query = $"Select * From CM_LineCondition Where CorpId = '{WrGlobal.CorpID}' and ProcessCode = '{processCode}' and LineCode = '{lineCode}' Order by ItemSeq";
            DataView dv = db.GetDataView("CM_LineCondition", query);


            BaseLayoutItem[] layoutCollection = new BaseLayoutItem[dv.Count * 3];

            if (dv != null)
            {
                int GroupIndex = 1;

                foreach (DataRowView item in dv)
                {
                    DevExpress.XtraEditors.TextEdit ItemSeq = new DevExpress.XtraEditors.TextEdit();
                    ItemSeq.Name = "ItemSeq" + GroupIndex.ToString();
                    ItemSeq.Location = new System.Drawing.Point(14, 34 + (36 * (GroupIndex - 1)));
                    ItemSeq.Properties.Padding = new System.Windows.Forms.Padding(2);
                    ItemSeq.Properties.ReadOnly = true;
                    ItemSeq.Size = new System.Drawing.Size(58, 28);
                    ItemSeq.StyleController = this.layoutControl1;
                    ItemSeq.TabIndex = GroupIndex + 3;
                    ItemSeq.Text = item["ItemSeq"].ToString();
                    this.layoutControl1.Controls.Add(ItemSeq);

                    LayoutControlItem SeqLayout = new LayoutControlItem();
                    SeqLayout.Control = ItemSeq;
                    SeqLayout.Name = "SeqLayout" + GroupIndex.ToString();
                    if (GroupIndex == 1)
                    {
                        SeqLayout.Location = new System.Drawing.Point(0, 0);
                        SeqLayout.MaxSize = new System.Drawing.Size(66, 56);
                        SeqLayout.MinSize = new System.Drawing.Size(66, 56);
                        SeqLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
                        SeqLayout.Size = new System.Drawing.Size(66, 56);
                        SeqLayout.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                        SeqLayout.Text = "순번";
                        SeqLayout.TextLocation = DevExpress.Utils.Locations.Top;
                        SeqLayout.TextSize = new System.Drawing.Size(48, 17);
                    }
                    else
                    {
                        SeqLayout.Location = new System.Drawing.Point(0, 56 + (36 * (GroupIndex - 1)));
                        SeqLayout.MaxSize = new System.Drawing.Size(66, 32);
                        SeqLayout.MinSize = new System.Drawing.Size(66, 32);
                        SeqLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
                        SeqLayout.Size = new System.Drawing.Size(66, 36);
                        SeqLayout.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                        SeqLayout.TextSize = new System.Drawing.Size(0, 0);
                        SeqLayout.TextVisible = false;
                    }
                    layoutCollection[(GroupIndex - 1) * 3] = SeqLayout;

                    DevExpress.XtraEditors.TextEdit ItemName = new DevExpress.XtraEditors.TextEdit();
                    ItemName.Name = "ItemName" + GroupIndex.ToString();
                    ItemName.Location = new System.Drawing.Point(80, 34 + (36 * (GroupIndex - 1)));
                    ItemName.Properties.Padding = new System.Windows.Forms.Padding(2);
                    ItemName.Properties.ReadOnly = true;
                    ItemName.Size = new System.Drawing.Size(466, 28);
                    ItemName.StyleController = this.layoutControl1;
                    ItemName.TabIndex = GroupIndex + 3;
                    ItemName.Text = item["ItemName"].ToString();
                    this.layoutControl1.Controls.Add(ItemName);

                    LayoutControlItem NameLayout = new LayoutControlItem();
                    NameLayout.Name = "NameLayout" + GroupIndex.ToString();
                    NameLayout.Control = ItemName;
                    NameLayout.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
                    if (GroupIndex == 1)
                    {
                        NameLayout.Location = new System.Drawing.Point(66, 0);
                        NameLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
                        NameLayout.Size = new System.Drawing.Size(474, 56);
                        NameLayout.Text = "측정항목";
                        NameLayout.TextLocation = DevExpress.Utils.Locations.Top;
                        NameLayout.TextSize = new System.Drawing.Size(48, 17);
                    }
                    else
                    {
                        NameLayout.Location = new System.Drawing.Point(66, 56 + (36 * (GroupIndex - 1)));
                        NameLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
                        NameLayout.Size = new System.Drawing.Size(474, 36);
                        NameLayout.TextSize = new System.Drawing.Size(0, 0);
                        NameLayout.TextVisible = false;
                    }
                    layoutCollection[(GroupIndex - 1) * 3 + 1] = NameLayout;

                    DevExpress.XtraEditors.TextEdit DataValue = new DevExpress.XtraEditors.TextEdit();
                    DataValue.Location = new System.Drawing.Point(554, 34 + (36 * (GroupIndex - 1)));
                    DataValue.Name = "DataValue" + GroupIndex.ToString();
                    DataValue.Properties.Padding = new System.Windows.Forms.Padding(2);
                    DataValue.Size = new System.Drawing.Size(158, 28);
                    DataValue.StyleController = this.layoutControl1;
                    DataValue.TabIndex = GroupIndex + 3;
                    this.layoutControl1.Controls.Add(DataValue);

                    LayoutControlItem ValueLayout = new LayoutControlItem();
                    ValueLayout.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
                    ValueLayout.Control = DataValue;
                    ValueLayout.Name = "ValueLayout" + GroupIndex.ToString();
                    if (GroupIndex == 1)
                    {
                        ValueLayout.Location = new System.Drawing.Point(540, 0);
                        ValueLayout.MaxSize = new System.Drawing.Size(166, 56);
                        ValueLayout.MinSize = new System.Drawing.Size(166, 56);
                        ValueLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
                        ValueLayout.Size = new System.Drawing.Size(166, 56);
                        ValueLayout.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                        ValueLayout.Text = "측정결과";
                        ValueLayout.TextLocation = DevExpress.Utils.Locations.Top;
                        ValueLayout.TextSize = new System.Drawing.Size(48, 17);
                    }
                    else
                    {
                        ValueLayout.Location = new System.Drawing.Point(540, 56 + (36 * (GroupIndex - 1)));
                        ValueLayout.MaxSize = new System.Drawing.Size(166, 36);
                        ValueLayout.MinSize = new System.Drawing.Size(166, 36);
                        ValueLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
                        ValueLayout.Size = new System.Drawing.Size(166, 36);
                        ValueLayout.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                        ValueLayout.TextSize = new System.Drawing.Size(0, 0);
                        ValueLayout.TextVisible = false;
                    }
                    layoutCollection[(GroupIndex - 1) * 3 + 2] = ValueLayout;

                    GroupIndex++;
                }
            }

            this.Root.Items.AddRange(layoutCollection);
        }
    }
}
