using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBI.Metrix.DataAccess
{
    public static class SQLHelper
    {
        public static string GbdatabaseConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["gbdatabase"].ConnectionString;
            }
        }

        public static string GbinfosystemConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["gbinfosystem"].ConnectionString;
            }
        }

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
        public static void ExecuteStoredProcedure(string spName, SqlParameter[] sqlParameter, string dbConnectionString, CommandType cmdType = CommandType.StoredProcedure)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = cmdType;
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
        /// 执行无参数返回DataTable的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public static DataTable ExecuteStoredProcedureToDataTable(string spName, string dbConnectionString, CommandType cmdType = CommandType.StoredProcedure)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = cmdType;
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
        public static DataTable ExecuteStoredProcedureToDataTable(string spName, SqlParameter[] sqlParameter, string dbConnectionString, CommandType cmdType = CommandType.StoredProcedure)
        {
            using (SqlConnection conn = new SqlConnection(dbConnectionString))
            {
                SqlCommand command = new SqlCommand(spName, conn);
                command.CommandType = cmdType;
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
                catch (Exception ex)
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
                command.Parameters.AddRange(sqlParameter);
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
    }
}
