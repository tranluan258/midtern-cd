﻿using Midterm_Chuyende.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Midterm_Chuyende.Controllers
{
    public class AccountController : Controller
    {
        MidternEntities db = new MidternEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String name, String password)
        {
            if (name == "")
            {
                ViewBag.Error = "Vui lòng nhập tên đăng nhập";
                return View();
            }
            else if (password == "")
            {
                ViewBag.Error = "Vui lòng nhập password";
                return View();
            }
            else if (password.Length < 5)
            {
                ViewBag.Error = "Password ít nhất 5 ký tự";
                return View();
            }
            var result = db.Accounts.FirstOrDefault(u => u.username == name && u.password == password);
            if (result != null)
            {
                Session["Account"] = result.username;
                FormsAuthentication.SetAuthCookie(result.username, false);
                var ticket = new FormsAuthenticationTicket(1, result.username, DateTime.Now, DateTime.Now.AddDays(1), false, "");
                var encrypt = FormsAuthentication.Encrypt(ticket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                HttpContext.Response.Cookies.Add(authCookie);
                return Redirect("/Home/Index");
            }
            ViewBag.Error = "Sai tên tai khoản hoặc mật khẩu";
            return View();
        }
    }
}