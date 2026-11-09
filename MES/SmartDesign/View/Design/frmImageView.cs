using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDesign.View.Design
{
    public partial class frmImageView : DevExpress.XtraEditors.XtraForm
    {
        public frmImageView()
        {
            InitializeComponent();
        }

        public frmImageView(Image pic)
        {
            InitializeComponent();
            pictureEdit1.Image = pic;
        }
    }
}