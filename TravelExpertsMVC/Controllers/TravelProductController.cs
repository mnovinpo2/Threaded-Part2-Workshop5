using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class TravelProductController : Controller
    {

        private TravelExpertsContext? db { get; set; }
        // added constructor
        public TravelProductController(TravelExpertsContext db)
        {
            this.db = db;
        }

        public IActionResult Bookings()
        {
            var customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
            {
                return RedirectToAction("Error");
            }
            ViewBag.Price = BookingDetailDB.GetTotalPrice(db, customerId);
            List<Booking> booking = BookingDB.getBooking(this.db!, customerId);
            return View(booking);
        }

        public IActionResult Details(int BookingId)
        {

            var customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
            {
                return RedirectToAction("Error");
            }

            List<BookingDetail> detail = BookingDetailDB.GetBookingDetail(this.db!, BookingId);
            
            
            ViewBag.Price = BookingDetailDB.GetTotalPrice(db, customerId);
            return View(detail);
        }

        public static int GetPrice()
        {
            int price;
            price = 1;
            return price;
        }

        public ActionResult ListBookings()
        {
            int id = 133; // get customer id from session (logged in)
            List<Booking> bookings = BookingDB.getBooking(db, id);
            return View(bookings);
        }

        public ActionResult ListDetails()
        {
            int id = 133; // get customer id from session (logged in)
            List<BookingDetail> bookings = BookingDetailDB.GetBookingDetail(db, id);
            return View(bookings);
        }

        
    }
}
