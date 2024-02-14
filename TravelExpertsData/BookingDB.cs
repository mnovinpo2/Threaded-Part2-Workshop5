using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TravelExpertsData
{
    public class BookingDB
    {
        
        public static List<Booking> getBooking (TravelExpertsContext db, int id)
        {
            List<Booking> booking = db.Bookings.Where(b => b.CustomerId == id).OrderBy(b => b.BookingId).ToList();
            return booking;
        }

        public static void AddBooking(TravelExpertsContext db, Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
        }
      
        public static void DeleteBooking(TravelExpertsContext db, int id)
        {
            int hi = 1;
            Booking? booking = db.Bookings.Find(id);
            BookingDetail? detail = db.BookingDetails.FirstOrDefault(b => b.BookingId == id);
            if (booking != null)
            {
                db.Bookings.Remove(booking);

                if (detail != null)
                {
                    db.BookingDetails.Remove(detail); // booking is a fk for bookingedetails, so need to delete details as well
                }
                db.SaveChanges(); 
                
            }
            
        }

        


        public static Booking? GetBookingById(TravelExpertsContext db, int id)
        {
            
            Booking? booking = db.Bookings.Find(id);
            return booking;
        }

    }
}
