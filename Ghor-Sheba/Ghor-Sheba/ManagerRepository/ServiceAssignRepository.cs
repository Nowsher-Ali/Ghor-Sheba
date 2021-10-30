using Ghor_Sheba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghor_Sheba.ManagerRepository
{
    public class ServiceAssignRepository
    {
        static ShebaDbEntities db;

        static ServiceAssignRepository()
        {
            db = new ShebaDbEntities();
        }

        public static void PlaceAssignServiceProvider(int bId)
        {
            Booking_confirms bc = new Booking_confirms();

            bc.booking_id = bId;
            bc.service_provider_id = 3;
            bc.status = "pending";

            db.Booking_confirms.Add(bc);
            db.SaveChanges();
        }

        public static List<Booking_confirms> MyServiceProviderAssign(int id)
        {
            //return db.orders.Where(e => e.id == id).ToList();

            var orders = (from e in db.Booking_confirms
                          where e.booking_id == id
                          select e);

            return orders.ToList();
        }
    }
}