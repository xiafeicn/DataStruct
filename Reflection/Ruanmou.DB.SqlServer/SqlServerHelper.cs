using Ruanmou.DB.Interface;
using Ruanmou.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace Ruanmou.DB.SqlServer
{
    /// <summary>
    /// SqlServer实现
    /// </summary>
    public class SqlServerHelper : IDBHelper
    {
        private static string ConnectionStringCustomers = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;

        public SqlServerHelper()
        {
            //Console.WriteLine("{0}被构造", this.GetType().Name);
        }

        public void Query()
        {
            //Console.WriteLine("{0}.Query", this.GetType().Name);
        }

        /// <summary>
        /// 一个方法满足不同的数据实体查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(int id)
        {
            Type type = typeof(T);
            string columnStrings = string.Join(",", type.GetProperties().Select(p => string.Format("[{0}]", p.Name)));

            string sql = string.Format("SELECT {0} FROM [{1}] Where Id={2}"
               , columnStrings
               , type.Name
               , id);

            object t = Activator.CreateInstance(type);
            using (SqlConnection conn = new SqlConnection(ConnectionStringCustomers))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    foreach (var item in type.GetProperties())
                    {
                        item.SetValue(t, reader[item.Name]);
                    }
                }
            }

            return (T)t;

            //return default(T);
        }
    }
}
