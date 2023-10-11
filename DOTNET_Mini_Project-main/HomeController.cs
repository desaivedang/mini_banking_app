using ExamProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamProject.Controllers
{
    
    public class HomeController : BaseController
    {
        MyDBEntities db = new MyDBEntities();
        // GET: Home
        public ActionResult ShowAccounts()
        {
            List<AccountInfo> list = db.AccountInfoes.ToList();
            return View(list);
        }



        [HttpGet]
        public ActionResult Transaction(int id)
        {

            AccountInfo user = (from x in db.AccountInfoes where x.Account_No == id select x).First();
            return View(user);
        }

        [HttpPost]
        public ActionResult Transaction(AccountInfo receiver,int id)
        {
            AccountInfo sender = (from s in db.AccountInfoes where s.Account_No == id select s).First();

            AccountInfo receiverr = (from r in db.AccountInfoes where r.Account_No == receiver.Account_No select r).FirstOrDefault();

            if (receiverr == null)
            {
                ViewBag.message1 = "Receiver Account Number is Wrong";
                return View(sender);
            }
            else {
                
            if (sender.Balance > receiver.Balance)
            { 
                  sender.Balance=sender.Balance-receiver.Balance;
                  receiverr.Balance=receiverr.Balance+receiver.Balance;
                    db.SaveChanges();
                    return Redirect("/Home/ShowAccounts");
            }
                else 
                {
                    ViewBag.message1 = "Sender Account has insufficient Funds";
                    return View(sender);
                }
            }
        }
    }
}