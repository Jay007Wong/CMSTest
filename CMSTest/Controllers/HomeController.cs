using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSTest.BLL;
using CMSTest.Model;

namespace CMSTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UserInfo userInfo = Session["UserInfo"] as UserInfo;
            if (userInfo == null)
            {
                return Redirect("/Login/Index");
            } else
            {
                ViewBag.userLogin = userInfo.UUserName;
                return View();
            }

        }
    }
}