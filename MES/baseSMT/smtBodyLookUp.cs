using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
using DevExpress.XtraEditors;

namespace baseSMT
{
    public partial class smtBodyLookUp : DevExpress.XtraEditors.XtraUserControl
    {
        private int _DefaultSelectIndex = -1;

        private bool _Must = false; //필수입력

        //라벨 타이틀
        [Category("1.smtLabel")]
        public bool smtLabelTitleVisible
        {
            get { return lbl_Title.Visible; }
            set { lbl_Title.Visible = value; }
        }
        [Category("1.smtLabel")]
        public string smtLabelTitleValue
        {
            get { return lbl_Title.Text; }
            set { lbl_Title.Text = value; }
        }
        [Category("1.smtLabel")]
        public int smtLabelTitleWidth
        {
            get { return lbl_Title.Width; }
            set { lbl_Title.Width = value; }
        }

        //룩업박스
        [Category("2.smtLookUp")]
        public string smtLookUpValue
        {
            get { return lup_Value.EditValue == null ? "" : lup_Value.EditValue.ToString(); }
            set { lup_Value.EditValue = value.Trim() == "" ? null : value; }
        }
        [Category("2.smtLookUp")]
        public string smtLookUpText
        {
            get { return lup_Value.Text == null ? "" : lup_Value.Text; }
            set { lup_Value.Text = value.Trim();}
        }
        [Category("2.smtLookUp"),Browsable(false)]
        public int smtLookUpSelectdIndex
        {
            get
            {
                DataTable dt = smtLookUpDataSource;
                if (dt != null && dt.Rows.Count > 0 && dt.Columns.Contains(lup_Value.Properties.ValueMember) && lup_Value.EditValue != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][lup_Value.Properties.ValueMember] == lup_Value.EditValue) return i;
                    }
                    return -1;
                }
                else return -1;
            }
            set
            {
                DataTable dt = smtLookUpDataSource;
                if (dt != null && dt.Rows.Count > value && dt.Columns.Contains(lup_Value.Properties.ValueMember))
                {
                    lup_Value.EditValue = dt.Rows[value][lup_Value.Properties.ValueMember];
                }
            }
        }
        [Category("2.smtLookUp")]
        public DevExpress.Utils.HorzAlignment smtLookUpAlignment
        {
            get { return lup_Value.Properties.Appearance.TextOptions.HAlignment; }
            set { lup_Value.Properties.Appearance.TextOptions.HAlignment = value; }
        }
        [Category("2.smtLookUp")]
        public string smtLookUpDataValueMember
        {
            get { return lup_Value.Properties.ValueMember; }
            set{lup_Value.Properties.ValueMember = value;}
        }
        [Category("2.smtLookUp")]
        public string smtLookUpDataDisplayMember
        {
            get { return lup_Value.Properties.DisplayMember; }
            set { lup_Value.Properties.DisplayMember = value; }
        }
        [Category("2.smtLookUp")]
        public DataTable smtLookUpDataSource
        {
            get { return (DataTable)lup_Value.Properties.DataSource; }
            set
            {
                lup_Value.Properties.DataSource = value;
                DataTable dt = value;

                //멤버 지정않할 시 자동멤버
                if (dt != null && dt.Columns.Count > 1)
                {
                    if(this.smtLookUpDataValueMember == "") this.smtLookUpDataValueMember = dt.Columns[0].ColumnName;
                    if (this.smtLookUpDataDisplayMember == "") this.smtLookUpDataDisplayMember = dt.Columns[1].ColumnName;
                }

                //지정 데이터가 있을시
                if (lup_Value.EditValue != null && lup_Value.EditValue.ToString() != "")
                {

                }
                //디폴트 선택이 있으면
                else if (_DefaultSelectIndex >= 0 && dt != null && dt.Rows.Count > _DefaultSelectIndex && dt.Columns.Contains(lup_Value.Properties.ValueMember))
                {
                    lup_Value.EditValue = dt.Rows[_DefaultSelectIndex][lup_Value.Properties.ValueMember];
                }
            }
        }
        [Category("2.smtLookUp")]
        public bool smtLookUpReadOnly
        {
            get { return lup_Value.Properties.ReadOnly; }
            set 
            { 
                lup_Value.Properties.ReadOnly = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtLookUp"), Description("필수입력")]
        public bool smtLookUpMust
        {
            get { return _Must; }
            set 
            { 
                _Must = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtLookUp")]
        [Editor("System.Drawing.Design.UITypeEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ParenthesizePropertyName(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ControlBindingsCollection smtLookUpDataDataBindings
        {
            get { return lup_Value.DataBindings; }
        }
        [Category("2.smtLookUp")]
        public event EventHandler smtLookUpEditValueChanged;

        public smtBodyLookUp()
        {
            InitializeComponent();
            lup_Value.EditValueChanged += new EventHandler(EditValueChanged);
            this.SizeChanged += new EventHandler(fnSizeChanged);
        }
        private void fnMustReadChanged()
        {
            if (smtLookUpMust)
            {
                lup_Value.Properties.Appearance.BackColor = smtDesignSetting.MustColor;
            }
            else
            {
                if (smtLookUpReadOnly) lup_Value.Properties.Appearance.BackColor = smtDesignSetting.ReadOnlyColor;
                else lup_Value.Properties.Appearance.BackColor = System.Drawing.Color.White;
            }
        }
        private void EditValueChanged(object sender, EventArgs eventargs)
        {
            if (smtLookUpEditValueChanged != null)
            {
                smtLookUpEditValueChanged.Invoke(sender, eventargs);
            }
        }
        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            this.Height = 32;
        }
    }
}
