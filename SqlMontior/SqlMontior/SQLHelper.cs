using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SqlMontior
{
    public static class SQLHelper
    {
        public static string LocalIpConnectionString { get; set; }
        public static string PublicIpConnectionString { get; set; }
     
      
        /// <summary>
        /// 执行无参数无返回值的存储过程
        /// </summary>
        /// <param name="spName"></param>
        public static void ExecuteStoredProcedure(string spName, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = spName;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行参数列表无返回值的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sqlParameter"></param>
        public static void ExecuteStoredProcedure(string spName, SqlParameter[] sqlParameter, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameter);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    command.Parameters.Clear();
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 执行参数列表有返回值的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sqlParameter"></param>
        public static int ExecuteNonQuery(string spName, SqlParameter[] sqlParameter, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameter);
                try
                {
                    conn.Open();
                    return command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    command.Parameters.Clear();
                    conn.Close();
                }
            }
        }
        //执行不带参数的sql操作
        public static int ExecuteSqlNonQuery(string spName, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(spName, conn))
                {
                    try
                    {
                        conn.Open();
                        int rows = command.ExecuteNonQuery();
                        return rows;
                    }
                    catch 
                    {
                        conn.Close();
                        throw;
                    }
                }

            }
        }
        //执行带参数的sql操作
        public static int ExecuteSql(string spName, SqlParameter[] sqlParameter, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameter);
                try
                {
                    conn.Open();
                    return command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    command.Parameters.Clear();
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 执行无参数返回DataTable的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public static DataTable ExecuteStoredProcedureToDataTable(string spName, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                    DataTable dt = new DataTable("table");
                    sqlDA.Fill(dt);
                    return dt;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// 执行无参数返回DataTable的SQL
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public static DataTable ExecuteSqlToDataTable(string sql, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.CommandType = System.Data.CommandType.Text;
                try
                {
                    conn.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                    DataTable dt = new DataTable("table");
                    sqlDA.Fill(dt);
                    return dt;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
         /// 执行有参数返回DataTable的SQl
         /// </summary>
         /// <param name="sql"></param>
         /// <param name="sqlParameters"></param>
         /// <param name="dbConnectionString"></param>
         /// <returns></returns>
         public static DataTable ExecuteSqlToDataTable(string sql, SqlParameter[] sqlParameters, string dbConnectionString) {
           using (SqlConnection conn = new SqlConnection(dbConnectionString))
             {
                 SqlCommand command = new SqlCommand(sql, conn);
                 command.CommandType = System.Data.CommandType.Text;
                 command.Parameters.AddRange(sqlParameters);
                 try
                 {
                     conn.Open();
                     SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                     DataTable dt = new DataTable("table");
                     sqlDA.Fill(dt);
                     return dt;
                 }
                 catch
                 {
                     throw;
                 }
                 finally
                 {
                     conn.Close();
                 }
             }
         }
        /// <summary>
        /// 执行参数为数组列表返回值为DataTable的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sqlParameter"></param>
        /// SqlParameter sp = new SqlParameter("@para", value);
        /// <returns></returns>
        public static DataTable ExecuteStoredProcedureToDataTable(string spName, SqlParameter[] sqlParameter, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameter);
                try
                {
                    conn.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                    DataTable dt = new DataTable("table");
                    sqlDA.Fill(dt);
                    command.Parameters.Clear();
                    return dt;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行无参数，返回DataSet的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public static DataSet ExecuteStoredProcedureToDataSet(string spName, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    return ds;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行参数为数组，返回值为DataSet的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sqlParameter"></param>
        /// SqlParameter sp = new SqlParameter("@para", value);
        /// <returns></returns>
        public static DataSet ExecuteStoredProcedureToDataSet(string spName, SqlParameter[] sqlParameter, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameter);
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                    command.Parameters.Clear();
                    return ds;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 通过sql获得结果的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="dbConnectionString"></param>
        /// <returns></returns>
        public static string ExecteSqlScalar(string sql, SqlParameter[] sqlParameters, string dbConnectionString) {
          var table = ExecuteSqlToDataTable(sql, sqlParameters, dbConnectionString);
          return table.Rows[0][0].ToString();
        }
        /// <summary>
        /// 通过sql获得结果的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbConnectionString"></param>
        /// <returns></returns>
        public static string ExecteSqlScalar(string sql, string dbConnectionString) {
          var table = ExecuteSqlToDataTable(sql, dbConnectionString);
          return table.Rows[0][0].ToString();
        }
        /// <summary>
        /// 通过Spname获得结果的第一行第一列
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public static string ExecuteScalar(string spName, string dbConnectionString)
        {
            return ExecuteScalar(spName, null, dbConnectionString);
        }

        /// <summary>
        /// 通过SpName和参数列表返回结果值得第一行第一列
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sqlParameter"></param>
        /// <returns></returns>
        public static string ExecuteScalar(string spName, SqlParameter[] sqlParameter, string dbConnectionString)
        {
            string result = string.Empty;
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                if (sqlParameter != null && sqlParameter.Length > 0)
                {
                    command.Parameters.AddRange(sqlParameter);
                }
                try
                {
                    conn.Open();
                    object obj = command.ExecuteScalar();
                    if (obj != null)
                        result = obj.ToString();
                    command.Parameters.Clear();
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList, string dbConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch(Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                   
                }
            }
        }
    }
}
