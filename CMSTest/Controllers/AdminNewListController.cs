using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSTest.BLL;
using CMSTest.Model;
using System.IO;

namespace CMSTest.Controllers
{
    public class AdminNewListController : Controller
    {
        NewInfoBLL newInfoBll = new NewInfoBLL();
        // GET: AdminNewList
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? Convert.ToInt32(Request["pageIndex"]) : 1;
            int pageSize = 5;
            int pageCount = newInfoBll.GetPageCount(pageSize);

            //获取分页数据
            List<NewInfo> listNewInfo = newInfoBll.GetPageList(pageIndex, pageSize);

            ViewBag.listNewInfo = listNewInfo;
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageCount = pageCount;
            return View();
        }


        public ActionResult GetNewInfoModel()
        {
            int id = Convert.ToInt32(Request["id"]);
            NewInfo newInfoModel = newInfoBll.GetUserInfoModel(id);
            return Json(newInfoModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteNewInfo()
        {
            int id = Convert.ToInt32(Request["id"]);
            int result = newInfoBll.DeleteNewInfo(id);
            return Content(result > 0 ? "ok" : "no");
        }

        public ActionResult ShowAddInfo()
        {
            return View();
        }

        public ActionResult FileUpload()
        {
            HttpPostedFileBase postFile = Request.Files["fileUp"];
            if (postFile == null)
            {
                return Content("no:请选择上传文件");
            } else
            {
                string fileName = Path.GetFileName(postFile.FileName);//文件名
                string fileExt = Path.GetExtension(fileName);
                if (fileExt == ".jpg")
                {
                    string dir = "/attached/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";

                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹

                    string newFileName = Guid.NewGuid().ToString();//新的文件名
                    string fulDir = dir + newFileName + fileExt;
                    postFile.SaveAs(Request.MapPath(fulDir));
                    return Content("ok:" + fulDir);
                } else
                {
                    return Content("no:文件格式错误");
                }
            }
        }



        [ValidateInput(false)]
        public ActionResult AddNewInfo(NewInfo newInfo)
        {
            newInfo.SubDateTime = DateTime.Now;
            return newInfoBll.AddInfo(newInfo) ? Content("ok") : Content("no");
        }
    }
}