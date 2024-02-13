using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class TravelProductController : Controller
    {
        private const string CharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private TravelExpertsContext? db { get; set; }
       
        public TravelProductController(TravelExpertsContext db)
        {
            this.db = db;
        }

        public IActionResult Bookings()
        {
            ViewBag.RandomGen = GenerateRandomString(7);
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

        // Get
        public ActionResult Delete(int id)
        {
            //as for confirm to delete
            
            Booking? booking = BookingDB.GetBookingById(db!, id);
            
            return View(booking);
        }

        // Post
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Booking booking)
        {
            try
            {
                BookingDB.DeleteBooking(db!, id);
                return RedirectToAction("Bookings");
            }
            catch
            {
                return View(booking);
            }
        }


        public static string GenerateRandomString(int length)
        {

            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(CharSet.Length);
                stringBuilder.Append(CharSet[index]);
            }

            return stringBuilder.ToString();
        }
        

        

        

        // GET Package
        public ActionResult BookPackage()
        {
            ViewBag.RandomGen = GenerateRandomString(7);
            var customerIdClaim = User.FindFirst("CustomerId");
            if (customerIdClaim == null || !int.TryParse(customerIdClaim.Value, out int customerId))
            {
                return RedirectToAction("Error");
            }
            HttpContext.Session.SetString("BookingDate", DateTime.Now.ToString());
            ViewBag.CustomerId = customerId;
            ViewBag.BookingDate = DateTime.Now;
            List<Package> packages = PackageDB.GetPackages(db!);
            var list = new SelectList(packages, "PackageId", "PkgName").ToList();
            ViewBag.Packages = list;
            Booking booking = new Booking();
            return View(booking);
        }

        //Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookPackage(Booking newBooking)
        {
            try
            {
                
                List<Package> packages = PackageDB.GetPackages(db!);
                var list = new SelectList(packages, "PackageId", "PkgName").ToList();
                ViewBag.Packages = list;

                if (ModelState.IsValid)
                {
                    BookingDB.AddBooking(db!, newBooking);
                    BookingDetailDB.AddDetails(db!, newBooking.PackageId, newBooking.BookingId);
                    return RedirectToAction("Bookings"); 
                }
                else
                {
                    return View(newBooking);
                }
            }
            catch
            {
                return View(newBooking);
            }
        }

    }
}
