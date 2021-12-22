using Dapper;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace RozetkaPageFactory.TestDataAccess
{
    class ExcelDataAccess
    {
        public static string TestDataFileConnection()
        {
            string fileName = Directory.GetCurrentDirectory();
            fileName = fileName.Substring(0, fileName.Length - 17);
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}\TestDataAccess\TestData.xlsx; Extended Properties=Excel 12.0;", fileName);
            return con;
        }

        public static UserData GetTestData(string keyName)
        {
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                connection.Open();
                var query = string.Format("select * from [DataSet$] where key='{0}'", keyName);
                var value = connection.Query<UserData>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }
    }
}
