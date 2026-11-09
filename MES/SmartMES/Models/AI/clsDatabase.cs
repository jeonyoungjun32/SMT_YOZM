
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace SmartMES.Models.AI
{
    public class Database
    {
        private SqlConnection adoCon = new SqlConnection();
        private DataView returnView = null;

        //public static String m_ConnectionString = ConfigurationManager.ConnectionStrings["SmartAI.Properties.Settings.TargetConnectionString"].ConnectionString; //데이터베이스 연결 스트링
        
        public static string DbName = string.Empty;
        public static string Url = string.Empty;

        public Database()
        {
            string conStr = $"Data Source=smtv.iptime.org,2434;"
                          + $"Initial Catalog=AI_HOSUNG;"
                          + $"User ID=smtv;"
                          + $"Password=#smtv@9150$";
            adoCon.ConnectionString = conStr;
        }

        public Database(string server, string database, string uid, string pwd)
        {
            adoCon.ConnectionString = $"SERVER={server};" +
                                      $"DATABASE={database};" +
                                      $"UID={uid};" +
                                      $"PWD={pwd}";
        }

        public void DBconnection()
        {
            try
            {
                adoCon.Open();

                if (adoCon.State != ConnectionState.Open)
                {
                    MessageBox.Show("DB 연결 실패", "DB 연결", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                if (adoCon.State != ConnectionState.Open)
                {

                    MessageBox.Show(ex.Message, "DB 연결", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//end try
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
            SqlCommand cmd = new SqlCommand(procedurename, adoCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedurename;
            return cmd;
        }

        public DataView GetDataView(string tableName, string strQuery)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.SelectCommand.CommandTimeout = 180;
                SqlAdapter.Fill(ds, tableName);

                returnView = ds.Tables[tableName]?.DefaultView;
                return returnView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
            }
        }//end function


        public SqlDataReader GetDataReader(string strQuery)
        {
            try
            {
                DBconnection();
                BindingSource bindsource = new BindingSource();
                SqlCommand cmd = new SqlCommand(strQuery, adoCon);
                return cmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                DBClose();
            }
        }//end function

        public DataTable GetDataSet(string strQuery)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.SelectCommand.CommandTimeout = 180;
                SqlAdapter.Fill(ds);

                if (ds.Tables.Count == 0)
                {
                    ds.Tables.Add(new DataTable());
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
            }
        }//end function

        public DataTable GetDataSet_MES(string strQuery)
        {
            string conStr = $"Data Source=smtv.iptime.org,2433;"
              + $"Initial Catalog={DbName.Replace("AI_", "")};"
              + $"User ID=smtv;"
              + $"Password=#smtv@9150$";
            adoCon.ConnectionString = conStr;
            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.SelectCommand.CommandTimeout = 180;
                SqlAdapter.Fill(ds);

                if (ds.Tables.Count == 0)
                {
                    ds.Tables.Add(new DataTable());
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
            }
        }//end function

        public DataTable GetDataSetNoMsg(string strQuery)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.SelectCommand.CommandTimeout = 180;
                SqlAdapter.Fill(ds);

                if (ds.Tables.Count == 0)
                {
                    ds.Tables.Add(new DataTable());
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }//end function

        public DataSet GetDataSet(string tableName, string strQuery)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {

                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.Fill(ds, tableName);
                return ds;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
            }

        }//end function


        public DataRowView GetDataRecord(string strQuery)
        {
            
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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }       
        public bool ExecuteQuery(string strQuery)
        {

            SqlCommand cmd = new SqlCommand(strQuery, adoCon);
           bool blRtv = true;

            try
            {
                cmd.CommandTimeout = 180;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try
            
            return blRtv;

        }//end function

        public bool ExecuteQueryImage(string strQuery, byte[] image)
        {
            SqlCommand cmd = new SqlCommand(strQuery, adoCon);
            if(image == null)
            {   //이미지가 null(No Image Data)인경우 null값 입력
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Image;
                parameter.Value = DBNull.Value;
                parameter.ParameterName = "@Pic";

                cmd.Parameters.Add(parameter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Pic", image);
            }

            
            bool blRtv = true;

            try
            {
                cmd.CommandTimeout = 180;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try

            return blRtv;

        }//end function

        public bool ExecuteQueryImage2(string strQuery, byte[] image1, byte[] image2)
        {
            SqlCommand cmd = new SqlCommand(strQuery, adoCon);
            if (image1 == null || image2 == null)
            {   //이미지가 null(No Image Data)인경우 null값 입력
                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = SqlDbType.Image;
                parameter.Value = DBNull.Value;
                parameter.ParameterName = "@Pic1";
                parameter.ParameterName = "@Pic2";

                cmd.Parameters.Add(parameter);
            }

            else
            {
                cmd.Parameters.AddWithValue("@Pic1", image1);
                cmd.Parameters.AddWithValue("@Pic2", image2);
            }


            bool blRtv = true;

            try
            {
                cmd.CommandTimeout = 180;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try

            return blRtv;

        }//end function

        public int ExecuteQueryAndReturnRows(string strQuery, string tableName)
        {
           

            int rtv = 0;

            DataSet ds = new DataSet();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.SelectCommand.CommandTimeout = 180;
                SqlAdapter.Fill(ds, tableName);

                returnView = ds.Tables[tableName].DefaultView;
                return returnView.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
            }



           // try
           // {
           //     cmd.Connection.Open();
           //     rtv = cmd.ExecuteNonQuery();
           // }
           // catch (Exception ex)
           // {
           //     MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           //     return 0;
           // }
           // finally
           // {
           //     cmd.Connection.Close();
           //     //a.Dispose();
           // }//end try
           return rtv;
        }//end 

        public string DeletePlacingOutput(string _PlacingNo, string _Message)
        {

            DBconnection();
            string returnValue = string.Empty;

            SqlCommand comm = null;

            try
            {
                comm = new SqlCommand("proc_PlacingDelete", adoCon);

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@pPlacingNo", _PlacingNo));
                //comm.Parameters.Add(new SqlParameter("@pMessage", _Message));

                var returnParam = new SqlParameter
                {
                    ParameterName = "@pMessage",
                    Size = 100,
                    Direction = ParameterDirection.Output
                };

                comm.Parameters.Add(returnParam);

                comm.ExecuteNonQuery();

                returnValue = (string)returnParam.Value;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comm = null;
                DBClose();
            }//end try 

            return returnValue;

        }//end fucntion

        public string UpdatePlacingOutput(string _PlacingNo, string _Message)
        {

            DBconnection();
            string returnValue = string.Empty;

            SqlCommand comm = null;

            try
            {
                comm = new SqlCommand("proc_PlacingUpdate", adoCon);

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@pPlacingNo", _PlacingNo));
                //comm.Parameters.Add(new SqlParameter("@pMessage", _Message));

                var returnParam = new SqlParameter
                {
                    ParameterName = "@pMessage",
                    Size = 100,
                    Direction = ParameterDirection.Output
                };

                comm.Parameters.Add(returnParam);

                comm.ExecuteNonQuery();

                returnValue = (string)returnParam.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comm = null;
                DBClose();
            }//end try 

            return returnValue;

        }//end fucntion

        public string InputCheck(string _PlacingNo, string _InputYn)
        {

            DBconnection();
            string returnValue = string.Empty;

            SqlCommand comm = null;

            try
            {
                comm = new SqlCommand("Proc_PlacingDeleteCheck", adoCon);

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@pPoNo", _PlacingNo));
                //comm.Parameters.Add(new SqlParameter("@pMessage", _Message));

                var returnParam = new SqlParameter
                {
                    ParameterName = "@pInputYn",
                    Size = 100,
                    Direction = ParameterDirection.Output
                };

                comm.Parameters.Add(returnParam);

                comm.ExecuteNonQuery();

                returnValue = (string)returnParam.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comm = null;
                DBClose();
            }//end try 

            return returnValue;

        }//end fucntion
        internal DataView GetTempProductPrice()
        {
            throw new NotImplementedException();
        }

        internal object GetDeptWarehouse(string p)
        {
            throw new NotImplementedException();
        }

        public void WriteBulkInsert(DataTable dt)
        {
            //ExecuteQuery("Truncate Table " + dt.TableName);

            SqlBulkCopy bulkCopy = new SqlBulkCopy(adoCon, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
            //  Insert 할 데이터베이스의 테이블 이름을 지정한다.
            bulkCopy.DestinationTableName = dt.TableName;
            adoCon.Open();
            bulkCopy.NotifyAfter = 1000;
            bulkCopy.BatchSize = 1000;

            foreach(DataColumn col in dt.Columns)
            {
                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            }

            bulkCopy.WriteToServer(dt);
            adoCon.Close();
        }

        public void Edit_Property(object cls)
        {
            //속성에 작은따옴료(')있으면 '' 로 수정
            foreach (PropertyInfo property in cls.GetType().GetProperties())    //속성 탐색
            {
                string value = Convert.ToString(property.GetValue(cls));    //속성값 가져오기

                if (value.Contains("'"))
                {
                    //작은따옴표 있으면 수정
                    value = value.Replace("'", "''");
                    property.SetValue(cls, value);
                }
            }
        }
    }
}
