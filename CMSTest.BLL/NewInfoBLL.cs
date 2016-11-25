using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSTest.DAL;
using CMSTest.Model;

namespace CMSTest.BLL
{
    public class NewInfoBLL
    {
        NewInfoDAL newInfoDal = new NewInfoDAL();

        public List<NewInfo> GetPageList(int pageIndex, int pageSize)
        {
            int start = pageSize * (pageIndex - 1) + 1;
            int end = pageSize * pageIndex;
            List<NewInfo> newInfo = newInfoDal.GetPageList(start, end);
            return newInfo;
        }

        #region 获取总页数
        public int GetPageCount(int pageSize)
        {
            int recordCount = newInfoDal.GetRecordCount();
            int pageCount = Convert.ToInt32(Math.Ceiling(recordCount * 0.1 / pageSize));
            return pageCount;
        }
        #endregion

        public NewInfo GetUserInfoModel(int id)
        {
            return newInfoDal.GetUserInfoModel(id);
        }

        public int DeleteNewInfo(int id)
        {
            return newInfoDal.DeleteNewInfo(id);
        }

        public bool AddInfo(NewInfo newInfo)
        {
            return newInfoDal.AddInfo(newInfo) > 0;
        }
    }
}
