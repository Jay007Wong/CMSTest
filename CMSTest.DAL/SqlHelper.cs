using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSTest.DAL
{
    public class SqlHelper
    {
        public static readonly string connstr = ConfigurationManager.ConnectionStrings["strdb"].ConnectionString;


        #region ExecuteNonQuery
        /// <summary>
        /// 增加删除修改
        /// </summary>
        /// <param name="sql">sql语句或存储过程</param>
        /// <param name="cmdType">类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, CommandType cmdType, params SqlParameter[] param)
        {
            int r = -1;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = cmdType;

                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    r = cmd.ExecuteNonQuery();
                }
            }

            return r;
        }
        #endregion

        #region ExecuteDataTable
        /// <summary>
        /// 执行查询，返回Datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }

            return dt;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行查询返回首行首列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, CommandType cmdType, params SqlParameter[] param)
        {
            object o = null;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    o = cmd.ExecuteScalar();

                }
            }

            return o;
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行查询返回SqlDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">类型</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType, params SqlParameter[] param)
        {

            SqlConnection conn = new SqlConnection(connstr);

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = cmdType;
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return sdr;
                } catch
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }

            }

        }
        #endregion
    }
}
