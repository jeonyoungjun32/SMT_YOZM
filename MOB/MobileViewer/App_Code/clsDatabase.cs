using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MobileViewer
{
    public class clsDatabase
    {
        private SqlConnection adoCon = new SqlConnection();
        private DataView returnView = null;
        public String m_ConnectionString = "SERVER=smtv.iptime.org,2433;Database= YOZM;User ID=smtv;Password=#smtv@9150$";   //데이터베이스 연결 스트링

        public clsDatabase()
        {
            adoCon.ConnectionString = m_ConnectionString;
        }

        public void DBconnection()
        {
            try
            {
                if (adoCon.State != ConnectionState.Open)
                {
                    adoCon.Open();
                }
            }

            catch (Exception ex)
            {
                if (adoCon.State != ConnectionState.Open)
                {
                    HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                }
            }//end try

        }//end function

        public DataTable GetDataTable(string strQuery)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();

                    SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
                    SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                    SqlAdapter.SelectCommand.CommandTimeout = 180;
                    SqlAdapter.Fill(ds);

                    if (ds.Tables.Count == 0)
                    {
                        ds.Tables.Add(new DataTable());
                    }

                    //adoCon.Close();

                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                //WriteLog(strQuery);
                return null;
            }
            finally
            {
            }
        }//end function

        public void DBClose()
        {
            if (adoCon.State == ConnectionState.Open)
            {
                adoCon.Close();
            }

        }//end function

        public SqlCommand GetProcedure(string procedurename)
        {
            DBconnection();

            SqlCommand cmd = new SqlCommand(procedurename, adoCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedurename;
            return cmd;
        }

        public DataView GetDataView(string tblName, string strQuery)
        {
            DBconnection();

            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.Fill(ds, tblName);

                returnView = ds.Tables[tblName].DefaultView;
                return returnView;
            }
            catch (Exception ex)
            {

                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                return null;
            }
            finally
            {
                DBClose();
            }
        }//end function

        public SqlDataReader GetDataReader(string strQuery)
        {
            try
            {
                DBconnection();

                SqlCommand cmd = new SqlCommand(strQuery, adoCon);
                return cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                return null;
            }
            finally
            {

            }
        }//end function


        public DataSet GetDataSet(string strTable, string strQuery)
        {
            DBconnection();

            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.Fill(ds, strTable);
                return ds;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                return null;
            }
            finally
            {
                DBClose();
            }
        }//end function


        public DataRowView GetDataRecord(string strQuery)
        {
            DBconnection();

            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);

            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.Fill(ds, "OneRow");

                returnView = ds.Tables["OneRow"].DefaultView;
                if (returnView != null && returnView.Count == 1)
                {
                    return returnView[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                return null;
            }
            finally
            {
                DBClose();
            }
        }

        public int GetDataScalar(string strQuery)
        {
            int _scalar = 0;
            try
            {
                DBconnection();

                SqlCommand cmd = new SqlCommand(strQuery, adoCon);
                _scalar = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                _scalar = 0;
            }
            finally
            {
                DBClose();
            }
            return _scalar;

        }

        public bool ExecuteQuery(string strQuery)
        {
            DBconnection();

            bool result = false;
            SqlCommand cmd = new SqlCommand(strQuery, adoCon);

            try
            {
                cmd.ExecuteNonQuery();

                result = true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                DBClose();
            }
            return result;
        }


        public string ExecProc(string _query)
        {
            string outString = "";
            try
            {
                DBconnection();

                SqlCommand cmd = new SqlCommand(_query, adoCon);
                outString = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                outString = "";
            }
            finally
            {
                DBClose();
            }
            return outString;

        }

        public string ExecAutoNumber(string corpId, string _docType, DateTime _currentDate)
        {
            string returnValue = string.Empty;
            SqlCommand comm = null;

            try
            {
                DBconnection();

                comm = new SqlCommand("Code_AutoNumber", adoCon);

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@pCorpID", corpId));
                comm.Parameters.Add(new SqlParameter("@pDocType", _docType));
                comm.Parameters.Add(new SqlParameter("@pCurrentDate", _currentDate.ToString("yyyyMMdd")));

                var returnParam = new SqlParameter
                {
                    ParameterName = "@pNumber",
                    Size = 13,
                    Direction = ParameterDirection.Output
                };

                comm.Parameters.Add(returnParam);

                comm.ExecuteNonQuery();

                returnValue = (string)returnParam.Value;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script language='JavaScript'>alert('" + ex.Message + "')</script>");
                returnValue = "";
            }
            finally
            {
                DBClose();
            }

            return returnValue;
        }

    }
}