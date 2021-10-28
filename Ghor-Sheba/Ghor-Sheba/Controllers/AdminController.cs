using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Ghor_Sheba.Models;

namespace Ghor_Sheba.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            var data = User.Identity.Name;
            var user = new JavaScriptSerializer().Deserialize<LoginUser>(data.ToString());
            ViewData["username"] = user.username;
            return View();
        }

        public ActionResult Profile()
        {
            var db = new ShebaDbEntities();
            var data = User.Identity.Name;
            var user = new JavaScriptSerializer().Deserialize<LoginUser>(data.ToString());
            ViewData["username"] = user.username;
            ViewData["password"] = user.password;
            ViewData["phone"] = user.phone;
            ViewData["address"] = user.address;
            return View();
        }
        [HttpPost]
        public ActionResult Profile(string username, string password, string phone, string address)
        {
            var data = User.Identity.Name;
            var user = new JavaScriptSerializer().Deserialize<LoginUser>(data.ToString());
            var db = new ShebaDbEntities();
            var res = (from u in db.LoginUsers
                       where u.id == user.id
                       select u).FirstOrDefault();

            res.username = username;
            res.password = password;
            res.phone = phone;
            res.address = address;
            db.SaveChanges();
            FormsAuthentication.SignOut();
            string json = new JavaScriptSerializer().Serialize(res);
            FormsAuthentication.SetAuthCookie(json, true);
            ViewData["username"] = res.username;
            ViewData["password"] = res.password;
            ViewData["phone"] = res.phone;
            ViewData["address"] = res.address;
            return View();
        }
        // Manage Part begins
        public ActionResult ManageManager()
        {
            var data = User.Identity.Name;
            var user = new JavaScriptSerializer().Deserialize<LoginUser>(data.ToString());
            ViewData["username"] = user.username;
            var db = new ShebaDbEntities();

            var managers = (from d in db.LoginUsers
                            where d.user_type == "Manager"
                            select d).ToList();

            return View(managers);
        }

        public ActionResult ManageCustomer()
        {
            var data = User.Identity.Name;
            var user = new JavaScriptSerializer().Deserialize<LoginUser>(data.ToString());
            ViewData["username"] = user.username;
            var db = new ShebaDbEntities();

            var customers = (from d in db.LoginUsers
                             where d.user_type == "Customer"
                             select d).ToList();

            return View(customers);
        }

        public ActionResult ManageServiceProvider()
        {
            var data = User.Identity.Name;
            var user = new JavaScriptSerializer().Deserialize<LoginUser>(data.ToString());
            ViewData["username"] = user.username;
            var db = new ShebaDbEntities();

            var ServiceProviders = (from d in db.LoginUsers
                                    where d.user_type == "ServiceProvider"
                                    select d).ToList();

            return View(ServiceProviders);
        }
        // Manage Part ends
        // Add part begins
        public ActionResult AddManager()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddManager(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var db = new ShebaDbEntities();
                user.user_type = "Manager";
                user.status = "unblocked";

                var temp = (from td in db.LoginUsers
                            where td.email == user.email
                            select td).FirstOrDefault();

                if (temp == null)
                {
                    db.LoginUsers.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("ManageManager", "Admin");
                }
                else
                {
                    ViewData["message"] = "User with this email already exists";
                    return View(user);
                }
            }
            return View(user);
        }

        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var db = new ShebaDbEntities();
                user.user_type = "Customer";
                user.status = "unblocked";

                var temp = (from td in db.LoginUsers
                            where td.email == user.email
                            select td).FirstOrDefault();

                if (temp == null)
                {
                    db.LoginUsers.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("ManageCustomer", "Admin");
                }
                else
                {
                    ViewData["message"] = "User with this email already exists";
                    return View(user);
                }

            }
            return View();
        }

        public ActionResult AddServiceProvider()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddServiceProvider(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var db = new ShebaDbEntities();
                user.user_type = "ServiceProvider";
                user.status = "unblocked";

                var temp = (from td in db.LoginUsers
                            where td.email == user.email
                            select td).FirstOrDefault();

                if (temp == null)
                {
                    db.LoginUsers.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("ManageServiceProvider", "Admin");
                }
                else
                {
                    ViewData["message"] = "User with this email already exists";
                    return View(user);
                }
            }
            return View(user);
        }

        // Add part ends

        // Delete Part begins

        public ActionResult DeleteManager(int id)
        {
            var db = new ShebaDbEntities();
            var user = (from data in db.LoginUsers
                        where data.id == id
                        select data).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult DeleteManager(LoginUser user)
        {
            var db = new ShebaDbEntities();
            var manager = (from data in db.LoginUsers
                           where data.id == user.id
                           select data).FirstOrDefault();
            db.LoginUsers.Remove(manager);
            db.SaveChanges();
            return RedirectToAction("ManageManager", "Admin");
        }

        public ActionResult DeleteCustomer(int id)
        {
            var db = new ShebaDbEntities();
            var user = (from data in db.LoginUsers
                        where data.id == id
                        select data).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult DeleteCustomer(LoginUser user)
        {
            var db = new ShebaDbEntities();
            var customer = (from data in db.LoginUsers
                           where data.id == user.id
                           select data).FirstOrDefault();
            db.LoginUsers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("ManageCustomer", "Admin");
        }

        public ActionResult DeleteServiceProvider(int id)
        {
            var db = new ShebaDbEntities();
            var user = (from data in db.LoginUsers
                        where data.id == id
                        select data).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult DeleteServiceProvider(LoginUser user)
        {
            var db = new ShebaDbEntities();
            var sp = (from data in db.LoginUsers
                            where data.id == user.id
                            select data).FirstOrDefault();
            db.LoginUsers.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("ManageServiceProvider", "Admin");
        }

        // Delete Part ends

        // Edit Part begins
        public ActionResult EditManager(int id)
        {
            var db = new ShebaDbEntities();
            var user = (from data in db.LoginUsers
                        where data.id == id
                        select data).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult EditManager(LoginUser user)
        {
            var db = new ShebaDbEntities();
            var manager = (from data in db.LoginUsers
                           where data.id == user.id
                           select data).FirstOrDefault();
            db.Entry(manager).CurrentValues.SetValues(user);
            db.SaveChanges();
            return View();
        }

        public ActionResult EditCustomer(int id)
        {
            var db = new ShebaDbEntities();
            var user = (from data in db.LoginUsers
                        where data.id == id
                        select data).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult EditCustomer(LoginUser user)
        {
            var db = new ShebaDbEntities();
            var customer = (from data in db.LoginUsers
                           where data.id == user.id
                           select data).FirstOrDefault();
            db.Entry(customer).CurrentValues.SetValues(user);
            db.SaveChanges();
            return View();
        }

        public ActionResult EditServiceProvider(int id)
        {
            var db = new ShebaDbEntities();
            var user = (from data in db.LoginUsers
                        where data.id == id
                        select data).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult EditServiceProvider(LoginUser user)
        {
            var db = new ShebaDbEntities();
            var sp = (from data in db.LoginUsers
                            where data.id == user.id
                            select data).FirstOrDefault();
            db.Entry(sp).CurrentValues.SetValues(user);
            db.SaveChanges();
            return View();
        }
        
        // Edit Part Ends
    }
}