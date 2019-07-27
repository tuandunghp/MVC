using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Areas.Admin.Models;
using BLL;
using MVC.Common;
using MVC.Models;

namespace MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserModel um = new UserModel();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var result = new UserBLL().exist(model.UserName, model.Password);
            if(result && ModelState.IsValid)
            {
                //SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
                //Session["UserID"] = model.UserId.ToString();
                Session["UserName"] = model.UserName.ToString();
                // lấy bảng user theo LoginName truyền vào
                var user = um.GetUserBy_LoginName(model.UserName);
                var UserSession = new UserLogin();
                //
                UserSession.UserName = user.LoginName;
                UserSession.FullName = user.FullName;
                UserSession.UserID = user.UserId;

                Session.Add(CommonSession.USER_SESSION, UserSession);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(model);
        }
    }
}