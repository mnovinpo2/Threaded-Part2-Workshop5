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
        public static void AddDetails(TravelExpertsContext db, int? PackageId, int newbookingId)
        {
            string destination = "";
            string regionCodeId = "";
            decimal basePrice = 0m;
            decimal agencyComm = 0m;
            string newFeeId = "";
            int prodsup = 0;
            Package package = db.Packages.FirstOrDefault(p => p.PackageId == PackageId);
            Booking booking = db.Bookings.FirstOrDefault(b => b.BookingId == newbookingId);
            if (booking.TravelerCount > 1)
            {
                newFeeId = "GR";
            }
            else
            {
                newFeeId = "BK";
            }
            if (PackageId == 1)
            {
                regionCodeId = "NA";
                destination = "Caribbean Islands";
                basePrice = 4800m;
                agencyComm = 400m;
                prodsup = 49; //cruise
            }
            else if (PackageId == 2)
            {
                regionCodeId = "SP";
                destination = "Polynesian Islands";
                basePrice = 3000m;
                agencyComm = 310m;
                prodsup = 76; // tours
            }
            else if (PackageId == 3)
            {
                regionCodeId = "ASIA";
                destination = "Thailand, Korea, Japan";
                basePrice = 2800m;
                agencyComm = 300m;
                prodsup = 12; // hotel
            }
            else if (PackageId == 4)
            {
                regionCodeId = "EU";
                destination = "France, Italy, Spain";
                basePrice = 3000m;
                agencyComm = 280m;
                prodsup = 39; // travel insurance
            }
            BookingDetail detail = new BookingDetail()
            {
                //Generate rest of the attributes
                BookingId = newbookingId,
                Description = package.PkgDesc,
                Destination = destination,
                TripStart = package.PkgStartDate,
                TripEnd = package.PkgEndDate,
                RegionId = regionCodeId,
                ClassId = "FST",
                BasePrice = basePrice,
                AgencyCommission = agencyComm,
                FeeId = newFeeId,
                ItineraryNo = 123,
                ProductSupplierId = prodsup,
            };

            db.BookingDetails.Add(detail);
            db.SaveChanges();
            
        }


    }
}
