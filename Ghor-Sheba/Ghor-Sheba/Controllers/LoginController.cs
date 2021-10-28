using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Ghor_Sheba.Models;

namespace Ghor_Sheba.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (User.Identity.Name == "") {
                return View();
            }
           
            var db = new ShebaDbEntities();
            var data = User.Identity.Name;
            var user = new JavaScriptSerializer().Deserialize < LoginUser >(data.ToString());
            if(user.user_type=="Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var db = new ShebaDbEntities();
            var user = (from data in db.LoginUsers
                        where data.email == email && data.password == password
                        select data).FirstOrDefault();


            if(user!=null)
            {
                if(user.user_type=="Admin")
                {
                    string json = new JavaScriptSerializer().Serialize(user);
                    FormsAuthentication.SetAuthCookie(json, true);
                    return RedirectToAction("Index", "Admin");
                }

            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult Signup()
        {
            return View();
        }
    }
}