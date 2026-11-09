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
using DevExpress.XtraEditors.Controls;

namespace baseSMT
{
    public partial class smtBodyRadioGroup : DevExpress.XtraEditors.XtraUserControl
    {
        private bool _Must = false; // 필수입력
        private string _valueField = string.Empty;
        private string _displayField = string.Empty;
        private DataTable _dataSource = null;
        private Color _Color = Color.Transparent;

        // 라벨 타이틀
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

        [Category("2.smtRadioGroup")]
        public string smtRadioGroupDataValueMember
        {
            get { return _valueField; }
            set
            {
                _valueField = value;
                UpdateRadioItems();
            }
        }

        [Category("2.smtRadioGroup")]
        public string smtRadioGroupDataDisplayMember
        {
            get { return _displayField; }
            set
            {
                _displayField = value;
                UpdateRadioItems();
            }
        }

        [Category("2.smtRadioGroup")]
        public DataTable smtRadioGroupDataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;

                if (value != null && value.Columns.Count >= 1)
                {
                    if (string.IsNullOrEmpty(_valueField))
                        _valueField = value.Columns[0].ColumnName;

                    //DisplayField, ValueField 분리된 Radio는 따로 만들어야함

                    if (string.IsNullOrEmpty(_displayField))
                        _displayField = value.Columns[0].ColumnName;
                    
                    UpdateRadioItems();
                }
            }
        }

        [Category("2.smtRadioGroup")]
        public bool smtLookUpReadOnly
        {
            get { return radioValue.Properties.ReadOnly; }
            set
            {
                radioValue.Properties.ReadOnly = value;
                fnMustReadChanged();
            }
        }

        [Category("2.smtRadioGroup"), Description("필수입력")]
        public bool smtRadioGroupMust
        {
            get { return _Must; }
            set
            {
                _Must = value;
                fnMustReadChanged();
            }
        }

        [Category("2.smtRadioGroup"), Description("색상")]
        public Color smtRadioGroupColor
        {
            get { return _Color; }
            set
            {
                _Color = value;
                radioValue.BackColor = value;
            }
        }

        [Category("2.smtRadioGroup")]
        public string smtRadioGroupValue
        {
            get { return radioValue.EditValue == null ? "" : radioValue.EditValue.ToString(); }
            set { radioValue.EditValue = value.Trim() == "" ? null : value; }
        }

        [Category("2.smtRadioGroup")]
        public event EventHandler smtRadioGroupEditValueChanged;

        public smtBodyRadioGroup()
        {
            InitializeComponent();
            radioValue.EditValueChanged += new EventHandler(EditValueChanged);
            this.SizeChanged += new EventHandler(fnSizeChanged);
        }

        private void UpdateRadioItems()
        {
            if (_dataSource == null ||
                string.IsNullOrEmpty(_valueField) ||
                string.IsNullOrEmpty(_displayField))
                return;

            radioValue.Properties.Items.Clear();

            foreach (DataRow row in _dataSource.Rows)
            {
                RadioGroupItem item = new RadioGroupItem();
                item.Description = row[_displayField].ToString();
                item.Value = row[_valueField];
                radioValue.Properties.Items.Add(item);
            }
        }

        private void fnMustReadChanged()
        {
            if (smtRadioGroupMust)
            {
                radioValue.Properties.Appearance.BackColor = smtDesignSetting.MustColor;
            }
            else
            {
                radioValue.BackColor = Color.Transparent;
            }
        }

        private void EditValueChanged(object sender, EventArgs eventargs)
        {
            smtRadioGroupEditValueChanged?.Invoke(sender, eventargs);
        }

        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            this.Height = 32; // 기본 높이 설정
        }
    }
}