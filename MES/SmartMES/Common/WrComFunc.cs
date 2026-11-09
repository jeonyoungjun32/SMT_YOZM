using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.Common
{
    public class WrComFunc
    {

        #region "CLASS"

        public DataView GetCLASS(string corpId)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = string.Format(" Select * FROM CM_CLASS Where CorpID = '{0}' ", corpId);
                view = sqlDb.GetDataView("Class", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function

        public DataView GetCLASS(string corpId, string classCode, string parentClassCode, string classLevel)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = string.Format("select CLASS, CLASSNAME FROM CM_CLASS Where CorpId ='{0}' And CLASS  like '{1}%' AND HIGHCLASS = '{2}' AND CLASSLEVEL = '{3}'", corpId, classCode, parentClassCode, classLevel);

                view = sqlDb.GetDataView("Class", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function

        public void GetClassCodeSearchLookup(RepositoryItemSearchLookUpEdit sender, string HIGHCLASS)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select CLASS 코드 , CLASSNAME 명칭 FROM CM_CLASS WHERE HIGHCLASS = '" + HIGHCLASS + "' AND CLASSLEVEL = 1 ";

                sender.DataSource = sqlDb.GetDataView("CLASS", query);
                sender.ValueMember = "코드";
                sender.DisplayMember = "명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }

        #endregion

        #region CM_CateItem 관련 검색
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="query"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        public void GetCM_CateItem(Object sender, string query = "", string valueMember = "", string displayMember = "")
        {
            Database sqlDb = null;

            try
            {
                sqlDb = new Database();

                if (sender.GetType().Equals(typeof(SearchLookUpEdit)))
                {
                    ((SearchLookUpEdit)sender).Properties.DataSource = sqlDb.GetDataView("CM_CateItem", query);
                    ((SearchLookUpEdit)sender).Properties.ValueMember = valueMember.Trim();
                    ((SearchLookUpEdit)sender).Properties.DisplayMember = displayMember.Trim();
                }
                else if (sender.GetType().Equals(typeof(RepositoryItemSearchLookUpEdit)))
                {
                    ((RepositoryItemSearchLookUpEdit)sender).DataSource = sqlDb.GetDataView("CM_CateItem", query);
                    ((RepositoryItemSearchLookUpEdit)sender).ValueMember = valueMember.Trim();
                    ((RepositoryItemSearchLookUpEdit)sender).DisplayMember = displayMember.Trim();
                }//end if
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cateId1"></param>
        /// <param name="cateId2"></param>
        /// <param name="cateId3"></param>
        /// <param name="itemCode"></param>
        public void GetCM_CateItem(SearchLookUpEdit sender, string corpId, string classId)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = string.Format(" Select ItemCode as 코드, Title as 명칭" +
                        " FROM CM_CateItem " +
                        " Where corpId = '{0}' And class = '{1}' ", corpId, classId);

                sender.Properties.DataSource = sqlDb.GetDataView("CM_CateItem", query);
                sender.Properties.ValueMember = "코드";
                sender.Properties.DisplayMember = "명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function
         //

        public void GetCM_CateItem(RepositoryItemSearchLookUpEdit sender, string corpId, string classId)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = string.Format(" Select ItemCode as 코드, Title as 명칭" +
                        " FROM CM_CateItem " +
                        " Where corpId = '{0}' And class = '{1}' ", corpId, classId);

                sender.DataSource = sqlDb.GetDataView("CM_CateItem", query);
                sender.ValueMember = "코드";
                sender.DisplayMember = "명칭";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        //public void GetCM_CateItemName(SearchLookUpEdit sender, string cateId1 = "", string cateId2 = "", string cateId3 = "", string itemCode = "")
        //{
        //    Database sqlDb = null;
        //    string query = string.Empty;

        //    try
        //    {
        //        sqlDb = new Database();
        //        query = " Select Title as 명칭" +
        //                " FROM CM_CateItem " +
        //                " Where CateId1 = '" + cateId1 + "' ";

        //        if (cateId2.Length > 0)
        //        {
        //            query += " And CateId2 = '" + cateId2 + "'";
        //        }//end if

        //        if (cateId3.Length > 0)
        //        {
        //            query += " And CateId3 = '" + cateId3 + "'";
        //        }//end if

        //        if (itemCode.Length > 0)
        //        {
        //            query += " And ItemCode in ('" + itemCode + "')";
        //        }//end if

        //        sender.Properties.DataSource = sqlDb.GetDataView("CM_CateItem", query);
        //        sender.Properties.ValueMember = "명칭";
        //        sender.Properties.DisplayMember = "명칭";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        sqlDb.DBClose();
        //        sqlDb = null;
        //    }//end try
        //}//end function


        public void GetCM_CateItemWithdAll(SearchLookUpEdit sender, string cateId1, string cateId2)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select '' as 코드, '전체' as 명칭" +
                        " Union ALL " +
                        " Select ItemCode as 코드, Title as 명칭" +
                        " FROM CM_CateItem " +
                        " Where CateId1 = '" + cateId1 + "' " +
                              " And CateId2 = '" + cateId2 + "'";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_CateItem", query);
                sender.Properties.ValueMember = "코드";
                sender.Properties.DisplayMember = "명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCM_CateItemWithGol(SearchLookUpEdit sender, string cateId1 = "", string cateId2 = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select ItemCode as 코드, Title as 명칭, ExtraValue1 as 이미지" +
                        " FROM CM_CateItem " +
                        " Where CateId1 = '" + cateId1 + "' " +
                              " And CateId2 = '" + cateId2 + "'";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_CateItem", query);
                sender.Properties.ValueMember = "코드";
                sender.Properties.DisplayMember = "명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cateId1"></param>
        /// <param name="cateId2"></param>
        /// <param name="cateId3"></param>
        /// <param name="itemCode"></param>
        public void GetCM_CateItem(RepositoryItemSearchLookUpEdit sender, string classId)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select ItemCode as 코드, Title as 명칭" +
                        " FROM CM_CateItem " +
                        " Where Class = '" + classId + "' ";

                sender.DataSource = sqlDb.GetDataView("CM_CateItem", query);
                sender.ValueMember = "코드";
                sender.DisplayMember = "명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetWareHouse(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " select LOC_CD as 코드 , LOC_NM as 코드명 from V_WareHouse ";

                sender.DataSource = sqlDb.GetDataView("WareHouse", query);
                sender.ValueMember = "코드";
                sender.DisplayMember = "코드명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        #endregion


        #region cmAuthRole
        public void GetCmAuthRole(SearchLookUpEdit sender, string userId = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select UserId, ModuleId " +
                        " From CmAuthRole " + 
                        " Where UserId = '" + userId.Trim() + "'";
 

                sender.Properties.DataSource = sqlDb.GetDataView("CM_CateItem", query);
                sender.Properties.ValueMember = "UserId";
                sender.Properties.DisplayMember = "ModuleId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function
        #endregion

        #region 운송비 단가 등록

        public void GetCMLogisticsUnitPrice(SearchLookUpEdit sender, string transType)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select Location as 지역코드, LocationName as 지역명, Area as 면적, Price as 단가, ApplyDate as 적용일자" +
                        " From CmLogisticsUnitPrice " +
                        " Where TransType ='" + transType + "'";


                sender.Properties.DataSource = sqlDb.GetDataView("CmLogisticsUnitPrice", query);
                sender.Properties.ValueMember = "지역코드";
                sender.Properties.DisplayMember = "지역명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCMLogisticsUnitPrice(RepositoryItemSearchLookUpEdit sender, string transType)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select TransType as 운송방법, Location as 지역코드, LocationName as 지역명, Area as 면적, Price as 단가" +
                        " From CmLogisticsUnitPrice ";
                        // " Where TransType ='" + transType + "'";

                sender.DataSource = sqlDb.GetDataView("CmLogisticsUnitPrice", query);
                sender.ValueMember = "지역코드";
                sender.DisplayMember = "지역명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        #endregion


        #region 사용자

        public DataView GetCmUser( string corpId, string userID , string retiredYN = "")
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = string.Format(" Select EmpNo as 사번, Name as 이름 From SM_User Where CorpID = '{0}' and EmpNo = '{1}'  ", corpId, userID);

                view = sqlDb.GetDataView("CmUser", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;

        }//end function

        public void GetCmUser(SearchLookUpEdit sender, string userID = "", string retiredYN = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = string.Format(" Select EmpNo as 사번, Name as 이름 From SM_User Where CorpID = '{0}' ", WrGlobal.CorpID);

                sender.Properties.DataSource = sqlDb.GetDataView("CmUser", query);
                sender.Properties.ValueMember = "사번";
                sender.Properties.DisplayMember = "이름";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }//end function

        public void GetCmUser(RepositoryItemSearchLookUpEdit sender, string userID = "", string retiredYN = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select UserID as 사번, UserName as 이름" +
                        " From CmUser ";


                if (userID.Length > 0 && retiredYN.Length > 0)
                {
                    query += " Where UserId='" + userID.Trim() + "'0" +
                             " And RetiredYn='" + retiredYN + "'";
                }
                else if (userID.Length > 0 && retiredYN.Length == 0)
                {
                    query += " Where UserId='" + userID.Trim() + "'0";
                }
                else if (userID.Length > 0 && retiredYN.Length > 0)
                {
                    query += " Where RetiredYn='" + retiredYN + "'0";
                }//end if

                sender.DataSource = sqlDb.GetDataView("CmUser", query);
                sender.ValueMember = "사번";
                sender.DisplayMember = "이름";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }//end function



        #endregion

        #region 부서코드
        public void GetCmCodeDept(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select DeptCode as 부서코드, DeptTitle as 부서명칭 " +
                        " From CmCodeDept";


                sender.Properties.DataSource = sqlDb.GetDataView("CmCodeDept", query);
                sender.Properties.ValueMember = "부서코드";
                sender.Properties.DisplayMember = "부서명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }//end function

        public void GetCmCodeDept(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select DeptCode as 부서코드, DeptTitle as 부서명칭 " +
                        " From CmCodeDept";

                sender.DataSource = sqlDb.GetDataView("CmCodeDept", query);
                sender.ValueMember = "부서코드";
                sender.DisplayMember = "부서명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }//end function

        #endregion

        #region 거래처
        public DataView GetRecentlyUsedCM_Cust()
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = " select cc.CustCode, cc.CustName, isnull(cci.Title, '') as Title, isnull(cm.UserName, '') as UserName,  isnull(cp.corpName, '')  as corpName " + 
                        " from CM_Cust cc " + 
                        " left join CM_CateItem cci on cci.CateId1 = '06' and cci.CateId2 ='06' And cci.ClassItemCode = cc.OrderType "+
                        " left join CmUser cm on cm.UserId = cc.SalesMan " + 
                        " left join SM_Corp cp on cp.CorpId = CC.CorpId " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate " + 
                              " And cc.recentlyUsedYN='Y'";

                view = sqlDb.GetDataView("CM_Cust", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function

        public DataView GetCM_CustForOrderReg(string custCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " Select cm.CustCode, cm.CustName, cm.SalesMan, cu.UserName, cm.CorpId, cm.OrderType, cm.VatType " +
                        " From CM_Cust cm " +
                        " Left Join CmUser cu on cu.UserId = cm.SalesMan " +
                        " Where cm.CustCode='" + custCode + "' And getDate() between cm.OpenDate and cm.CloseDate ";

                view = sqlDb.GetDataView("CM_Cust", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;

        }//end function

        public DataView GetCM_Cust(string custCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " Select *  " +
                        " From CM_Cust " +
                        " Where CustCode ='" + custCode + "' " ;

                view = sqlDb.GetDataView("CM_Cust", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function

        public void GetCM_Cust(SearchLookUpEdit sender, string custType= "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                             " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                        " From CM_Cust cc " +
                        " Left join CM_CateItem cci on cci.CorpId = cc.CorpId and cc.CustType = cci.ClassItemCode and cci.Class = 'A020' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate";

                if (custType.Length > 0)
                {
                    query += string.Format(" And cc.CustType in ({0}) ", custType); // 거래처 구분
                }//end if

                sender.Properties.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.Properties.ValueMember = "거래처코드";
                sender.Properties.DisplayMember = "거래처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCMShipToByCM_Cust(SearchLookUpEdit sender, string corpId, string custCode )
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = string.Format(" Select cc.CustCode as 납품처코드, cc.CustName as 납품처명, cc.CustNo  as 사업자번호,  cc.PresiName as 대표자명 " +
                        " From CM_Cust cc " +
                        " inner Join CmShipTo cs on cs.CorpId = cc.CorpId and cs.CustCode = cc.CustCode " +
                        " Where cc.CorpId = '{0}' and cc.CustCode = '{1}' and getDate() between cc.OpenDate and cc.CloseDate ", corpId, custCode);


                sender.Properties.DataSource = sqlDb.GetDataView("CmShipTo", query);
                sender.Properties.ValueMember = "납품처코드";
                sender.Properties.DisplayMember = "납품처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCMShipToByCM_Cust(RepositoryItemSearchLookUpEdit sender, string corpId, string custCode)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = string.Format(" Select cc.CustCode as 납품처코드, cc.CustName as 납품처명, cc.CustNo  as 사업자번호,  cc.PresiName as 대표자명 " +
                        " From CM_Cust cc " +
                        " inner Join CmShipTo cs on cs.CorpId = cc.CorpId and cs.CustCode = cc.CustCode " +
                        " Where cc.CorpId = '{0}' and cc.CustCode = '{1}' and getDate() between cc.OpenDate and cc.CloseDate ", corpId, custCode);

                sender.DataSource = sqlDb.GetDataView("CmShipTo", query);
                sender.View.OptionsView.ColumnAutoWidth = false;
                sender.View.BestFitColumns(true);
                sender.ValueMember = "납품처코드";
                sender.DisplayMember = "납품처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function



        public void GetCM_CustForPlanMaster(SearchLookUpEdit sender, string custType = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                             " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태, cu.UserName as 영업사원 " +
                        " From CM_Cust cc " +
                        " Left Join CmUser cu on cu.UserId = cc.SalesMan" + 
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate";

                if (custType.Length > 0)
                {
                    query += " And cc.CustType in ('20', '30') "; // 거래처 구분
                }//end if

                sender.Properties.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.Properties.ValueMember = "거래처코드";
                sender.Properties.DisplayMember = "거래처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCM_CustForCode(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                             " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                        " From CM_Cust cc " +
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate";


                sender.Properties.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.Properties.ValueMember = "거래처코드";
                sender.Properties.DisplayMember = "거래처코드";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCM_Cust(RepositoryItemSearchLookUpEdit sender, string custType= "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                               " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                        " From CM_Cust cc " +
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate";

                if (custType.Length > 0)
                {
                    query += " And cc.CustType in (" + custType + ") "; // 거래처 구분
                }//end if

                sender.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.ValueMember = "거래처코드";
                sender.DisplayMember = "거래처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCM_CustByBom(RepositoryItemSearchLookUpEdit sender, string custType = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                               " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                        " From CM_Cust cc " +
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate" +
                                    " And cc.CustType in ('10', '30') ";


                sender.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.ValueMember = "거래처코드";
                sender.DisplayMember = "거래처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCM_CustByMeterial(RepositoryItemSearchLookUpEdit sender, string custType = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                               " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                        " From CM_Cust cc " +
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate" ;


                sender.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.ValueMember = "거래처코드";
                sender.DisplayMember = "거래처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function



        public void GetCM_CustForCode(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                               " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                        " From CM_Cust cc " +
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate";

                sender.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.ValueMember = "거래처코드";
                sender.DisplayMember = "거래처코드";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCM_CustByCustType(RepositoryItemSearchLookUpEdit sender, string custType)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
               query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                               " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                        " From CM_Cust cc " +
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate" +
                                    " And cc.CustType in (" + custType + ") ";



                sender.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.ValueMember = "거래처코드";
                sender.DisplayMember = "거래처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCM_CustByCustType(SearchLookUpEdit sender, string custType)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " +
                              " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태 " +
                       " From CM_Cust cc " +
                       " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                       " Where getDate() between cc.OpenDate and cc.CloseDate" +
                                   " And cc.CustType in (" + custType + ") ";


                sender.Properties.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.Properties.ValueMember = "거래처코드";
                sender.Properties.DisplayMember = "거래처코드";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        /// <summary>
        /// 원자자 투입에서 발주처에 대한 원지,원단 마닐라의 거래처에 등록된 거래처만 처리
        /// </summary>
        /// <param name="sender"></param>
        public void GetCM_CustForBom(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cc.CustCode as 거래처코드, cc.CustName as 거래처명, cc.CustNo  as 사업자번호, " + 
                        " cc.PresiName as 대표자명, isnull(cci.Title, '') as 거래형태" + 
                        " From CM_Cust cc " + 
                        " Left Join CM_CateItem cci on cci.CateId1='06' and cci.CateId2 = '03' and cci.ClassItemCode = cc.CustType " +
                        " Where getDate() between cc.OpenDate and cc.CloseDate" + 
	                          " And exists(Select 'XXX' From CmPurPrice where CustCode = cc.CustCode)";

                sender.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.ValueMember = "거래처코드";
                sender.DisplayMember = "거래처명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public string GetOrderType(string custCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            string orderType = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select OrderType " +
                        " From CM_Cust " +
                        " Where CustCode ='" + custCode + "'";

                DataView view = sqlDb.GetDataView("CM_Cust", query);
                foreach (DataRowView item in view)
                {
                    orderType = item["OrderType"].ToString();
                }//end 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return orderType;
        }//end function

        /// <summary>
        /// 사용자가 영업사원인지 검사한다.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool isSalesman(string userid)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            bool blRtv = false;
            try
            {
                sqlDb = new Database();

                query = string.Format(" Select IsSalesMan from CmUser " +
                                      " Where UserId = '{0}'", userid);

                view = sqlDb.GetDataView("CM_Cust", query);
                foreach (DataRowView item in view)
                {
                    if (item["IsSalesMan"].ToString().Trim().Equals("Y"))
                    {
                        blRtv = true;
                    }//end if
                }//end foreach
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;
        }//end function

        public void GetSalesman(SearchLookUpEdit sender)
        {
         
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select '' as 사원코드 , '전체' as 사원명 " +
                        " Union All " +
                        " Select cu.SalesMan as 사원코드, cmu.UserName as 사원명" + 
                        " From CmMeterial cm " + 
                        " Inner join CM_Cust cu on cu.CustCode = cm.CustCode " + 
                        " Left join CmUser cmu on cmu.UserId = cu.SalesMan " +
                        " where cm.EISYN = 'Y' " + 
	                          " And cu.SalesMan is not null " + 
                        " Group by cu.SalesMan, cmu.UserName";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.Properties.ValueMember = "사원코드";
                sender.Properties.DisplayMember = "사원명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }//end function

        #endregion

        #region 제품코드 CmMeterial

        /// <summary>
        /// 제품코드에 대한 일반자료 반환
        /// </summary>
        /// <param name="sender"></param>
        public void GetAllCmMeterial(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = $@" Select ItemCode as 제품코드, ItemName as 제품명
                    From CM_Item";
                sender.DataSource = sqlDb.GetDataView("CM_Item", query);
                sender.View.OptionsView.ColumnAutoWidth = true;
                sender.View.BestFitColumns(true);
                sender.ValueMember = "제품코드";
                sender.DisplayMember = "제품명";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCmMeterialByGroupId(RepositoryItemSearchLookUpEdit sender, string corpid, string groupId)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = string.Format(" Select ItemCode as 제품코드, ItemName as 제품명" +
                                      " From CM_Item" +
                                      " Where CorpId = '{0}' And GroupID in ({1})  And  DisplayYn = 'Y'", corpid, groupId);

                sender.DataSource = sqlDb.GetDataView("CM_Item", query);
                sender.View.OptionsView.ColumnAutoWidth = true;
                sender.View.BestFitColumns(true);
                sender.ValueMember = "제품코드";
                sender.DisplayMember = "제품명";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        /// <summary>
        /// 제품코드에 대한 일반자료 반환
        /// </summary>
        /// <param name="sender"></param>
        public void GetAllCmMeterial(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select MeterialCode as 제품코드, MeterialName as 제품명, Lenght as 장, Width as 폭, Height as 고, " +
                            " MeterialGroup as 제품품목, PurUnit as 단위, StdEaPrice 표준단가, MeterialType as 제품구분" +
                         " From CmMeterial" +
                         " Where GETDATE() between StartDate and EndDate ";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_Cust", query);
                sender.Properties.ValueMember = "제품코드";
                sender.Properties.DisplayMember = "제품명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCmMeterialByCustomer(RepositoryItemSearchLookUpEdit sender, string custCode, String applyDate)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = " Select cust.CustCode, cust.CustName, isnull(material.MeterialClass1, '') as MeterialClass1, material.MeterialCode, cust.SalesMan, material.MeterialName, sdPrice.Price, material.MeterialType, material.SalesUnitCode,  sdPrice.ApplyDate " +
                        " From CM_Cust cust " +
                        " Inner Join CmSdPrice sdPrice on sdPrice.CustCode = cust.CustCode  And '" + applyDate + "' between sdPrice.ApplyDate and sdPrice.endDate " +
                        " Inner Join CmMeterial material on material.MeterialCode = sdPrice.MeterialCode " +
                        " Where cust.CustCode = '" + custCode + "' ";

                sender.DataSource = sqlDb.GetDataView("CmMeterial", query);
                sender.ValueMember = "MeterialCode";
                sender.DisplayMember = "MeterialName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCmMeterial(RepositoryItemSearchLookUpEdit sender, string custCode, String applyDate)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = " Select cust.CustCode, cust.CustName, isnull(material.MeterialClass1, '') as MeterialClass1, material.MeterialCode, cust.SalesMan, material.MeterialName, sdPrice.Price, material.MeterialType, material.SalesUnitCode,  sdPrice.ApplyDate " +
                        " From CM_Cust cust " +
                        " Inner Join CmSdPrice sdPrice on sdPrice.CustCode = cust.CustCode  And '" + applyDate + "' between sdPrice.ApplyDate and sdPrice.endDate " +
                        " Inner Join CmMeterial material on material.MeterialCode = sdPrice.MeterialCode " +
                        " Where cust.CustCode = '" + custCode + "' And GETDATE() between cust.StartDate and cust.EndDate ";

                sender.DataSource = sqlDb.GetDataView("CmMeterial", query);
                sender.ValueMember = "제품코드";
                sender.DisplayMember = "제품명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCmMeterial(SearchLookUpEdit sender, string custCode, String applyDate)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = " Select cust.CustCode, cust.CustName, isnull(material.MeterialClass1, '') as MeterialClass1, material.MeterialCode, cust.SalesMan, material.MeterialName, sdPrice.Price, material.MeterialType, material.SalesUnitCode,  sdPrice.ApplyDate " +
                        " From CM_Cust cust " +
                        " Inner Join CmSdPrice sdPrice on sdPrice.CustCode = cust.CustCode  And '" + applyDate + "' between sdPrice.ApplyDate and sdPrice.endDate " +
                        " Inner Join CmMeterial material on material.MeterialCode = sdPrice.MeterialCode " +
                        " Where cust.CustCode = '" + custCode + "' And GETDATE() between cust.StartDate and cust.EndDate ";


                sender.Properties.DataSource = sqlDb.GetDataView("CmMeterial", query);
                sender.Properties.ValueMember = "제품코드";
                sender.Properties.DisplayMember = "제품명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCmMeterial(RepositoryItemSearchLookUpEdit sender,  String applyDate)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cm.MeterialCode as 제품코드, cm.MeterialName as 제품명 " +
                        " From CmMeterial cm  " +
                        " Inner join CM_CateItem cci1 on cci1.ItemCode = meterialgroup and  cci1.CateId1 = '06' and cci1.cateid2 = '01' " +
                        " Inner join CM_CateItem cci2 on cci2.ItemCode = ObtainType and  cci2.CateId1 = '06' and cci2.cateid2 = '01' " +
                        " Inner Join CmSdPrice sdp on sdp.MeterialCode = cm.MeterialCode and  cast('" + applyDate + "' as datetime) between sdp.ApplyDate and sdp.EndDate " +
                        " Where cm.meterialgroup in ('10', '20')  " +
                                " And  GETDATE() between cm.StartDate and cm.EndDate";

                sender.DataSource = sqlDb.GetDataView("CmMeterial", query);
                sender.ValueMember = "제품코드";
                sender.DisplayMember = "제품명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        // 거래처에 연결된 제품을 가져온다.
        //public DataView GetCmMeterial(string custCode, String applyDate)
        //{
        //    Database sqlDb = null;
        //    string query = string.Empty;
        //    DataView view = null;
        //    try
        //    {
        //        sqlDb = new Database();

        //        query = " Select cm.MeterialCode " + 
        //                " From CmMeterial cm  " + 
        //                " Inner join CM_CateItem cci1 on cci1.ItemCode = meterialgroup and  cci1.CateId1 = '06' and cci1.cateid2 = '01' " + 
        //                " Inner join CM_CateItem cci2 on cci2.ItemCode = ObtainType and  cci2.CateId1 = '06' and cci2.cateid2 = '01' " +
        //                " Inner Join CmSdPrice sdp on sdp.MeterialCode = cm.MeterialCode and  cast('" + applyDate + "' as datetime) between sdp.ApplyDate and sdp.EndDate " +
        //                " Where cm.meterialgroup in ('10', '20')  " +
        //                      " And sdp.CustCode = '" + custCode + "' And cm.setCode = '10' And GETDATE() between cm.StartDate and cm.EndDate";

        //        //query = " Select MeterialCode " +
        //        //        " From CmSdPrice " +
        //        //        " Where CustCode = '" + custCode + "'" +
        //        //            " And  cast('" + applyDate + "' as datetime) between ApplyDate and EndDate";

        //        view = sqlDb.GetDataView("CmMeterial", query);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        sqlDb.DBClose();
        //        sqlDb = null;
        //    }//end try

        //    return view;
        //}//end function

        public DataView GetCmMeterial(string corpId, string meterialCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = String.Format(" Select * from CmMeterial " +
                        " Where CorpId ='{0}' and MeterialCode='{1}'", corpId, meterialCode);

                view = sqlDb.GetDataView("CmMeterial", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function

        public DataView CM_ItemImage(string corpId, string meterialCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = String.Format(" Select * from CM_ItemImage " +
                        " Where CorpId ='{0}' and ItemCode='{1}'", corpId, meterialCode);

                view = sqlDb.GetDataView("CmMeterial", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function


        public void GetCmMeterialGol(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = " Select ItemCode as ItemCode, Title  as ItemTitle " + 
                        " FROM CM_CateItem where CateId1 = '07' and cateid2 = '08' " + 
                        " union all" + 
                        " Select StdGM2Price as ItemCode, cast(StdGM2Price as varchar(20))  as ItemTitle" +
                        " From CmMeterial where MeterialGroup = '50'" + 
                        " Group by StdGM2Price";


                sender.DataSource = sqlDb.GetDataView("CmMeterial", query);
                sender.ValueMember = "ItemCode";
                sender.DisplayMember = "ItemTitle";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCmMeterialSalesMan(SearchLookUpEdit sender, DateTime fromDate, DateTime toDate, string status, string corpId)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = " select '' as 사원코드, '전체' as 사원명 " +
                        " union all " +
                        " Select sm.SalesMan as 사원코드, sm.SalesManName as 사원명" +
                        " From BM_SdOrderM sm " +
                        " Inner Join BM_SdOrderD sd on sd.SdOrderNo = sm.SdOrderNo  " +
                        " Where sm.ObtainDate between '" + fromDate.ToString("yyyy-MM-dd") + "' and '" + toDate.ToString("yyyy-MM-dd") + "' " +
                            " And sd.State = '" + status + "' ";
                if (corpId.Length > 0)
                {
                    query += " And Sm.CorpId = '" + corpId + "' "; 
                }//endif

                query += " Group by sm.SalesMan, sm.SalesManName ";


                sender.Properties.DataSource = sqlDb.GetDataView("BM_SdOrderM", query);
                sender.Properties.ValueMember = "사원코드";
                sender.Properties.DisplayMember = "사원명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetSalesManForReadOnly(SearchLookUpEdit sender, string userId)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = string.Format(" Select  UserId as 사원코드 , UserName as 사원명" +
                                      " From CmUser " +
                                      " Where UserId = '{0}'", userId);


                sender.Properties.DataSource = sqlDb.GetDataView("CmUser", query);
                sender.Properties.ValueMember = "사원코드";
                sender.Properties.DisplayMember = "사원명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public DataView GetObtainOrderStatusList(string fromDate, string toDate, string status, string corpid, string salesMan)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

               query =  "Select T.SoldToCode, T.CustName, T.SalesMan, T.SalesManName, T.CustNo " +
				    " From ( " + 
					" Select '' as SoldToCode, '전체' as CustName, '' as SalesMan, '' as SalesManName, '' as CustNo " +
					" union all " +
					" Select sm.SoldToCode, cc.CustName, sm.SalesMan, sm.SalesManName, cc.CustNo " +
					" From BM_SdOrderM sm " +
					" Inner Join CM_Cust cc on cc.CustCode = sm.SoldToCode " +
					" Where sm.ObtainDate between '" + fromDate + "' and '" + toDate + "'" +
							" And sm.State = '" + status + "'" +
							" And Sm.CorpId = '" + corpid + "'" ;
 
	            if(salesMan.Length > 0){
                    query = query + " And Sm.SalesMan = '" + salesMan + "'"; 
                }//end if

                query = query + " Group By sm.SoldToCode, cc.CustName, sm.SalesMan, sm.SalesManName, cc.CustNo " +
                                ") T " +
                                " Order By T.SoldToCode, T.CustName, T.SalesMan, T.SalesManName, T.CustNo ";

                view = sqlDb.GetDataView("CmMeterial", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function

        /// <summary>
        /// 거래처에 해당하는 최대 제품 코드 번호를 구한다.
        /// </summary>
        /// <param name="custCode"></param>
        /// <returns></returns>
        public string GetPrefixMaxMeterialNumber(string custCode)
        {
            string maxMeterialNumber = string.Empty;

            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                //query = string.Format(" Select Max(substring(MeterialCode, 1, 6)) as meterialCode " +
                //                      " From CmMeterial " +
                //                      " Where CustCode = '{0}'", custCode);
                //제품코드를 생성할때 거래처 코드가 아니라 제품코드에서 따서 처리 2015-12-07
                query = string.Format(" Select Max(substring(MeterialCode, 1, 6)) as meterialCode " +
                                     " From CmMeterial " +
                                     " Where SUBSTRING(meterialCode, 1, 3) = '{0}'", custCode.Substring(0, 3));

                view = sqlDb.GetDataView("CmMeterial", query);
                foreach (DataRowView item in view)
                {
                    maxMeterialNumber = item["meterialCode"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return maxMeterialNumber;

        }//end function

        public string GetPostfixMaxMeterialNumber(string meterialCode, string custCode)
        {
            string maxMeterialNumber = string.Empty;

            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = string.Format(" Select Max(MeterialCode) as meterialCode " +
                                      " From CmMeterial " +
                                      "  Where SUBSTRING(MeterialCode,1, 6) = '{0}' And CustCode = '{1}'", meterialCode.Substring(0,6), 
                                                                                                           custCode);

                view = sqlDb.GetDataView("CmMeterial", query);
                foreach (DataRowView item in view)
                {
                    maxMeterialNumber = item["meterialCode"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return maxMeterialNumber;

        }//end function

        public string GetMaxMeterialNumber(string custCode)
        {
            string maxMeterialNumber = string.Empty;

            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = string.Format(" Select Max(MeterialCode) as meterialCode " +
                                      " From CmMeterial " +
                                      "  Where CustCode = '{0}'", custCode);

                view = sqlDb.GetDataView("CmMeterial", query);
                foreach (DataRowView item in view)
                {
                    maxMeterialNumber = item["meterialCode"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return maxMeterialNumber;

        }//end function

        public string makeNextMeterialCode(string meterialCode)
        {
            string NextMeterialNumber = string.Empty;
            //1. 신규입력을 하는 경우 거래처 코드를 기반으로 코드를 자동 채번한다.
            //string custCode = SlCM_Cust.EditValue.ToString();
            if (meterialCode != null && meterialCode.Length > 0)
            {
                string prefix = string.Empty;
                string prefixAZ = string.Empty;
                int currentNumber = 0;
                int intMeterialNumber = 0;

                //2. 거래처에 해당하는 최대 제품코드 값을 구한다.
                string maxMeterialNumber = GetPrefixMaxMeterialNumber(meterialCode);
                if (maxMeterialNumber.Length > 0)
                {   // 기존 제품코드 구조
                    // 0069990 -> + 1 => 006A010 
                    // 거래처 코드 (3자리) + A ~ Z (1자리) + 010 ~ 990 (3자리) + 부품목코드(1자리 ~ ) 
                    // 기존에 자료가 있는 경우
                    prefix = maxMeterialNumber.Substring(0, 3);
                    prefixAZ = maxMeterialNumber.Substring(3, 1);
                    currentNumber = Convert.ToInt32(maxMeterialNumber.Substring(4, 2));
                    if (currentNumber >= 99)
                    {
                        //prefixAZ = WrIntFunc.ToHex(WrIntFunc.FromHex(prefixAZ) + 1);
                        prefixAZ = "0";
                        intMeterialNumber = 1;
                    }
                    else
                    {
                        intMeterialNumber = currentNumber + 1;
                    }//end if
                }
                else
                {
                    // 기존에 자료가 등록되어 있지 않은 경우
                    prefix = meterialCode.Substring(0, 3);
                    prefixAZ = "0";
                    intMeterialNumber = 1;
                }//end if

                NextMeterialNumber = prefix + prefixAZ + String.Format("{0:00}", intMeterialNumber) + "0";

            }//end if

            return NextMeterialNumber;

        }//end function

        /// <summary>
        /// 제품이력 요약 처리
        /// </summary>
        /// <param name="meterialCode"></param>
        /// <returns></returns>
        public DataView GetMeterialHistorySummary(string meterialCode)
        {
            string maxMeterialNumber = string.Empty;

            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = string.Format(" Select cm.Lenght + '*' + cm.Width + '*' + cm.Height as CustSize, cu.UserName,  " + 
		                                  "  cm40.MeterialName as cm40MeterialName, cb40.Lenght +'*'+ cb40.Width as cm40Size, " + 
		                                  "  cm50.MeterialName as cm50Meterialname, cb50.Lenght +'*'+ cb50.Width as cm50Size, " + 
		                                  "  cbp.WorkQty as sobuQty , cci.Title as sobuTitle, " +		// 소부 
		                                  "  cbpPrint.WorkQty as printQty, cciPrint.Title as printTitle " + // 인쇄
                                       " from CmMeterial cm " +
                                       " Left Join CmUser cu on cu.UserId = cm.CreId " +
                                       " Left Join CM_BOM cb40 on cb40.MeterialCode = cm.MeterialCode and cb40.MeterialGroup = '40' " +
                                       " Left Join CmMeterial cm40 on cm40.MeterialCode = cb40.BomMeterialNo " + 
                                       " Left Join CM_BOM cb50 on cb50.MeterialCode = cm.MeterialCode and cb50.MeterialGroup = '50' " +
                                       " Left Join CmMeterial cm50 on cm50.MeterialCode = cb50.BomMeterialNo " + 
                                       " Left Join CM_BOMProcess cbp on cbp.MeterialCode = cm.MeterialCode and cbp.ProcessCode = '010' " + 
                                       " Left Join CM_CateItem cci on cci.CateId1='07' and cci.CateId2='06' and cci.ClassItemCode = cbp.WorkUnit " +
                                       " Left Join CM_BOMProcess cbpPrint on cbpPrint.MeterialCode = cm.MeterialCode and cbpPrint.ProcessCode = '100' " +
                                       " Left Join CM_CateItem cciPrint on cciPrint.CateId1='07' and cciPrint.CateId2='06' and cciPrint.ItemCode = cbpPrint.WorkUnit " +
                                       " where cm.MeterialCode='{0}'", meterialCode);

                view = sqlDb.GetDataView("CmMeterial", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;

        }//end function

        public DataView GetMeterialHistoryBomProcess(string meterialCode)
        {
            string maxMeterialNumber = string.Empty;

            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = string.Format(" Select cbp.ProcessCode, cbp.WorkQty, cci.Title " +
                                      "  from CM_BOMProcess cbp " +
                                      "  Left Join CM_CateItem cci on cci.CateId1='07' and cci.CateId2='06' and cci.ClassItemCode = cbp.WorkUnit " +
                                      "  where MeterialCode ='{0}'", meterialCode);

                view = sqlDb.GetDataView("CmMeterial", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;

        }//end function


        public DataView GetMeterialLotCapacity(string corpId, string meterialCode)
        {
            string maxMeterialNumber = string.Empty;

            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = string.Format(" Select * " +
                                      " From CM_ItemLotCapacity " +
                                      " Where CorpId ='{0}' and  ItemCode ='{1}' And getdate() between StartDate and EndDate ", corpId, meterialCode);

                view = sqlDb.GetDataView("CM_ItemLotCapacity", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;

        }//end function

        /// <summary>
        /// 자동문서번호를 받는다.
        /// </summary>
        /// <param name="pGroup"></param>
        /// <param name="pDate"></param>
        /// <returns></returns>

        #endregion

        #region "Lot 관리"
        public DataView GetOrderLot(string corpId, string bomNo)
        {
            string maxMeterialNumber = string.Empty;

            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = string.Format(" Select * " +
                                      " From OrderLot " +
                                      " Where CorpId ='{0}' and  LinkNo ='{1}'", corpId, bomNo);

                view = sqlDb.GetDataView("OrderLot", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;

        }//end function
        #endregion

        #region  세트코드

        public DataView GetCmSetDetail(string meterialCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " Select MeterialCode, MeterialName, Height, Lenght, Width " +
                        " From CmSetDetail " + 
                        " Where SetCode = '" + meterialCode + "' " +
                              " And MeterialCode != '" + meterialCode + "' " +
                              " And SetType = '20'";

                view = sqlDb.GetDataView("CmSetDetail", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
            return view;

        }//end function
        #endregion


        #region 제품카테고리

        public void GetCmMeteterialGroup(SearchLookUpEdit sender, string meterialGroup, int level )
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select ItemCode as 분류코드, ItemTitle as 분류명칭 " + 
                        " From CM_ItemGroup " +
                        " Where MaterialGroup = '" + meterialGroup + "' " +  
                              " And NodeLevel = " + level;


                sender.Properties.DataSource = sqlDb.GetDataView("CM_ItemGroup", query);
                sender.Properties.ValueMember = "분류코드";
                sender.Properties.DisplayMember = "분류명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetCmMeteterialGroup(RepositoryItemSearchLookUpEdit sender, string meterialGroup, int level)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select ItemCode as 분류코드, ItemTitle as 분류명칭 " +
                        " From CM_ItemGroup " +
                        " Where MaterialGroup = '" + meterialGroup + "' " +
                             " And NodeLevel = " + level;


                sender.DataSource = sqlDb.GetDataView("CM_ItemGroup", query);
                sender.ValueMember = "분류코드";
                sender.DisplayMember = "분류명칭";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        #endregion

        #region 소속사업장
        public void GetCorpID(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select CorpId as '사업장코드', CorpName as '사업장명' " +
                        " from SM_Corp ";
                       //  " Where MasterYn = 'N' ";


                sender.DataSource = sqlDb.GetDataView("SM_Corp", query);
                sender.ValueMember = "사업장코드";
                sender.DisplayMember = "사업장명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCorpID(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select '' as '사업장코드', '전체' as '사업장명' " +
                        " Union All " +
                        " Select CorpId as '사업장코드', CorpName as '사업장명' " +
                        " From SM_Corp  ";
                        //" Where MasterYn = 'N' ";

                sender.Properties.DataSource = sqlDb.GetDataView("SM_Corp", query);
                sender.Properties.ValueMember = "사업장코드";
                sender.Properties.DisplayMember = "사업장명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function
        #endregion

        #region 제품그룹
        public void GetMeterialGroup(RepositoryItemSearchLookUpEdit sender, string meterialGroup)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select ItemCode as '제품코드', ItemTitle as '제품명' " +
                        " From CM_ItemGroup " +
                        " Where MaterialGroup = '" + meterialGroup + "' ";


                sender.DataSource = sqlDb.GetDataView("CM_ItemGroup", query);
                sender.ValueMember = "제품코드";
                sender.DisplayMember = "제품명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetMeterialGroup(SearchLookUpEdit sender, string meterialGroup)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select ItemCode as '제품코드', ItemTitle as '제품명' " +
                         " From CM_ItemGroup " +
                         " Where MaterialGroup = '" + meterialGroup + "' ";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_ItemGroup", query);
                sender.Properties.ValueMember = "제품코드";
                sender.Properties.DisplayMember = "제품명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public void GetMeterialGroup(RepositoryItemSearchLookUpEdit sender, int Level)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {

                sqlDb = new Database();
                query = " Select ItemCode as '그룹코드', ItemTitle as '그룹명' " +
                        " From CM_ItemGroup " +
                        " Where NodeLevel = " + Level;


                sender.DataSource = sqlDb.GetDataView("CM_ItemGroup", query);
                sender.ValueMember = "그룹코드";
                sender.DisplayMember = "그룹명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetMeterialGroup(SearchLookUpEdit sender, int Level, string groupCode = "")
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {

                sqlDb = new Database();

                query = " Select ItemCode as 그룹코드, ItemTitle as 그룹명 " +
                         " From CM_ItemGroup " +
                         " Where NodeLevel = " + Level;
                if (groupCode.Length > 0)
                {
                    query += " And MaterialGroup ='" + groupCode + "'";
                }//end if

                sender.Properties.DataSource = sqlDb.GetDataView("CM_ItemGroup", query);
                sender.Properties.ValueMember = "그룹코드";
                sender.Properties.DisplayMember = "그룹명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        #endregion

        #region ComCorp

        public void GetSM_Corp(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select CorpId as '사업장코드', CorpName as '사업장명' " +
                        " From SM_Corp";

                sender.DataSource = sqlDb.GetDataView("SM_Corp", query);
                sender.ValueMember = "사업장코드";
                sender.DisplayMember = "사업장명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetSM_Corp(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select CorpId as '사업장코드', CorpName as '사업장명' " +
                        " From SM_Corp";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_ItemGroup", query);
                sender.Properties.ValueMember = "제품코드";
                sender.Properties.DisplayMember = "사업장명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        #endregion

        #region CM_Warehouse
        public void GetCM_Warehouse(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select WareId as '창고코드', WareDesc as '창고명' " +
                        " From CM_Warehouse" +
                        " Where WareState = '10' ";

                sender.DataSource = sqlDb.GetDataView("CM_Warehouse", query);
                sender.ValueMember = "창고코드";
                sender.DisplayMember = "창고명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCM_Warehouse(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select WareId as '창고코드', WareDesc as '창고명' " +
                       " From CM_Warehouse" +
                       " Where WareState = '10' ";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_Warehouse", query);
                sender.Properties.ValueMember = "창고코드";
                sender.Properties.DisplayMember = "창고명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function
        #endregion

        #region
        /// <summary>
        /// CmSdPrice 테이블에서 특정 회사에 소속된 제품만 검색한다.
        /// </summary>
        /// <param name="custCode"></param>
        /// <returns></returns>
        public DataView  GetMaterialFromCmSdPrice(string custCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " Select MeterialCode " +
                        " From CmMeterial " + 
                        " Where CustCode = '" + custCode + "'" +
                        " Group by MeterialCode ";

                view = sqlDb.GetDataView("CM_Cust", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
            return view;

        }//end function

        public DataView GetMaterialFromCmPurPrice(string custCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " Select MeterialCode " +
                        " From CmPurPrice " +
                        " Where CustCode = '" + custCode + "'" +
                        " Group by MeterialCode ";

                view = sqlDb.GetDataView("CM_Cust", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
            return view;

        }//end function

        #endregion


        #region 공정코드

        public DataView GetCM_Process(string corpId, string processCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = String.Format(" Select * From CM_Process Where CorpId = '{0}' and ProcessCode = '{1}'", corpId, processCode);
                view = sqlDb.GetDataView("CM_Process", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function

        public void GetCM_ProcessSearchLookup(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cmp.ProcessCode , cmp.ProcessName, cm1.Title as WorkUnitTitle, cm2.Title as QtyUnitTitle, cm3.Title as WorkTypeTitle, cmp.WorkUnit, cmp.QtyUnit, cmp.WorkType, cmp.StdPrice " +
                        " From CM_Process cmp " +
                        " Left join CM_CateItem cm1 on cm1.CateId1 = '07' and cm1.CateId2 = '06' and cm1.ItemCode = cmp.WorkUnit " +
                        " Left join CM_CateItem cm2 on cm2.CateId1 = '07' and cm2.CateId2 = '07' and cm2.ItemCode = cmp.QtyUnit " +
                        " Left join CM_CateItem cm3 on cm3.CateId1 = '07' and cm3.CateId2 = '03' and cm3.ItemCode = cmp.WorkType ";

                sender.DataSource = sqlDb.GetDataView("CM_Process", query);
                sender.ValueMember = "ProcessCode";
                sender.DisplayMember = "ProcessName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function

        public DataView GetCM_ProcessSearchLookup()
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null ;
            try
            {
                sqlDb = new Database();
                query = " Select cmp.ProcessCode , cmp.ProcessName, cm1.Title as WorkUnitTitle, cm2.Title as QtyUnitTitle, cm3.Title as WorkTypeTitle, cmp.WorkUnit, cmp.QtyUnit, cmp.WorkType, cmp.StdPrice " +
                        " From CM_Process cmp " +
                        " Left join CM_CateItem cm1 on cm1.CateId1 = '07' and cm1.CateId2 = '06' and cm1.ItemCode = cmp.WorkUnit " +
                        " Left join CM_CateItem cm2 on cm2.CateId1 = '07' and cm2.CateId2 = '07' and cm2.ItemCode = cmp.QtyUnit " +
                        " Left join CM_CateItem cm3 on cm3.CateId1 = '07' and cm3.CateId2 = '03' and cm3.ItemCode = cmp.WorkType " +
                        " Where cmp.WorkType <> '10'";

                view = sqlDb.GetDataView("CM_Process", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function



        public void GetCM_ProcessSearchLookup(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cmp.ProcessCode , cmp.ProcessName, cm1.Title as WorkUnitTitle, cm2.Title as QtyUnitTitle, cm3.Title as WorkTypeTitle, cmp.WorkUnit, cmp.QtyUnit, cmp.WorkType " +
                       " From CM_Process cmp " +
                       " Left join CM_CateItem cm1 on cm1.CateId1 = '07' and cm1.CateId2 = '06' and cm1.ItemCode = cmp.WorkUnit " +
                       " Left join CM_CateItem cm2 on cm2.CateId1 = '07' and cm2.CateId2 = '07' and cm2.ItemCode = cmp.QtyUnit " +
                       " Left join CM_CateItem cm3 on cm3.CateId1 = '07' and cm3.CateId2 = '03' and cm3.ItemCode = cmp.WorkType ";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_Warehouse", query);
                sender.Properties.ValueMember = "ProcessCode";
                sender.Properties.DisplayMember = "ProcessName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCM_ProcessSearchLookupWithWorkType(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cp.ProcessCode, cp.ProcessName, cci.Title " + 
                        " From CM_Process cp " +
                        " Left join CM_CateItem cci on cci.CateId1 ='07' and cci.CateId2 = '03' and cp.WorkType = cci.ClassItemCode";

                sender.DataSource = sqlDb.GetDataView("CM_Process", query);
                sender.ValueMember = "ProcessCode";
                sender.DisplayMember = "ProcessName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        public void GetCM_ProcessSearchLookupWithWorkType(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select cp.ProcessCode, cp.ProcessName, cci.Title " +
                          " From CM_Process cp " +
                          " Left join CM_CateItem cci on cci.CateId1 ='07' and cci.CateId2 = '03' and cp.WorkType = cci.ClassItemCode";

                sender.Properties.DataSource = sqlDb.GetDataView("CM_Warehouse", query);
                sender.Properties.ValueMember = "ProcessCode";
                sender.Properties.DisplayMember = "ProcessName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
        }//end function


        #endregion

        #region "품질관리"
        public DataView GetDefaultExamItem(string corpid, string ItemCode, string itemcode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();

                query = string.Format(" Select cm.ItemCode, cm.ItemName, cc.ItemCode, cc.Title, cc.ExtraValue as ExamGroup, cc.ExtraValue1 as Method " +
                        " From CM_Item cm " +
                        " Inner join CM_ItemExam ce on ce.CorpId = cm.CorpId and ce.ItemCode = cm.ItemCode " +
                        " Inner join CM_CateItem cc on cc.CorpId = ce.CorpId and cc.ItemCode = ce.ItemCode and cc.Class = 'H010' and cc.ItemCode in ({2}) " +
                        " Left join OrderExam oe on oe.CorpId = cm.CorpId " +
                        " Where cm.CorpId = '{0}' and cm.ItemCode = '{1}'", corpid, ItemCode, itemcode);

                view = sqlDb.GetDataView("CM_Cust", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function
        #endregion


        #region 라인코드

        public void GetLineSearchLookup(RepositoryItemSearchLookUpEdit sender, string processCode)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = string.Format(" Select cl.LineCode 코드, cl.Title 라인명, cm.MachineCode 설비코드, cm.Title 설비명 From CM_Line cl Left Join EQ_Machine cm on cm.MachineCode = cl.MachineCode Where cl.ProcessCode = '{0}'", processCode) ;
                sender.DataSource = sqlDb.GetDataView("설비", query);
                sender.ValueMember = "코드";
                sender.DisplayMember = "라인명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try


        }

        public void GetLineSearchLookup(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = $@" Select cl.LineCode 코드, cl.LineName 라인명, cm.MachineCode 설비코드, cm.MachineName 설비명 
                From CM_Line cl 
                Left Join EQ_Machine cm on cm.MachineCode = cl.MachineCode 
                Where cl.CorpId = '{WrGlobal.CorpID}'";

                sender.Properties.DataSource = sqlDb.GetDataView("설비", query);
                sender.Properties.ValueMember = "코드";
                sender.Properties.DisplayMember = "라인명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }
        #endregion

        #region 설비코드

        public void GetMachineSearchLookup(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select MachineCode 코드,  Title 설비명 From EQ_Machine";

                sender.DataSource = sqlDb.GetDataView("설비", query);
                sender.ValueMember = "코드";
                sender.DisplayMember = "설비명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try


        }
        public void GetMachineSearchLookup(SearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select MachineCode 코드,  Title 설비명 From EQ_Machine";

                sender.Properties.DataSource = sqlDb.GetDataView("설비", query);
                sender.Properties.ValueMember = "코드";
                sender.Properties.DisplayMember = "설비명";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

        }

        #endregion

        #region 지그

        public void GetJigSearchLookup(RepositoryItemSearchLookUpEdit sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Select JigID 코드,  Hanger 행거타입 From JigMaster";

                sender.DataSource = sqlDb.GetDataView("지그", query);
                sender.ValueMember = "코드";
                sender.DisplayMember = "행거타입";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try


        }

        #endregion

        #region 수주차수를 구한다.
        public DataView GetSdPlanSdq(string userid, string planYear, string planType)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " Select distinct PlanSeq  " + 
                        " From SdPlanMaster" + 
                        " Where  PlanYear  ='" + planYear + "'" + 
	                         " And PlanType ='" + planType + "'" ;

                view = sqlDb.GetDataView("SdPlanMaster", query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try
            return view;

        }//end function

        #endregion

        #region 세트 코드 처리.
        // 매출기준 자료 처리
        public bool ExecuteMakeSalesObjectVSResult(string hostname, string planYear, int planSeq, string salesType, string corpId, string salesMan)
        {

             Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Exec Proc_GetSalesObjectVSResult '{0}', '{1}', {2}, '{3}', '{4}', '{5}'", hostname, planYear, planSeq, salesType, corpId, salesMan);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;

        }//end functioh

        // 출고기준 자료 처리
        public bool ExecuteMakeSalesObjectVSResult_Out(string hostname, string planYear, int planSeq, string salesType, string corpId, string salesMan)
        {

            Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Exec Proc_GetSalesObjectVSResult_Out '{0}', '{1}', {2}, '{3}', '{4}', '{5}'", hostname, planYear, planSeq, salesType, corpId, salesMan);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;

        }//end functioh

        public bool ExecuteMakeTempCmSetDetail(string hostName, string meterialCode){

            Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Exec Proc_GetTempCmSetDetail '{0}', '{1}'", hostName, meterialCode);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;
        }//end function

        public bool DeleteCmSetDetail(string masterCode, string meterialCode)
        {

            Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Delete From CmSetDetail Where SetCode ='{0}' and MeterialCode ='{1}'", masterCode, meterialCode);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;
        }//end function

        #endregion

        #region DailyReport

        public DataView GetDailyReportByDocNo(string docNo)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " Select * from DailyReport " +
                        " Where DocNo = '" + docNo + "'";

                view = sqlDb.GetDataView("DailyReport", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function


        #endregion

        public void UpdateBM_SdOrderD(string sdOrderNo, string meterialCode)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();
                query = " Update BM_SdOrderD set MeterialType = cm.MeterialGroup, MeterialName = cm.meterialName, Lenght = cm.Lenght, Width = cm.Width, Height = cm.Height, PoNo = isnull(cm.PoNo, '')  " +
                        " from BM_SdOrderD sod " +
                        " inner join CmMeterial cm on cm.MeterialCode = sod.MeterialCode " +
                        " where sod.SdOrderNo='" + sdOrderNo + "' And sod.MeterialCode ='" + meterialCode + "'" +
                              " And sod.State = '수주등록'";


                sqlDb.ExecuteQuery(query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return;

        }//end function

        public void GetMergeId(Object sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = $@" SELECT DISTINCT md.Merge_Id AS 병합ID
                                          , sd.Cond_Id AS 공정조건ID
                                          , sd.Ex_Id AS 외부조건ID
                                          , CONVERT(DATE, sd.GetDate_From) AS 수집시작일자
                                          , CONVERT(DATE, sd.GetDate_To) AS 수집종료일자
                                          , sd. Remark
                            FROM OC_MergeData md
                            LEFT JOIN ST_Std_Dataset sd ON md.Merge_Id = sd.Merge_Id
                            WHERE md.CorpId = '{WrGlobal.CorpID}' 
                            ORDER BY md.Merge_Id ";

                if (sender.GetType().Equals(typeof(SearchLookUpEdit)))
                {
                    SearchLookUpEdit searchLook = sender as SearchLookUpEdit;

                    searchLook.Properties.DataSource = sqlDb.GetDataView("M", query);
                    searchLook.Properties.ValueMember = "병합ID";
                    searchLook.Properties.DisplayMember = "병합ID";

                    searchLook.Properties.PopulateViewColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }
        }

        public bool IsExistsInBM_SdOrderD( string meterialCode)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            bool blRtv = false;

            try
            {
                sqlDb = new Database();
                query = " Select top 1 MeterialCode  " + 
                        " From BM_SdOrderD " + 
                        " Where MeterialCode = '" + meterialCode + "'";


                view = sqlDb.GetDataView("DailyReport", query);

                if (view != null && view.Count > 0)
                {
                    blRtv = true;
                } //end if
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;

        }//end function


        /// <summary>
        /// 수주취소 관련 리스트
        /// </summary>
        /// <param name="sdOrderNo"></param>
        /// <param name="sdOrderNoSeq"></param>
        /// <returns></returns>
        public DataView GetProcMesCheckObtain(string sdOrderNo, int sdOrderNoSeq)
        {
            Database sqlDb = null;
            string query = string.Empty;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                query = " exec KNETMES.dbo.Proc_CheckObtainOrder '" + sdOrderNo + "'," + sdOrderNoSeq;

                view = sqlDb.GetDataView("Proc_CheckObtainOrder", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function


        /// <summary>
        /// 수주취소를 하는 경우 공정자료 삭제
        /// </summary>
        /// <param name="sdOrderNo"></param>
        /// <param name="sdOrderSeq"></param>
        /// <returns></returns>
        public bool ExecuteProcCheckObtainDelete(string sdOrderNo, int sdOrderSeq)
        {

            Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Exec KNETMES.dbo.Proc_CheckObtainOrderDelete '{0}', {1}", sdOrderNo, sdOrderSeq);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;

        }//end functioh


        /// <summary>
        /// 수주자료가 변경된 경우 
        /// </summary>
        /// <param name="sdOrderNo"></param>
        /// <param name="sdOrderSeq"></param>
        /// <returns></returns>
        public bool ExecuteProcCheckObtainOrderUpdate(string sdOrderNo, int sdOrderSeq)
        {
            Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Exec KNETMES.dbo.Proc_CheckObtainOrderUpdate '{0}', {1}", sdOrderNo, sdOrderSeq);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;

        }//end functioh

        /// <summary>
        /// 임시 BOM과 Process를 생성한다.즉, BOM과 Process가 없는 수주등록과, 수주취소된 자료에 대해 처리한다.
        /// </summary>
        /// <param name="sdOrderNo"></param>
        /// <param name="sdOrderSeq"></param>
        /// <returns></returns>
        public bool ExecuteProc_SdOrderTempBomAndProcessMake(string sdOrderNo, int sdOrderSeq, string meterialCode, string writeId, string hostName)
        {
            Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Exec Proc_SdOrderTempBomAndProcessMake '{0}', {1}, '{2}', '{3}', '{4}'", sdOrderNo, sdOrderSeq, meterialCode, writeId, hostName);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;

        }//end functioh

        /// <summary>
        /// 제품코드에 해당하는 자료의 EISYN에 Y를 설정한다.
        /// </summary>
        /// <param name="meterial"></param>
        /// <returns></returns>
        public bool ExecuteCmMeterialEISYNUpdate(string meterial)
        {
            Database sqlDb = null;
            string query = string.Empty;
            bool blRtv = true;

            try
            {
                sqlDb = new Database();
                query = string.Format("Update CmMeterial set EISYN='Y' where MeterialCode='{0}'", meterial);
                sqlDb.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return blRtv;

        }//end functioh


        public DataView ExportExcel(string query)
        {
            Database sqlDb = null;
            DataView view = null;
            try
            {
                sqlDb = new Database();
                view = sqlDb.GetDataView("ExportExcel", query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }//end try

            return view;
        }//end function
    }//end class
}//end namespace
