using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ArticleModel
    {
        WebDBEntities db = new WebDBEntities();
        public IEnumerable<sp_Search_FullText_Article_Title_Result> ListAllPagingArticle(string searchString, int page, int pageSize)
        {
            // Truyền vào từ khóa tìm kiếm, còn lại sử lý trong stored procedure
            var model = db.sp_Search_FullText_Article_Title(searchString).ToList();
            // nếu search không bằng rỗng, tức là đang search
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    // Tìm kiếm từ khóa nhập vào trong cột Title hoặc cột Discription
            //    model = model.Where(x => x.Title.Contains(searchString) || x.Discription.Contains(searchString)).ToList();
            //}
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
            
        }

        // Xóa bản ghi bằng entity
        public bool DeleteArticle(int Id)
        {
            try
            {
                var at = db.Articles.Find(Id);
                db.Articles.Remove(at);
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        // Kiểm tra LoginName có trong bảng không
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.LoginName == userName) > 0;
        }
    }
}