using SmartPOP.Models.IF;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace SmartPOP.Common
{
    class Database
    {
        private SqlConnection adoCon = new SqlConnection();
        private DataView returnView = null;
        //public static String m_ConnectionString = "SERVER=192.168.105.127;Database=ACEMESDB;User ID=sa;Password=Asis@@2020";   //데이터베이스 연결 스트링
        public static String m_ConnectionString = "SERVER=smtv.iptime.org,2433;Database=YOZM;User ID=smtv;Password=#smtv@9150$";   //데이터베이스 연결 스트링

        private bool disposedValue = false; // 중복 호출을 검색하려면

        public static object DbName { get; internal set; }

        public Database()
        {
            //adoCon.ConnectionString = "Provider=SQLOLEDB;" + m_ConnectionString;
            adoCon.ConnectionString = m_ConnectionString;
        }

        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                DBClose();

                disposedValue = true;
            }
        }

        public void Edit_Property(object cls)
        {
            //속성에 작은따옴료(')있으면 '' 로 수정
            foreach (PropertyInfo property in cls.GetType().GetProperties())    //속성(Property) 탐색
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

        public void PropertyConvert(object className)
        {
            foreach (PropertyInfo propertyInfo in className.GetType().GetProperties().Where(w => w.PropertyType == typeof(string)))
            {
                if (propertyInfo.GetValue(className, null) != null)
                {
                    if (propertyInfo.GetValue(className, null).ToString().Contains("'"))
                    {
                        propertyInfo.SetValue(className, propertyInfo.GetValue(className, null).ToString().Replace("'", "''"));
                    }
                }
            }
        }

        public void DBconnection()
        {
            try
            {
                adoCon.Open();

                if (adoCon.State != ConnectionState.Open)
                {
                    //cls_MessageBox.Show("DB 연결 실패", "DB 연결", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                if (adoCon.State != ConnectionState.Open)
                {

                    //cls_MessageBox.Show(ex.Message, "DB 연결", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void WriteBulkInsert(DataTable dt)
        {
            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(adoCon, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                    //  Insert 할 데이터베이스의 테이블 이름을 지정한다.
                    bulkCopy.DestinationTableName = dt.TableName;
                    bulkCopy.NotifyAfter = 1000;
                    bulkCopy.BatchSize = 1000;

                    foreach (DataColumn col in dt.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    bulkCopy.WriteToServer(dt);
                    //adoCon.Close();
                }
            }
            catch
            {
            }
        }

        public bool ExecuteQueryList(List<string> queryList)
        {
            SqlCommand cmd = new SqlCommand();
            bool blRtv = false;

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    cmd.Connection = adoCon;
                    blRtv = true;

                    cmd.CommandTimeout = 180;
                    //cmd.Connection.Open();

                    foreach (string query in queryList)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                blRtv = false;
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //cmd.Connection.Close();
            }

            return blRtv;
        }

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

                returnView = ds.Tables[tableName].DefaultView;
                return returnView;
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
            }
        }//end function


        public DataTable GetDataTable(string strQuery)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
            try
            {

                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {

                //cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
            }

        }//end function

        public DataRow GetDataRow(string strQuery)
        {


            DataSet ds = new DataSet();
            DataTable dt;

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);

                    using (SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon))
                    {
                        SqlAdapter.Fill(ds, "OneRow");

                        dt = ds.Tables["OneRow"];
                        if (dt != null && dt.Rows.Count == 1)
                        {
                            return dt.Rows[0];
                        }
                    }

                    //adoCon.Close();

                    return null;
                }
            }
            catch (Exception ex)
            {
                //WriteLog(strQuery);
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {

            }
        }

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
                //cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public string ExecAutoNumber(string _docType, DateTime _currentDate)
        {

            DBconnection();
            string returnValue = string.Empty;

            SqlCommand comm = null;

            try
            {
                comm = new SqlCommand("Code_AutoNumber", adoCon);

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@pCorpID", WrGlobal.CorpID));
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
                throw ex;
            }
            finally
            {
                comm = null;
                DBClose();
            }//end try 

            return returnValue;

        }//end fucntion
        public string ExecProcYYYY(string _docType, DateTime _currentDate)
        {
            string _docNo = "";
            try
            {
                DBconnection();
                string query = String.Format("Exec Proc_AutoNumber '{0}','{1}'", _docType, _currentDate.ToString("yyyy"));

                SqlCommand cmd = new SqlCommand(query, adoCon);
                _docNo = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                //cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _docNo = null;
            }
            finally
            {
                DBClose();
            }
            return _docNo;
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
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try

            return blRtv;

        }//end function

        public bool ExcuteNonQuery1(string[] querys)
        {
            SqlConnection adoCon = new SqlConnection(m_ConnectionString);
            bool blRtv = true;

            try
            {
                adoCon.Open();
                foreach (string query in querys)
                {
                    SqlCommand cmd = new SqlCommand(query, adoCon);
                    cmd.Connection = adoCon;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                blRtv = false;
                SMTFunction.Log("ExcuteNonQuery1 DB Error - " + ex.Message);
            }
            finally
            {
                adoCon.Close();
            }

            return blRtv;
        }

        public bool ExecuteQueryImage(string strQuery, byte[] image)
        {
            SqlCommand cmd = new SqlCommand(strQuery, adoCon);
            if (image == null)
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
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try

            return blRtv;

        }//end function

        public bool ExecuteCommand(SqlCommand cmd)
        {
            bool blRtv = true;
            try
            {
                cmd.Connection = adoCon;
                cmd.CommandTimeout = 180;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                blRtv = false;
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return blRtv;
        }

        public int ExecuteQueryAndReturnRows(string strQuery)
        {
            int rtv = 0;

            SqlCommand cmd = new SqlCommand(strQuery, adoCon);


            try
            {
                cmd.Connection.Open();
                rtv = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try
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

        public object ExecuteQueryAndReturnResult(string strQuery)
        {
            object result = null;

            using (SqlCommand cmd = new SqlCommand(strQuery, adoCon))
            {
                try
                {
                    cmd.Connection.Open();
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                cmd.Connection.Close();
            }
            return result;
        }

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

        public bool ExecuteBulkCopy(DataTable dt)
        {
            bool result = false;

            DBconnection();
            SqlTransaction adoTran = adoCon.BeginTransaction();

            try
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(adoCon, SqlBulkCopyOptions.TableLock, adoTran))
                {
                    bulk.BulkCopyTimeout = 0;
                    bulk.BatchSize = dt.Rows.Count;
                    bulk.DestinationTableName = dt.TableName;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        bulk.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }

                    bulk.WriteToServer(dt);
                    dt.Clear();

                    bulk.Close();
                }

                adoTran.Commit();

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                adoTran.Dispose();
                DBClose();
            }

            return result;
        }
        public bool ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            bool result = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(m_ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandTimeout = 180;

                        // 파라미터가 있는 경우 추가
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);
                            }
                        }

                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            return result;
        }

        public int ExecuteScalarQuery(string strQuery)
        {
            SqlCommand cmd = null;
            int result = 0;

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    cmd = new SqlCommand(strQuery, adoCon);

                    cmd.CommandTimeout = 180;
                    //cmd.Connection.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar()); // 행의 수를 반환받음
                }
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = -1;
            }
            finally
            {
                //cmd.Connection.Close();
            }

            return result;
        }

        public string ExecuteScalarStringQuery(string strQuery)
        {
            SqlCommand cmd = null;
            string result = string.Empty;

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    cmd = new SqlCommand(strQuery, adoCon);

                    cmd.CommandTimeout = 180;
                    //cmd.Connection.Open();
                    result = cmd.ExecuteScalar()?.ToString(); // 첫 번째 행의 첫 번째 값을 문자열로 반환
                }
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //cmd.Connection.Close();
            }

            return result;
        }
    }
}

