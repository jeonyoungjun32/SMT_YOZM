using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace SmartCommon.Common
{
    class clsLookup
    {
        /// <summary>
        /// LookUpEidt ComboBox 형태
        /// </summary>
        /// <param name="lup">ComboBox형태로 바꿀 LookUpEdit</param>
        /// <param name="dt">입력할 DataTable</param>
        /// <param name="str_Value">Value로 가질 Table Column명</param>
        /// <param name="str_Disp">화면에 표기할 Table Column명</param>
        internal void SetLup_Combo(LookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            //Lookup -> ComboBox 형태

            //Repository LookUpEdit 생성
            RepositoryItemLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;             //실제값
            lupData.DisplayMember = str_Disp;            //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
            lupData.ForceInitialize();                   //Datasource 적용(안해주면 visible 안먹힘)
            lupData.PopulateColumns();                   //Datasource 적용(안해주면 visible 안먹힘)
            lupData.Columns[str_Value].Visible = false;  //코드 숨김
            lupData.ShowHeader = false;                  //Column Header 숨김
        }

        internal void Create_RepositoryItemSearchLookUp(SearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            RepositoryItemSearchLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;               //실제값
            lupData.DisplayMember = str_Disp;             //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
        }

        internal void Create_RepositoryItemSearchLookUp(RepositoryItemSearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            RepositoryItemSearchLookUpEdit lupData = lup;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;               //실제값
            lupData.DisplayMember = str_Disp;             //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
        }

        internal RepositoryItemSearchLookUpEdit Create_RepositoryItemSearchLookUp(DataTable dt, string str_Value, string str_Disp)
        {
            //Repository SearchLookUpEdit 생성
            RepositoryItemSearchLookUpEdit rpoLup = new RepositoryItemSearchLookUpEdit();
            rpoLup.DataSource = dt;             //공통코드 리스트
            rpoLup.ValueMember = str_Value;     //실제값
            rpoLup.DisplayMember = str_Disp;    //표시값

            rpoLup.NullText = "";   //빈값 그냥 빈칸으로 나오도록 수정

            return rpoLup;
        }

        internal void Create_RepositoryItemSearchLookUpDefault(SearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            RepositoryItemSearchLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;                        //공통코드 리스트
            lupData.ValueMember = str_Value;                //코드값
            lupData.DisplayMember = str_Disp;               //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    lup.EditValue = dt.Rows[0][str_Value].ToString();
                }
            }
        }

        /// <summary>
        /// SearchLookUpEdit에 공백 무시 검색 기능을 적용합니다.
        /// </summary>
        /// <param name="searchLookUp">적용할 SearchLookUpEdit 컨트롤</param>
        /// <param name="searchFieldName">공백을 무시하고 검색할 기준 컬럼명 (예: "ItemName")</param>
        public void SetIgnoreSpaceSearch(SearchLookUpEdit searchLookUp, string searchFieldName)
        {
            if (searchLookUp == null) return;

            // SearchLookUpEdit의 팝업 뷰(GridView) 가져오기
            if (searchLookUp.Properties.PopupView is GridView popupGridView)
            {
                // 기존에 혹시나 연결되어 있을지 모를 이벤트를 제거 후 새로 연결 (중복 등록 방지)
                popupGridView.CustomRowFilter -= PopupGridView_CustomRowFilter;

                // 검색 컬럼 정보를 뷰의 Tag에 임시 저장하여 이벤트 메서드에서 활용할 수 있도록 합니다.
                popupGridView.Tag = searchFieldName;

                // 이벤트 연결
                popupGridView.CustomRowFilter += PopupGridView_CustomRowFilter;
            }
        }

        private static void PopupGridView_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            // Tag에 저장해둔 기준 컬럼명을 가져옴
            string searchFieldName = view.Tag as string;
            if (string.IsNullOrEmpty(searchFieldName)) return;

            // 파인드 패널 입력값 가져오기
            string filterText = view.FindFilterText;

            // 검색어가 없으면 기본 필터 로직 적용
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return;
            }

            // 1. 사용자가 입력한 검색어의 공백 제거 및 대문자화
            string cleanSearchText = filterText.Replace(" ", "").ToUpper();

            // 2. 셋팅된 지정 컬럼의 cell 값 가져오기
            object cellValue = view.GetListSourceRowCellValue(e.ListSourceRow, searchFieldName);

            if (cellValue != null)
            {
                // 3. DB 데이터의 공백 제거 및 대문자화
                string cleanDbText = cellValue.ToString().Replace(" ", "").ToUpper();

                // 4. 포함 여부 검사
                if (cleanDbText.Contains(cleanSearchText))
                {
                    e.Visible = true;   // 조건 충족 시 표시
                    e.Handled = true;   // 기본 필터링 무시
                }
                else
                {
                    e.Visible = false;  // 미충족 시 숨김
                    e.Handled = true;
                }
            }
        }
    }
}
