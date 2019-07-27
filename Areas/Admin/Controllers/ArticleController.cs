using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MVC.Common;
using MVC.Models;
using PagedList;

namespace MVC.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
        WebDBEntities db = new WebDBEntities();
        UserModel u = new UserModel();
        ArticleModel at = new ArticleModel();
        // GET: Admin/Article
        // 
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ArticleModel at = new ArticleModel();
            var model = at.ListAllPagingArticle(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
            //return View(db.sp_Sel_Article());
        }

        // GET: Admin/Article/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Article/Create
        public ActionResult Create()
        {

            //using (WebDBEntities cITYSTATEEntities = new WebDBEntities())
            //{
            //    ViewBag.StateList = cITYSTATEEntities.sp_Sel_Menu().ToList();
            //}
           

            //SetPageViewBag(41);
            SetMenuViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult GetCityList(string stateID)
        {
            //trả về kiểu json khi gọi từ ajax
            List<Page> lstcity = new List<Page>();
            int stateiD = Convert.ToInt32(stateID);
            using (WebDBEntities cITYSTATEEntities = new WebDBEntities())
            {
                // lấy danh sách bảng Page theo MenuId chuyền vào
                lstcity = (cITYSTATEEntities.Pages.Where(x => x.MenuId == stateiD)).ToList<Page>();
            }
            // gặp lỗi lấy dữ liệu ở đây thì thêm Configuration.LazyLoadingEnabled = false; vào file context.cs :
            //public WebDBEntities()
            //: base("name=WebDBEntities")
            //{
            //    Configuration.LazyLoadingEnabled = false;
            //}

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(lstcity);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(FormCollection collection)
        public ActionResult Create(Article collection)
        {
            try
            {
                // TODO: Add insert logic here
                // gom các lỗi tạo bởi ModelState vào một mảng
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                // Nếu không thành công thì vẫn gọi lại ViewBag để load dữ liệu vào Selectbox
                SetMenuViewBag();

                if (ModelState.IsValid)
                {
                    //db.sp_Ins_Article()
                    // Lấy Danh sách Session User
                    var session = (UserLogin)Session[CommonSession.USER_SESSION];
                    // Lấy UserId từ Session
                    int UserId = session.UserID;
                    //int User = u.GetUserId(se);
                    collection.UserId = UserId;

                    int kt = db.sp_Ins_Article(collection.CategoryId, UserId, collection.Title, collection.Discription, collection.body, "ảnh", "22/6/2019", collection.Show
                        , collection.isHot, "", "", "", 0, "Url-title", "");
                    //db.Articles.Add(collection);
                    //db.SaveChanges();
                    if (kt == 1)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError("", "Có lỗi trong quá trình thêm CDSL !");
                        return View(collection);
                    }
                }
                //SetPageViewBag();
               
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

       

        // GET: Admin/Article/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Article/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        // GET: Admin/Article/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            dynamic showMessageString = string.Empty;
            
            try
            {
                //bool kt = at.DeleteArticle(id);
                bool kt = false;
                if (kt)
                {
                    showMessageString = new
                    {
                        param1 = 1,
                        param2 = "Xóa thành công!"
                    };
                }
                else
                {
                    showMessageString = new
                    {
                        param1 = 2,
                        param2 = "Có lỗi trong quá trình xóa CSDL !"
                    };
                }
                return Json(showMessageString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message.ToString();
                showMessageString = new
                {
                    param1 = 404,
                    param2 = "Lỗi code, liên hệ admin" + errorMsg
                };
                return Json(showMessageString, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("Index");
        }

        // POST: Admin/Article/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Hàm

        public void SetMenuViewBag(long? Selected = null)
        {
            // Hàm này load dữ liệu từ bảng Menu rồi đổ dữ liệu vào selectlist thông qua ViewBag
            // Nếu chuyền một Id của menu vào biến Selected thì nó sẽ select danh mục đó
            //ViewBag.Page = new SelectList(db.sp_Sel_Menu(), "Id", "MenuName", Selected);
            //ViewBag.MenuCate = new SelectList(db.sp_Sel_Menu().ToList(), "Id", "MenuName", Selected);

            List<SelectListItem> selectlist = new List<SelectListItem>();
            SelectListItem select = new SelectListItem
            {
                Text = "--Chọn chuyên mục--",
                Value = "0",
                Selected = false,
            };
            selectlist.Add(select);
            foreach (Menu me in db.Menus)
            {
                select = new SelectListItem
                {
                    Text = me.MenuName,
                    Value = me.Id.ToString(),
                    // Nếu biến Selected truyền vào bằng với Id thì select menu đó
                    Selected = me.Id == Selected ? true : false,
                };
                selectlist.Add(select);
            }
            ViewBag.MenuCate = selectlist;

            // Gọi Droplist Page
            SetPageViewBag();

        }

        public void SetPageViewBag(long? Selected = null, int? MenuID = null)
        {

            // Hàm này load dữ liệu từ bảng Page rồi đổ dữ liệu vào selectlist thông qua ViewBag
            // Nếu chuyền một Id của menu vào biến Selected thì nó sẽ select danh mục đó
            if (MenuID == null)
            {
                //ViewBag.CategoryId = new SelectList(db.sp_Sel_Pages(), "Id", "Category", Selected);
                // tạo một list rỗng
                ViewBag.CategoryId = new SelectList(new List<string>());
            }
            else
                ViewBag.CategoryId = new SelectList(db.sp_Sel_Where_Pages(MenuID), "Id", "Category", Selected);

        }

        #endregion
    }
}
