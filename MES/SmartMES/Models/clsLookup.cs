using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models
{
    class cls_Lookup
    {

        #region Lookup -> ComboBox 형태

        /// <summary>
        /// LookUpEidt ComboBox 형태
        /// </summary>
        /// <param name="lup">ComboBox형태로 바꿀 LookUpEdit</param>
        /// <param name="dt">입력할 DataTable</param>
        /// <param name="str_Value">Value로 가질 Table Column명</param>
        /// <param name="str_Disp">화면에 표기할 Table Column명</param>
        internal void SetLup_Combo(LookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
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

        /// <summary>
        /// Repository LookupEdit ComboBox 형태로 생성
        /// </summary>
        /// <param name="dt">입력할 DataTable</param>
        /// <param name="str_Value">Value로 가질 Table Column명</param>
        /// <param name="str_Disp">화면에 표기할 Table Column명</param>
        internal RepositoryItemLookUpEdit Create_RepositoryItem(DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            RepositoryItemLookUpEdit rpoLup = new RepositoryItemLookUpEdit();
            rpoLup.DataSource = dt;     //공통코드 리스트
            rpoLup.ValueMember = str_Value;             //실제값
            rpoLup.DisplayMember = str_Disp;            //표시값

            rpoLup.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
            rpoLup.PopulateColumns();                   //Datasource 적용(안해주면 visible 안먹힘)
            rpoLup.Columns[str_Value].Visible = false;  //코드 숨김
            rpoLup.ShowHeader = false;                  //Column Header 숨김

            return rpoLup;
        }

        #endregion Lookup -> ComboBox 형태 End

        #region RepositorySearchLookUp 형태
        internal void Create_RepositoryItemSearchLookUp(SearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            RepositoryItemSearchLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;               //실제값
            lupData.DisplayMember = str_Disp;             //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
        }

        internal void Create_RepositoryItemSearchLookUp(smtSearchLookUp lup, DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            lup.smtLookUpDataSource = dt;     //공통코드 리스트
            lup.smtLookUpDataValueMember = str_Value;               //실제값
            lup.smtLookUpDataDisplayMember = str_Disp;             //표시값

            //lup.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
        }

        internal void Create_ItemAdd_RepositoryItemSearchLookUp(SearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            RepositoryItemSearchLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;               //실제값
            lupData.DisplayMember = str_Disp;             //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정

            lupData.ShowAddNewButton = true;
            lupData.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            lupData.View.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            lupData.View.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;

            lupData.PopulateViewColumns();

            lupData.View.OptionsEditForm.EditFormColumnCount = 1;
            lupData.View.OptionsEditForm.PopupEditFormWidth = 400;
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

        internal RepositoryItemSearchLookUpEdit Create_ItemAdd_RepositoryItemSearchLookUp(DataTable dt, string str_Value, string str_Disp)
        {
            //Repository SearchLookUpEdit 생성
            RepositoryItemSearchLookUpEdit rpoLup = new RepositoryItemSearchLookUpEdit();
            rpoLup.DataSource = dt;             //공통코드 리스트
            rpoLup.ValueMember = str_Value;     //실제값
            rpoLup.DisplayMember = str_Disp;    //표시값

            rpoLup.NullText = "";   //빈값 그냥 빈칸으로 나오도록 수정

            rpoLup.ShowAddNewButton = true;
            rpoLup.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            rpoLup.View.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            rpoLup.View.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;

            rpoLup.PopulateViewColumns();

            rpoLup.View.OptionsEditForm.EditFormColumnCount = 1;
            rpoLup.View.OptionsEditForm.PopupEditFormWidth = 400;

            return rpoLup;
        }
        #endregion RepositorySearchLookUp 형태 END
    }
}
