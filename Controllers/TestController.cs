using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            // truyền dữ liệu từ controller đến view có 2 cách
            // cách 1 : dùng ViewBag, TitleString là tự đặt
            //ViewBag.TitleString = "lưu tuấn dũng";
            // ở trang view chỗ nào muốn hiển thị viết : @ViewBag.TitleString

            // cách 2 : 
            // - Tạo 1 class MessageModel trong thư mục Models, sau đó tạo phương thức: public string Message{set; get;}
            // Tiếp theo gán dữ liệu vào phương thước Message :
            var mes = new Models.MessageModel();
            mes.Message = "test mvc";
            return View(mes);
        }
    }
}