using System;
using System.Data;
using System.Data.SqlClient;

namespace SmartMES.Common
{
    /// <summary>사업장·사용자·화면별 배치를 저장합니다. 업무 데이터는 저장하지 않습니다.</summary>
    internal sealed class ScreenLayoutRepository
    {
        private readonly string connectionString;
        private readonly string corpId;
        private readonly string employeeId;

        internal ScreenLayoutRepository(string connectionString, string corpId, string employeeId)
        {
            this.connectionString = connectionString;
            this.corpId = corpId;
            this.employeeId = employeeId;
        }

        internal string Load(string key)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = Command(connection,
                "SELECT CONVERT(nvarchar(max), LayoutXml) FROM dbo.SM_ScreenLayout WHERE CorpId=@corp AND EmployeeId=@user AND ScreenKey=@key", key))
            {
                connection.Open();
                return command.ExecuteScalar() as string;
            }
        }

        internal void Save(string key, string formName, string menuId, string xml)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = Command(connection, @"
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;
                UPDATE dbo.SM_ScreenLayout WITH (UPDLOCK, SERIALIZABLE)
                SET LayoutXml=@xml, FormName=@form, MenuId=@menu, ModDt=SYSDATETIME()
                WHERE CorpId=@corp AND EmployeeId=@user AND ScreenKey=@key;
                IF @@ROWCOUNT=0
                    INSERT dbo.SM_ScreenLayout (CorpId,EmployeeId,ScreenKey,FormName,MenuId,LayoutXml)
                    VALUES (@corp,@user,@key,@form,@menu,@xml);
                COMMIT;", key))
            {
                command.Parameters.Add("@xml", SqlDbType.Xml).Value = xml;
                command.Parameters.Add("@form", SqlDbType.NVarChar, 256).Value = formName;
                command.Parameters.Add("@menu", SqlDbType.NVarChar, 100).Value = menuId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal void Delete(string key)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = Command(connection,
                "DELETE dbo.SM_ScreenLayout WHERE CorpId=@corp AND EmployeeId=@user AND ScreenKey=@key", key))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private SqlCommand Command(SqlConnection connection, string sql, string key)
        {
            if (string.IsNullOrWhiteSpace(corpId) || string.IsNullOrWhiteSpace(employeeId))
                throw new InvalidOperationException("로그인한 사용자와 사업장을 확인하세요.");
            var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@corp", SqlDbType.NVarChar, 10).Value = corpId;
            command.Parameters.Add("@user", SqlDbType.NVarChar, 128).Value = employeeId;
            command.Parameters.Add("@key", SqlDbType.Char, 64).Value = key;
            return command;
        }
    }
}
