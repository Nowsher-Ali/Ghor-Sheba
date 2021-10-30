﻿using Ghor_Sheba.ManagerRepository;
using Ghor_Sheba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ghor_Sheba.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manager
        public ActionResult Service_List()
        {
            var p = ServiceRepository.GetAll();

            return View(p);
        }

        [HttpGet]
        public ActionResult Service_Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Service_Create(Service s)
        {
            var db = new ShebaDbEntities();
            db.Services.Add(s);
            db.SaveChanges();

            return RedirectToAction("Service_List");
        }

        [HttpGet]
        public ActionResult Service_Edit(int id)
        {
            var db = new ShebaDbEntities();
            var product = (from p in db.Services
                           where p.id == id
                           select p).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public ActionResult Service_Edit(Service pro)
        {
            var db = new ShebaDbEntities();

            /* product.Name = pro.Name*/

            var product = (from p in db.Services
                           where p.id == pro.id
                           select p).FirstOrDefault();
            db.Entry(product).CurrentValues.SetValues(pro);
            db.SaveChanges();

            return RedirectToAction("Service_List");
        }

        //==========================================================
        //Booking View Page

        public ActionResult Booking_List()
        {
            var p = BookingRepository.GetAll();

            return View(p);
        }

        [HttpGet]
        public ActionResult Booking_Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Booking_Create(Booking s)
        {
            var db = new ShebaDbEntities();
            db.Bookings.Add(s);
            db.SaveChanges();

            return RedirectToAction("Booking_List");
        }

        [HttpGet]
        public ActionResult Booking_Edit(int id)
        {
            var db = new ShebaDbEntities();
            var product = (from p in db.Bookings
                           where p.id == id
                           select p).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public ActionResult Booking_Edit(Booking pro)
        {
            var db = new ShebaDbEntities();

            /* product.Name = pro.Name*/

            var product = (from p in db.Bookings
                           where p.id == pro.id
                           select p).FirstOrDefault();
            db.Entry(product).CurrentValues.SetValues(pro);
            db.SaveChanges();

            return RedirectToAction("Booking_List");
        }


        //==========================================================
        //Service Provider View Page

        public ActionResult Service_Provider_List()
        {
            var p = ServiceProviderRepository.GetAll();

            return View(p);
        }

        public ActionResult AddToCart(int id)
        {
            var spr = ServiceProviderRepository.Get(id);

            List<ServiceProviderModel> spm;

            if (Session["cart"] == null)
            {
                spm = new List<ServiceProviderModel>();
            }
            else
            {
                var json = Session["cart"].ToString();
                spm = new JavaScriptSerializer().Deserialize<List<ServiceProviderModel>>(json);
            }
            spm.Add(spr);

            var json2 = new JavaScriptSerializer().Serialize(spm);
            Session["cart"] = json2;

            return RedirectToAction("Service_Provider_List", "Manager");
        }

        public ActionResult Cart()
        {
            var json = Session["cart"].ToString();
            var spm = new JavaScriptSerializer().Deserialize<List<ServiceProviderModel>>(json);

            return View(spm);
        }

        public ActionResult Confirm()
        {
            var json = Session["cart"].ToString();
            var spm = new JavaScriptSerializer().Deserialize<List<ServiceProviderModel>>(json);

            var bid = 1; //User.Identity.Name => I pencipal class instance from cookei file

            ServiceAssignRepository.PlaceAssignServiceProvider(bid);

            Session.Remove("cart");

            return RedirectToAction("Service_Provider_List");
        }

        public ActionResult Service_Provider_Assign()
        {
            var bId = 1; //User.Identity.Name

            var sps = ServiceAssignRepository.MyServiceProviderAssign(bId);

            return View(sps);
        }
    }
}