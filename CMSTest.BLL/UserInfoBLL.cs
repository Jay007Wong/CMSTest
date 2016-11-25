using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSTest.DAL;
using CMSTest.Model;

namespace CMSTest.BLL
{
    public class UserInfoBLL
    {
        UserInfoDAL userDal = new UserInfoDAL();

        public UserInfo GetUserInfo(string userName, string userPwd)
        {
            return userDal.GetUserInfo(userName, userPwd);
        }
    }
}
