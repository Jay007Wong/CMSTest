using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSTest.Model;

namespace CMSTest.DAL
{
    public class UserInfoDAL
    {
        public UserInfo GetUserInfo(string userName, string userPwd)
        {
            string sql = "SELECT uid,UUserName,upwd,UEmail,URegisterTime FROM dbo.T_UserInfo WHERE UUserName=@UUserName and upwd=@upwd";


            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@UUserName",SqlDbType.NVarChar,100),
                new SqlParameter("@upwd",SqlDbType.NVarChar,100)
        };
            param[0].Value = userName;
            param[1].Value = userPwd;
            DataTable dtUserInfo = SqlHelper.ExecuteDataTable(sql, CommandType.Text, param);

            UserInfo user = new UserInfo();
            if (dtUserInfo.Rows.Count > 0)
            {
                user.Uid = Convert.ToInt32(dtUserInfo.Rows[0][0]);
                user.UUserName = dtUserInfo.Rows[0][1].ToString();
                user.UPwd = dtUserInfo.Rows[0][2].ToString();
                user.UEmail = dtUserInfo.Rows[0][3].ToString();
                user.URegisterTime = Convert.ToDateTime(dtUserInfo.Rows[0][4]);
            }

            return user;
        }
    }
}
