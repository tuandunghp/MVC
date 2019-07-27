using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        WebDBEntities db = new WebDBEntities();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            //var menu = new BLL.MenuBLL().Menus();
            //var model = menu.AsEnumerable();

            return View(db.sp_Sel_Article());
        }
    }
}