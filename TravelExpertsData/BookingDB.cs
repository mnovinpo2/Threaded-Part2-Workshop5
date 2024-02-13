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

        //public static int getBookingById(TravelExpertsContext db, int bookingid)
        //{
        //    int b = 1;
        //    return b;

        //}

    }
}
