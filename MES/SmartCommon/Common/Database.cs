using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SmartCommon.Common
{
    public class Database
    {
        private SqlConnection adoCon = new SqlConnection();
        private DataView returnView = null;
        public static String m_ConnectionString = SmartCommon.Properties.Settings.Default.MESDBConnectionString;



        public static string DbName = string.Empty;
        public static string Url = string.Empty;

        public Database()
        {
            string conStr = $"Data Source={Url};"
                          + $"Initial Catalog={DbName};"
                          + $"User ID=smtv;"
                          + $"Password=#smtv@9150$;"
                          + $"encrypt=false;";
            adoCon.ConnectionString = conStr;
            m_ConnectionString = conStr;
        }


        //public Database()
        //{
        //    adoCon.ConnectionString = m_ConnectionString;
        //}

        ~Database()
        {
            this.Dispose(false);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

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

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~WrDatabase()
        // {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion

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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {

            }
        }

        public DataView GetDataView(string tableName, string strQuery)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
                SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                SqlAdapter.SelectCommand.CommandTimeout = 180;
                SqlAdapter.Fill(ds, tableName);


                returnView = ds.Tables[tableName].DefaultView;
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

        public Object GetDataScalar(string strQuery)
        {
            try
            {
                DBconnection();
                BindingSource bindsource = new BindingSource();

                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    SqlCommand cmd = new SqlCommand(strQuery, adoCon);
                    return cmd.ExecuteScalar();
                }

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
        }

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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
            }
        }//end function

        public DataSet GetDataSet(string strQuery)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();

                    SqlDataAdapter SqlAdapter = new SqlDataAdapter(strQuery, adoCon);
                    SqlAdapter.SelectCommand = new SqlCommand(strQuery, adoCon);
                    SqlAdapter.Fill(ds);
                    return ds;
                }
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

                if (ds.Tables["OneRow"] == null)
                {
                    return null;
                }

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

        public string ExecAutoNumber(string _docType, DateTime _currentDate)
        {

            DBconnection();
            string returnValue = string.Empty;

            SqlCommand comm = null;

            try
            {
                comm = new SqlCommand("Code_AutoNumber", adoCon);

                comm.CommandType = CommandType.StoredProcedure;

                //comm.Parameters.Add(new SqlParameter("@pCorpID", WrGlobal.PublicCorpID));
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

        public void ExecStoreSalesReceiptUpdate(string storeSalesNo, byte[] bytes)
        {

            DBconnection();
            string returnValue = string.Empty;

            SqlCommand comm = null;

            try
            {

                comm = new SqlCommand($"Update StoreSalesReceipt Set ReceiptData = @ReceiptData Where CorpId = '{WrGlobal.CorpID}' and StoreSalesNo = '{storeSalesNo}'", adoCon);
                comm.Parameters.AddWithValue("@ReceiptData", bytes);

                comm.ExecuteNonQuery();
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

        }//end fucntion

        public string ExecAutoPublicNumber(string _docType, DateTime _currentDate)
        {

            DBconnection();
            string returnValue = string.Empty;

            SqlCommand comm = null;

            try
            {
                comm = new SqlCommand("Code_AutoNumber", adoCon);

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@pCorpID", WrGlobal.PublicCorpID));
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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try

            return blRtv;

        }//end function

        public bool ExecuteQuery(string strQuery, List<SqlParameter> _parameters)
        {
            bool blRtv = true;
            SqlCommand cmd = null;

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    cmd = new SqlCommand(strQuery, adoCon);

                    cmd.CommandTimeout = 180;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter p in _parameters)
                    {
                        cmd.Parameters.Add(p);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //cmd.Connection.Close();
                //a.Dispose();
            }//end try

            return blRtv;

        }//end function

        public bool ExecuteCommand(SqlCommand cmd)
        {
            bool blRtv = true;
            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    cmd.Connection = adoCon;
                    cmd.CommandTimeout = 180;
                    //cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //cmd.Connection.Close();
            }
            return blRtv;
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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //cmd.Connection.Close();
            }

            return result;
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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                cmd.Connection.Close();
                //a.Dispose();
            }//end try
            return rtv;
        }//end


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
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                adoTran.Dispose();
                DBClose();
            }

            return result;
        }

        public void WriteBulkInsert(DataTable dt)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(adoCon, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
            //  Insert 할 데이터베이스의 테이블 이름을 지정한다.
            bulkCopy.DestinationTableName = dt.TableName;
            adoCon.Open();
            bulkCopy.NotifyAfter = 1000;
            bulkCopy.BatchSize = 1000;

            foreach (DataColumn col in dt.Columns)
            {
                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            }

            bulkCopy.WriteToServer(dt);
            adoCon.Close();
        }

        public void WriteAdvanceBulkInsert(DataTable dt)
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(adoCon, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
            //  Insert 할 데이터베이스의 테이블 이름을 지정한다.
            bulkCopy.DestinationTableName = dt.TableName;
            adoCon.Open();
            bulkCopy.NotifyAfter = 1000;
            bulkCopy.BatchSize = 1000;

            DataView dv = GetDataView(dt.TableName, $"Select * from [{dt.TableName}] where 1 = 2");

            try
            {
                foreach (DataColumn col in dv.Table.Columns)
                {
                    if (dt.Columns.IndexOf(col.ColumnName) == -1)
                    {
                        dt.Columns.Add(col.ColumnName, col.DataType);
                    }
                }

                for (int i = dt.Columns.Count - 1; i >= 0; i--)
                {
                    DataColumn col = dt.Columns[i];
                    if (dv.Table.Columns.IndexOf(col.ColumnName) == -1)
                    {
                        dt.Columns.Remove(col);
                    }
                }

                foreach (DataColumn col in dt.Columns)
                {
                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }

                bulkCopy.WriteToServer(dt);
            }
            catch (Exception ex) { }
            finally { adoCon.Close(); }
        }

        public bool ExecuteQueryList(List<string> queryList)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = adoCon;
            bool blRtv = true;

            try
            {
                cmd.CommandTimeout = 180;
                cmd.Connection.Open();

                foreach (string query in queryList)
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Connection.Close();
            }

            return blRtv;
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

        //로그 작성
        private void WriteLog(string logTxt)
        {
            //로그파일 탐색
            string path = System.IO.Directory.GetCurrentDirectory();
            string fileName = "Log_ExcuteDB.txt";
            string fullPath = path + "\\" + fileName;

            //파일 경로에 파일이 있는지 확인
            if (System.IO.File.Exists(fullPath))
            {
                //있으면 텍스트파일 작성
                WriteQuery(fullPath, logTxt);
            }
            else
            {
                //없으면 파일 생성 후 작성
                System.IO.FileStream file = System.IO.File.Create(fullPath);
                file.Close();

                WriteQuery(fullPath, logTxt);
            }
        }
        private void WriteQuery(string path, string log)
        {
            //텍스트파일 작성
            string LogFront = "DB Query Fail\nError Query : \n";
            string LogBack = "\nLogTime : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n";

            System.IO.StreamWriter writer;
            writer = System.IO.File.AppendText(path);
            writer.WriteLine(LogFront + log + LogBack);
            writer.Close();
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

        //이미지 저장용
        public bool ExecuteQueryImage(string strQuery, byte[] image)
        {
            SqlCommand cmd = null;
            bool blRtv = false;

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    cmd = new SqlCommand(strQuery, adoCon);

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


                    blRtv = true;

                    cmd.CommandTimeout = 180;
					//cmd.Connection.Open();
					cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //cmd.Connection.Close();
                //a.Dispose();
            }//end try

            return blRtv;

        }//end function

        public bool ExecuteQueryImages(string query, byte[] pic1, byte[] pic2)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(m_ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.Add("@Pic1", SqlDbType.VarBinary).Value =
                        (object)pic1 ?? DBNull.Value;

                    cmd.Parameters.Add("@Pic2", SqlDbType.VarBinary).Value =
                        (object)pic2 ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        //여러개 이미지 저장
        public bool ExecuteQueryImage(string strQuery, Dictionary<string, byte[]> images)
        {
            bool blRtv = false;

            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();

                    SqlCommand cmd = new SqlCommand(strQuery, adoCon);

                    foreach (var img in images)
                    {
                        if (img.Value == null)
                            cmd.Parameters.Add(img.Key, SqlDbType.VarBinary).Value = DBNull.Value;
                        else
                            cmd.Parameters.AddWithValue(img.Key, img.Value);
                    }

                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();

                    blRtv = true;
                }
            }
            catch (Exception ex)
            {
                blRtv = false;
                MessageBox.Show(ex.Message);
            }

            return blRtv;
        }

        // DB에 저장된 이미지를 byte[]로 변환
        public byte[] ExecuteImageToByte(string query)
        {
            byte[] imageData = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(m_ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        imageData = cmd.ExecuteScalar() as byte[];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return imageData;
        }

        public byte[] GetImageFromQuery(string query)
        {
            try
            {
                using (SqlConnection adoCon = new SqlConnection(m_ConnectionString))
                {
                    adoCon.Open();
                    using (SqlCommand cmd = new SqlCommand(query, adoCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && !reader.IsDBNull(0))
                            {
                                return (byte[])reader[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public void SaveLayoutData(string spaceName, string formName, string controlName, byte[] data)
        {
            DBconnection();
            SqlCommand comm = null;
            string query = "";
            try
            {
                query = String.Format("IF NOT Exists(Select CorpId From SM_UserLayout Where CorpId = '{0}' and EmployeeId = '{1}' and Namespace = '{2}' and FormName = '{3}' and ControlName = '{4}') "
                                    + "Begin "
                                        + "Insert Into SM_UserLayout Values('{0}', '{1}', '{2}', '{3}', '{4}', @LayoutData) "
                                    + "End"
                                    , WrGlobal.CorpID, WrGlobal.LoginID, spaceName, formName, controlName);

                comm = new SqlCommand(query, adoCon);
                comm.Parameters.Add("@LayoutData", SqlDbType.VarBinary).Value = data;
                comm.ExecuteNonQuery();
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
        }//end fucntion
    }
}
