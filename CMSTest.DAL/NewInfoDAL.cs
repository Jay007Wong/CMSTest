using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSTest.Model;
using System.Data;
using System.Data.SqlClient;

namespace CMSTest.DAL
{
    public class NewInfoDAL
    {
        #region 获取信息列表集合
        /// <summary>
        /// 获取信息列表集合
        /// </summary>
        /// <param name="start">起始条数</param>
        /// <param name="end">结束条数</param>
        /// <returns></returns>
        public List<NewInfo> GetPageList(int start, int end)
        {
            string sql = "SELECT * FROM (SELECT *,ROW_NUMBER() OVER(ORDER BY Id) AS rownum FROM T_NewInfo) AS t WHERE t.rownum BETWEEN @start AND @end";

            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@start",SqlDbType.Int),
                new SqlParameter("@end",SqlDbType.Int)
        };
            param[0].Value = start;
            param[1].Value = end;

            DataTable dtNewInfo = SqlHelper.ExecuteDataTable(sql, CommandType.Text, param);

            List<NewInfo> listNewInfo = new List<NewInfo>();
            if (dtNewInfo.Rows.Count > 0)
            {
                foreach (DataRow dr in dtNewInfo.Rows)
                {
                    NewInfo newInfo = new NewInfo();
                    LoadEntity(newInfo, dr);
                    listNewInfo.Add(newInfo);
                }
            }

            return listNewInfo;
        }
        #endregion

        #region 总记录数
        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            string sql = "select count(*) from T_NewInfo";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
        }
        #endregion

        #region 根据ID获取NewInfo对象
        /// <summary>
        /// 根据ID获取NewInfo对象
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>NewInfo对象</returns>
        public NewInfo GetUserInfoModel(int id)
        {
            string sql = "SELECT Id, Title, Msg, ImagePath, Author, SubDateTime FROM T_NewInfo where Id=@id";

            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@id",SqlDbType.Int)
            };
            param[0].Value = id;

            DataTable dtNewInfo = SqlHelper.ExecuteDataTable(sql, CommandType.Text, param);

            NewInfo newInfo = new NewInfo();
            if (dtNewInfo.Rows.Count > 0)
            {
                LoadEntity(newInfo, dtNewInfo.Rows[0]);
            }

            return newInfo;

        }
        #endregion


        #region 删除NewInfo对象
        /// <summary>
        /// 删除NewInfo对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteNewInfo(int id)
        {
            string sql = "delete from T_NewInfo where Id=@id";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@id",SqlDbType.Int)
            };
            param[0].Value = id;
            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, param);

            return result;
        }
        #endregion


        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="newInfo">对象</param>
        /// <returns>增加数目</returns>
        public int AddInfo(NewInfo newInfo)
        {
            string sql = "INSERT INTO T_NewInfo( Title ,Msg ,ImagePath ,Author ,SubDateTime) VALUES  (@title,@msg,@imagePath,@author,@subDateTime)";

            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@title",SqlDbType.NVarChar),
                new SqlParameter("@msg",SqlDbType.NVarChar),
                new SqlParameter("@imagePath",SqlDbType.NVarChar),
                new SqlParameter("@author",SqlDbType.NVarChar),
                new SqlParameter("@subDateTime",SqlDbType.DateTime)
            };

            param[0].Value = newInfo.Title;
            param[1].Value = newInfo.Msg;
            param[2].Value = newInfo.ImagePath;
            param[3].Value = newInfo.Author;
            param[4].Value = newInfo.SubDateTime;

            int num = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, param);

            return num;
        }
        #endregion


        #region LoadEntity
        private void LoadEntity(NewInfo newInfo, DataRow dr)
        {

            newInfo.Id = Convert.ToInt32(dr["Id"]);
            newInfo.Title = dr["Title"].ToString();
            newInfo.Msg = dr["Msg"].ToString();
            newInfo.ImagePath = dr["ImagePath"].ToString();
            newInfo.Author = dr["Author"].ToString();
            newInfo.SubDateTime = Convert.ToDateTime(dr["SubDateTime"]);
        }
        #endregion
    }
}
