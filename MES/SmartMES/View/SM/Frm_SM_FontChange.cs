using DevExpress.XtraBars.Navigation;
using Microsoft.Win32;
using SmartCommon.Common;
using SmartMES.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_FontChange : DevExpress.XtraEditors.XtraForm
    {
        clsSystem sys = new clsSystem();

        public Frm_SM_FontChange()
        {
            InitializeComponent();
            Set_Design();
        }

        public Frm_SM_FontChange(Rectangle r)
        {
            InitializeComponent();

            this.Left = r.Left + (r.Width - this.Width) / 2;
            this.Top = r.Top + (r.Height - this.Height) / 4;
        }

        private void Set_Design()
        {
            //LoadFontList();
            LoadFontSettings();
        }

        private void LoadFontList()
        {
            string fontsFolder = Path.Combine(Application.StartupPath, "Fonts");
            if (Directory.Exists(fontsFolder))
            {
                string[] fontFiles = Directory.GetFiles(fontsFolder, "*.ttf");
                foreach (string fontFile in fontFiles)
                {
                    try
                    {
                        // 폰트 파일 추가
                        //privateFonts.AddFontFile(fontFile);

                        //// 로드한 폰트의 이름을 ComboBox에 추가
                        //foreach (FontFamily fontFamily in privateFonts.Families)
                        //{
                        //    fontComboBox.Items.Add(fontFamily.Name);
                        //}
                        string fontName = Path.GetFileNameWithoutExtension(fontFile);
                        Cb_Font.Properties.Items.Add(fontName);
                    }
                    catch (Exception ex)
                    {
                        // 오류 처리
                        MessageBox.Show($"폰트 로드 오류: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("폰트 폴더가 없습니다.");
            }

            foreach (System.Drawing.FontFamily font in System.Drawing.FontFamily.Families)
            {
                Cb_Font.Properties.Items.Add(font.Name);
            }
        }

        private void LoadFontSettings()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

            if (reg == null)
            {
                return;
            }

            string font = Convert.ToString(reg.GetValue("Font"));
            string size = Convert.ToString(reg.GetValue("Size"));

            Cb_Font.EditValue = font;
            Txt_Size.EditValue = size;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string font = Cb_Font.EditValue.ConvertString_Null_To_Empty();
            float size = Txt_Size.EditValue.ConvertFloat_Null_To_Zero() == 0 ? 10 : Txt_Size.EditValue.ConvertFloat_Null_To_Zero();

            if (font == "")
            {
                MessageBox.Show("폰트를 선택하십시오.");
                return;
            }

            if (size == 0)
            {
                MessageBox.Show("사이즈를 입력하십시오.");
                return;
            }

            Font newFont = new Font(font, size, FontStyle.Bold);
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = newFont;
            Set_TileNavePane_Font();
            //UserLookAndFeel.Default.UseDefaultLookAndFeel = false;

            SaveFontSettings();

            this.Close();
        }

        private void Set_TileNavePane_Font()
        {
            string font = Cb_Font.EditValue.ConvertString_Null_To_Empty();
            float size = Txt_Size.EditValue.ConvertFloat_Null_To_Zero() == 0 ? 10 : Txt_Size.EditValue.ConvertFloat_Null_To_Zero();

            Font newFont = new Font(font, size, FontStyle.Bold);

            foreach (Form frm in Application.OpenForms)
            {
                ApplyFontToTileNavPaneButtons(frm, newFont);
            }
        }

        private static void ApplyFontToTileNavPaneButtons(Control parent, Font font)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TileNavPane tileNavPane)
                {
                    tileNavPane.Appearance.Font = font;
                    tileNavPane.AppearanceHovered.Font = font;
                    tileNavPane.AppearanceSelected.Font = font;
                }
            }
        }

        private void SaveFontSettings()
        {
            string font = Cb_Font.EditValue.ConvertString_Null_To_Empty();
            float size = Txt_Size.EditValue.ConvertFloat_Null_To_Zero() == 0 ? 10 : Txt_Size.EditValue.ConvertFloat_Null_To_Zero();

            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

            reg.SetValue("Font", font);
            reg.SetValue("Size", size);
        }
    }
}