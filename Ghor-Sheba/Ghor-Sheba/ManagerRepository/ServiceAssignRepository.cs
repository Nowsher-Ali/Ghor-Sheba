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

        public static void PlaceAssign(List<ServiceProviderModel> products, int cId)
        {
            Booking o = new Booking();

            o.customer_id = cId;
            o.cost = 200;
            o.payment_status = "pending";
            o.status = "pending";

            db.Bookings.Add(o);
            db.SaveChanges();

            foreach (var p in products)
            {
                var od = new Booking_details()
                {
                    booking_id = p.id,
                    service_id = p.service_provider_id
                };
                db.Booking_details.Add(od);
                db.SaveChanges();
            }
        }

        public static List<Booking> ServiceProviderAssign(int id)
        {
            //return db.orders.Where(e => e.id == id).ToList();

            var orders = (from e in db.Booking_confirms
                          where e.booking_id == id
                          select e);

            return orders.ToList();
        }
    }
}