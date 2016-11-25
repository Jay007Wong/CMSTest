using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSTest.BLL;
using CMSTest.Model;

namespace CMSTest.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcessLogin()
        {
            string strUserName = Request["txtUserName"];
            string strPwd = Request["txtPwd"];
            UserInfoBLL userBll = new UserInfoBLL();
            string strLoginSuccess = "ok:登录成功,请稍等...";
            string strLoginFaild = "no:用户名或密码错误,请检查";
            UserInfo userInfo = userBll.GetUserInfo(strUserName, strPwd);
            Session.Add("UserInfo", userInfo);
            string strReturn = userInfo != null ? strLoginSuccess : strLoginFaild;

            return Content(strReturn);
        }
    }
}