using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class BookingDetailDB
    {
        public static List<Customer> GetCustomers(TravelExpertsContext db) // dependency injection
        {
            List<Customer> list = db.Customers.ToList();
            return list;
        }

        public static List<BookingDetail> GetBookingDetail(TravelExpertsContext db, int id) // dependency injection
        {
            
            //List<BookingDetail> list = db.BookingDetails.Where(b => b.BookingId == id).ToList();
            //return list;

            List<BookingDetail> list = db.BookingDetails.Where(b => b.BookingId == id).ToList();
            return list;

        }

        public static List<BookingDetail> GetPrice(TravelExpertsContext db, int id) // dependency injection
        {

            List<BookingDetail> list = db.BookingDetails.Where(b => b.BookingId == id).ToList();
            return list;

        }
        public static int GetTotalPrice(TravelExpertsContext db, int CustomerId)
        {
            int price = 0;
            // customerId called before this
            List<Booking> bookings = db.Bookings.Where(b => b.CustomerId == CustomerId).OrderBy(b => b.BookingId).ToList();
            foreach (Booking booking in bookings)
            {
                
                List<BookingDetail> bookingDetails = GetBookingDetail(db, booking.BookingId);

                foreach (BookingDetail detail in bookingDetails)
                {
                    if (detail.BasePrice != null)
                    {
                        price += (int)detail.BasePrice;
                    }
                    if (detail.AgencyCommission != null)
                    {
                        price += (int)detail.AgencyCommission;
                    }
                    if (detail.FeeId != null)
                    {
                        if (detail.FeeId == "BK")
                        {
                            price += 25;
                        }
                        else if (detail.FeeId == "CH")
                        {
                            price += 15;
                        }
                        else if (detail.FeeId == "GR")
                        {
                            price += 100;
                        }
                        else if (detail.FeeId == "NC")
                        {
                            price += 0;
                        }
                        else if (detail.FeeId == "NSF")
                        {
                            price += 25;
                        }
                        else if (detail.FeeId == "RF")
                        {
                            price += 25;
                        }
                        else if (detail.FeeId == "RS")
                        {
                            price += 50;
                        }
                    }
                }
                
            }

            return price;
        }
    }
}
