using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Windows.Forms;
using SmartCommon.Common;
using System.Data;
using DevExpress.Xpo.DB.Helpers;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DevExpress.XtraBars.Docking2010;

namespace SmartMES.View.Lookup
{
	public partial class Frm_LookUpDBBackup : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";

        DataRow Dr = null;
        private bool _isDownloading = false;

        public Frm_LookUpDBBackup()
        {
            InitializeComponent();
            Set_Lup();

            lup_Corp.smtLookUpValue = WrGlobal.CorpID;
            dateTimeBack.smtDateTimeValue = DateTime.Now;
            txt_SaveName.smtTextBoxValue = WrGlobal.CorpID.Trim() + "_" + DateTime.Now.ToString("yyyyMMdd");

            txt_DBname.smtTextBoxValue = WrGlobal.DBName.Replace("_G", "");
            txt_Path.smtTextBoxValue = @"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\";
        }

        public Frm_LookUpDBBackup(DataRow dr)
        {
            //수정
            InitializeComponent();

            Set_Lup();

            Dr = dr; //수정행

            DateTime date = DateTime.Now;
            DateTime.TryParse(dr["BackUp_Date"].ConvertString_Null_To_Empty(), out date);

            lup_Corp.smtLookUpValue = WrGlobal.CorpID;
            dateTimeBack.smtDateTimeValue = date;
            txt_SaveName.smtTextBoxValue = dr["File_Nm"].ConvertString_Null_To_Empty();

            txt_DBname.smtTextBoxValue = WrGlobal.DBName.Replace("_G","");
            txt_Path.smtTextBoxValue = dr["File_Path"].ConvertString_Null_To_Empty();
        }

        private void Frm_LookUpDBBackup_Load(object sender, EventArgs e)
        {

        }
        private void Set_Lup()
        {
            lup_Corp.smtLookUpDataSource = code.smtLookUp("사업장", "", "");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void cmdSave_Click(object sender, EventArgs e)
        {
            if (_isDownloading)
            {
                MessageBox.Show("이미 진행 중인 작업이 있습니다.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_SaveName.smtTextBoxValue))
            {
                MessageBox.Show("백업파일명을 입력해 주십시오", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string backUpDate = dateTimeBack.smtDateTimeValue.ToString("yyyyMMdd");
            string saveName = txt_SaveName.smtTextBoxValue;
            string filePath = txt_Path.smtTextBoxValue;
            string dbName = txt_DBname.smtTextBoxValue;

            try
            {
                if (Dr == null)
                {
                    // 신규 등록일 때만 파일 다운로드 진행 (수정은 이력만 변경)
                    string serverFilePath = $"{filePath}{dbName}_{saveName}.bak";

                    using (var sfd = new SaveFileDialog())
                    {
                        sfd.FileName = $"{dbName}_{saveName}.bak";
                        sfd.Filter = "Backup files (*.bak)|*.bak";

                        if (sfd.ShowDialog() != DialogResult.OK) return;

                        string localSavePath = sfd.FileName;

                        bool success = await BackUpDB(dbName, serverFilePath, localSavePath);
                        if (!success) return;
                    }

                    InsertBackupHistory(backUpDate, saveName, filePath, dbName);
                    MessageBox.Show("백업이 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    UpdateBackupHistory(saveName, Dr["Idx"]);
                    MessageBox.Show("수정이 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertBackupHistory(string backUpDate, string saveName, string filePath, string dbName)
        {
            string query = $@"INSERT INTO SM_Backup (CorpId,
                                                 Idx,
                                                 BackUp_Date,
                                                 File_Nm,
                                                 File_Path, 
                                                 DB_Nm,
                                                 CreId,
                                                 CreDt
                                            )
                        VALUES('{WrGlobal.CorpID}',
                            (SELECT ISNULL(MAX(Idx), 0) + 1 FROM SM_Backup),
                               '{backUpDate}',
                               '{saveName}',
                               '{filePath}',
                               '{dbName}',
                               '{WrGlobal.LoginID}',
                               '{DateTime.Now.ToString("yy-MM-dd HH:mm:ss")}'
                                )";


            db.ExecuteQuery(query);
        }

        private void UpdateBackupHistory(string saveName, object idx)
        {
            string query = $@"
                        UPDATE SM_Backup
                        SET File_Nm = '{saveName}'
                        WHERE Idx = '{Dr["Idx"]}'";


            db.ExecuteQuery(query);
        }

        // 성공하면 true, 실패하면 false 반환 (호출부에서 이력 저장 여부 판단용)
        private async Task<bool> BackUpDB(string dbName, string serverFilePath, string localSavePath)
        {
            _isDownloading = true;
            btn_Save.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "백업 생성 중...";

            try
            {
                // 1. 서버에서 백업 실행
                await Task.Run(() => RunBackup(dbName, serverFilePath));

                // 2. 백업 파일 다운로드 (스트리밍)
                lblStatus.Text = "다운로드 중... 0MB";
                var progress = new Progress<long>(bytesDownloaded =>
                {
                    lblStatus.Text = $"다운로드 중... {bytesDownloaded / 1024 / 1024}MB";
                });

                await Task.Run(() => DownloadBackupViaQuery(serverFilePath, localSavePath, progress));

                lblStatus.Text = "완료";
                return true;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "오류 발생";
                MessageBox.Show($"백업/다운로드 중 오류가 발생했습니다.\n\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                _isDownloading = false;
                btn_Save.Enabled = true;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
            }
        }

        private void RunBackup(string dbName, string serverFilePath)
        {
            // dbName은 대괄호 이스케이프 처리 (식별자 인젝션 방지)
            string safeDbName = dbName.Replace("]", "]]");

            string sql = $@"
        BACKUP DATABASE [{safeDbName}]
        TO DISK = N'{serverFilePath}'
        WITH INIT, COMPRESSION, STATS = 10";

            db.ExecuteQuery(sql);
        }

        private void DownloadBackupViaQuery(string serverFilePath, string localSavePath, IProgress<long> progress)
        {
            string sql = @"
        SELECT BulkColumn AS Data
        FROM OPENROWSET(BULK @FilePath, SINGLE_BLOB) AS x";

            using (var conn = new SqlConnection(Database.m_ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandText = $@"
            SELECT BulkColumn AS Data
            FROM OPENROWSET(BULK N'{serverFilePath.Replace("'", "''")}', SINGLE_BLOB) AS x";

                conn.Open();
                cmd.CommandTimeout = 0;

                using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    if (!reader.Read())
                        throw new Exception("백업 파일을 읽어올 수 없습니다.");

                    using (var output = new FileStream(localSavePath, FileMode.Create, FileAccess.Write))
                    {
                        const int bufferSize = 65536; // 64KB
                        byte[] buffer = new byte[bufferSize];
                        long fieldOffset = 0;
                        long bytesRead;

                        while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, (int)bytesRead);
                            fieldOffset += bytesRead;
                            progress?.Report(fieldOffset);
                        }
                    }
                }
            }
        }
    }
}
