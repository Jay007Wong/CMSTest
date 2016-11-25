using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSTest.Model
{
    public class UserInfo
    {
        public int Uid { get; set; }
        public string UUserName { get; set; }
        public string UPwd { get; set; }
        public string UEmail { get; set; }
        public DateTime URegisterTime { get; set; }
    }
}
