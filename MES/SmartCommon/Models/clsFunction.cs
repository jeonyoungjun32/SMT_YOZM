using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SmartCommon.Common
{
	public class clsFunction
	{
		Database db = new Database();
		string query = "";

		public void CheckButton(LayoutControl layoutControl, string MenuCode)
		{

			query = string.Format("select isnull(ADMINYN ,'N') as ADMINYN from permission mp Where mp.PType = 'Y' and mp.EMPL_NO = '{0}'  and mp.Class = '{1}' ", WrGlobal.LoginID, MenuCode);
			DataRowView drv = db.GetDataRecord(query);
			if (drv["ADMINYN"].ToString() == "Y")
				return;

			foreach (var item in layoutControl.Root.Items)
			{
				if (item.GetType().Name == "LayoutControlItem")
				{
					LayoutControlItem layout = (LayoutControlItem)item;
					//Control con = (Control)item;

					switch (layout.Control.GetType().Name)
					{
						case "GridControl":
							GridControl grid = (GridControl)(layout.Control);

							foreach (GridColumn col in ((DevExpress.XtraGrid.Views.Base.ColumnView)grid.MainView).Columns)
							{
								//if (col.ColumnEdit.GetType().Name == "ButtonEdit")
								//{
								//    col.Visible = false;
								//}
								if (col.Caption == "")
								{
									col.Visible = false;
								}

								break;

							}
							break;
						case "Panel":
							Panel panel = (Panel)(layout.Control);
							foreach (Control ctl in panel.Controls)
							{
								if (ctl.GetType().Name == "Button")
								{
									if (ctl.Text.Trim() == "닫기" || ctl.Text.Trim() == "조회")
										continue;
									ctl.Visible = false;
								}

							}
							break;
						default:
							break;
					}
				}
				else if (item.GetType().Name == "LayoutControlGroup")
				{
					LayoutControlGroup layoutGroup = (LayoutControlGroup)item;
					foreach (var gitem in layoutGroup.Items)
					{
						if (gitem.GetType().Name == "LayoutControlItem")
						{
							LayoutControlItem layout = (LayoutControlItem)gitem;
							switch (layout.Control.GetType().Name)
							{
								case "GridControl":
									GridControl grid = (GridControl)(layout.Control);

									foreach (GridColumn col in ((DevExpress.XtraGrid.Views.Base.ColumnView)grid.MainView).Columns)
									{
										if (col.Caption == "")
										{
											col.Visible = false;
										}
									}
									break;
								default:
									break;
							}
						}
					}
				}
				else if (item.GetType().Name == "TabbedControlGroup")
				{
					TabbedControlGroup layoutTapGroup = (TabbedControlGroup)item;
					foreach (var gitem in layoutTapGroup.TabPages)
					{
						if (gitem.GetType().Name == "LayoutControlGroup")
						{
							LayoutControlGroup taplayoutGroup = (LayoutControlGroup)gitem;
							foreach (var lgitem in taplayoutGroup.Items)
							{
								if (lgitem.GetType().Name == "LayoutControlItem")
								{
									LayoutControlItem layout = (LayoutControlItem)lgitem;
									switch (layout.Control.GetType().Name)
									{
										case "GridControl":
											GridControl grid = (GridControl)(layout.Control);

											foreach (GridColumn col in ((DevExpress.XtraGrid.Views.Base.ColumnView)grid.MainView).Columns)
											{
												if (col.Caption == "")
												{
													col.Visible = false;
												}
											}
											break;
										default:
											break;
									}
								}
							}
						}

					}
				}

			}
		}

		public int GetMaxSeq(GridView gv)
		{
			int seq = 0;
			for (int i = 0; i < gv.DataRowCount; i++)
			{
				int lastSeq = (int)gv.GetRowCellValue(i, "Seq");

				if (lastSeq > seq) seq = lastSeq;
			}

			return seq;

		}

		public void ExportExcel(GridView gv)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel Worksheets|*.Xls";

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				gv.ExportToXls(sfd.FileName);
				Process.Start(sfd.FileName);
			}//end fnction
		}

		public static string GetFileSize(long byteCount)
		{
			string size = "0 Bytes";
			if (byteCount >= 1073741824.0)
				size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
			else if (byteCount >= 1048576.0)
				size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
			else if (byteCount >= 1024.0)
				size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
			else if (byteCount > 0 && byteCount < 1024.0)
				size = byteCount.ToString() + " Bytes";

			return size;
		}

		public static string getString(object obj)
		{
			if (Convert.IsDBNull(obj))
			{
				return "";
			}
			else if (obj == null)
			{
				return "";
			}
			else
			{
				if (obj.GetType().Name == "DateTime")
				{
					DateTime dtime = (DateTime)obj;
					return dtime.ToShortDateString();
				}
				else
				{
					return obj.ToString();
				}

			}
		}

		public void DeleteCheckedRows(GridView gridView, BindingSource bindingSource)
		{
			gridView.BeginUpdate();
			bindingSource.SuspendBinding();

			try
			{
				// 선택된 Row 객체를 미리 리스트로 저장
				List<DataRowView> rowsToDelete = new List<DataRowView>();

				foreach (int rowHandle in gridView.GetSelectedRows())
				{
					var rowView = gridView.GetRow(rowHandle) as DataRowView;
					if (rowView != null)
					{
						rowsToDelete.Add(rowView);
					}
				}

				if (rowsToDelete.Count == 0)
				{
					return;
				}

				// 한꺼번에 삭제
				foreach (var rowView in rowsToDelete)
				{
					rowView.Delete();  // RemoveCurrent() 대신 Delete() 사용
				}

				bindingSource.EndEdit();  // 변경 반영
			}
			finally
			{
				bindingSource.ResumeBinding();
				gridView.EndUpdate();
			}
		}

		public void DeleteCheckedRows_Bulk(GridView gridView, BindingSource bindingSource)
		{
			gridView.BeginUpdate();

			try
			{
				// BindingSource의 DataTable 직접 가져오기
				var dataView = bindingSource.List as DataView;
				if (dataView == null) return;

				DataTable table = dataView.Table;

				// 선택된 RowHandles 기준으로 삭제 대상 Row 찾기
				List<DataRow> rowsToDelete = new List<DataRow>();

				foreach (int rowHandle in gridView.GetSelectedRows())
				{
					var rowView = gridView.GetRow(rowHandle) as DataRowView;
					if (rowView != null)
					{
						rowsToDelete.Add(rowView.Row);
					}
				}

				if (rowsToDelete.Count == 0)
					return;

				// UI와 이벤트 최소화를 위해 Bulk 처리
				table.BeginLoadData();
				foreach (var row in rowsToDelete)
				{
					row.Delete(); // RowState → Deleted
				}
				table.EndLoadData();

				bindingSource.EndEdit(); // 바인딩된 객체에 반영
			}
			finally
			{
				gridView.EndUpdate();
			}
		}


		/// <summary>
		/// 3초 후 자동 닫히는 메시지 박스 (XtraMessageBox 사용)
		/// </summary>
		public void ShowAutoCloseMessage(string text, int delay = 3000, string caption = "확인(자동종료)")
		{
			XtraMessageBoxArgs msg = new XtraMessageBoxArgs
			{
				Caption = caption,
				Text = text,
				AutoCloseOptions = { Delay = delay }
			};
			XtraMessageBox.Show(msg);
		}
	}
}
