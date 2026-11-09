using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace SmartMES.View
{
    public partial class WrSyncWork : WaitForm
    {
        public delegate void SendToCaller(DialogResult result, int code, string message);
        public SendToCaller OnSendToCaller;

        private struct WorkerParam
        {
            public DialogResult result { get; set; }
            public int code { get; set; }
            public string message { get; set; }
        }

        public WrSyncWork(string caption = "", string description = "")
        {
            InitializeComponent();

            if (caption.Length > 0)
            {
                this.SetCaption(caption);
            }

            if (description.Length > 0)
            {
                this.SetDescription(description);
            }

            this.progressPanel1.AutoHeight = true;
            this.ShowOnTopMode = ShowFormOnTopMode.AboveAll;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum WaitFormCommand
        {
        }

        public void runCommand(Action actnj)
        {
            BackgroundWorker worker = null;

            try
            {
                worker = new BackgroundWorker();
                worker.DoWork += (sender, e) => WorkerRunning(sender, e, actnj);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerComplete);
                worker.RunWorkerAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //if(worker != null)
                //{
                //    worker.Dispose();
                //    worker = null;
                //}
            }
        }
        void WorkerRunning(object sender, DoWorkEventArgs e, Action actnj)
        {
            WorkerParam param = new WorkerParam();
            param.result = DialogResult.OK;
            param.code = 0;
            param.message = string.Empty;

            try
            {

                actnj();
            }
            catch (Exception ex)
            {
                int code = System.Runtime.InteropServices.Marshal.GetExceptionCode();

                e.Cancel = true;
                param.result = DialogResult.Cancel;
                param.code = code;
                param.message = ex.ToString();
            }

            e.Result = param;
        }

        void WorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (OnSendToCaller != null)
                {
                    //if (!e.Cancelled)
                    //{
                    //    //정상적으로 처리가 된 경우
                    //}
                    WorkerParam param = (WorkerParam)e.Result;
                    OnSendToCaller(param.result, param.code, param.message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}