using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using baseSMT;

namespace SmartCommon.Common
{
	public class clsTree
	{
        #region 디자인 관련

        /// <summary>
        /// TreeList Column 생성
        /// </summary>
        /// <param name="gv">입력할 TreeList</param>
        /// <param name="caption">표기명칭</param>
        /// <param name="fieldName">연동데이터 Column명칭</param>
        /// <param name="repository">Repository Item(ComboBox, Lookup 등)</param>
        public void Create_TreeColummn(TreeList tree, string caption, string fieldName, RepositoryItem rpo = null)
        {
            TreeListColumn col = new TreeListColumn();
            col.Caption = caption;          //표기명
            col.FieldName = fieldName;      //연동 데이터 Column명
            col.ColumnEdit = rpo;    //Repository Item(ComboBox, Lookup 등)
            col.Visible = true;             //화면에 보이기

            col.OptionsColumn.AllowEdit = false;   //유저가 수정 허용여부

            tree.Columns.Add(col);            //GridView에 추가
        }

        /// <summary>
        /// TreeList 변경 허용
        /// </summary>
        /// <param name="col">적용할 Column</param>
        public void TreeColumn_AllowTrue(TreeListColumn col, bool edit = true)
        {
            col.OptionsColumn.AllowEdit = edit;
            if (edit)
            {
                col.AppearanceCell.BackColor = smtDesignSetting.MustColor;  //Column 배경색
            }
            else
            {
                col.AppearanceCell.BackColor = smtDesignSetting.ReadOnlyColor;  //Column 배경색
            }
        }

        #endregion 디자인 관련 End

        #region Event 관련

        /// <summary>
        /// 셀값 입력시 중복값 검사(ValidatingEditor Event용)
        /// </summary>
        /// <param name="tree">검사 할 TreeList</param>
        /// <param name="e">ValidatingEditor의 EventArgs</param>
        /// <param name="fieldName">검사 할 Column FieldName</param>
        public void Check_OverLap_CellValue(TreeList tree, BaseContainerValidateEditorEventArgs e, string fieldName)
        {
            if (tree.FocusedColumn.FieldName == fieldName)
            {
                //입력된 데이터값과 일치하는 Data수 검사
                if (Convert.ToString(tree.GetFocusedRowCellValue(tree.Columns["Status"])) == "신규")
                {
                    int cnt = (tree.DataSource as DataTable).Select($" {fieldName} = '{Convert.ToString(e.Value)}'").Count();

                    //1개 이상이면 입력 불가
                    if (cnt > 0)
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = $"중복된 {tree.FocusedColumn.Caption}이(가) 있습니다.";  //경고 알림
                    }
                }
                else
                {
                    e.Valid = false;
                    e.ErrorText = $"저장된 {tree.FocusedColumn.Caption}은/는 수정할 수 없습니다.";
                }
            }
        }

        /// <summary>
        /// 셀값 수정 시 상태값 변경(CellValueChanged Event용) / 가장 좌측 Column이 상태Column이여야함
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="nodeId"></param>
        public void Change_Status(TreeList tree, int nodeId)
        {
            //셀값 수정 시 상태값 수정
            DataRow dr = tree.GetDataRow(nodeId);
            string str_Status = Convert.ToString(dr["Status"]);    //상태 값

            if (str_Status != "신규" &&
                str_Status != "수정" &&
                str_Status != "삭제")
            {
                dr["Status"] = "수정";
            }
        }

        /// <summary>
        /// Form닫을때 저장내역 검사
        /// </summary>
        /// <param name="gv">입력할 GridView</param>
        /// <param name="fieldName">상태값 Column FieldName</param>
        /// <returns></returns>
        public bool Check_Gv_FormClosing(TreeList tree, string fieldName)
        {
            //true : 종료방지, false : 정상종료
            DataTable dt = tree.DataSource as DataTable;
            bool writeFlag = false;

            //1. 저장하지 않은 내역있는지 확인
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr[fieldName]) == "신규" ||
                   Convert.ToString(dr[fieldName]) == "수정" ||
                   Convert.ToString(dr[fieldName]) == "삭제")
                {
                    writeFlag = true;
                    break;
                }
            }

            //2. 사용자 선택
            if (writeFlag)
            {
                if (MessageBox.Show("저장하지 않은 내역이 있습니다.\n종료 하시겠습니까?", "Check_Save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion Event 관련 End
    }
}
