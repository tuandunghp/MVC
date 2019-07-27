using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    
    public class UserModel
    {
        WebDBEntities db = new WebDBEntities();

        // Trả về UserId khi truyền vào LoginName
        public int GetUserId(string UserName)
        {
            var result = db.Users.SingleOrDefault(x => x.LoginName == UserName);
            if (result == null)
                return 0;
            else
                return result.UserId;
        }

        // Lấy bảng User theo LoginName truyền vào
        public User GetUserBy_LoginName(string LoginName)
        {
            return db.Users.SingleOrDefault(x => x.LoginName == LoginName);
        }
    }
    
}