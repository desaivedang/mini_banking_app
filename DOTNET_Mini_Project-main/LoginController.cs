using ExamProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ExamProject.Controllers
{
    public class LoginController : Controller
    {
        MyDBEntities db = new MyDBEntities();
        // GET: Login
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(AccountInfo data)
        {
            AccountInfo user = (from x in db.AccountInfoes where data.Email == x.Email && data.Password == x.Password select x).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie("data.Account_No", false);
                return Redirect("/Home/ShowAccounts");

            }
            else
            {
                ViewBag.message = "Invalid Credentials";
                return View("SignIn");
            }
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return View("SignIn");
        }

    }
}