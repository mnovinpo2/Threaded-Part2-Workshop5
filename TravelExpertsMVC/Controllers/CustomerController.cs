using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExpertsData;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TravelExpertsMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: SlipController 
        private TravelExpertsContext db { get; set; }

        //added constructor
        public CustomerController(TravelExpertsContext db)
        {
            this.db = db;
        }
        // GET: CustomerController
        public ActionResult Register(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Redirect("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error", ex.Message);
                return Redirect("/");
            }
        }
        /// <summary>
        /// Controller for Customer Profile 
        /// Written By Mustafa 
        /// </summary>
        /// <returns>Customer Details View</returns>
        [Authorize]
        public ActionResult Profile()
        {
            Customer? cust = null!;
            try
            {
                int? id = HttpContext.Session.GetInt32("CustomerId")!;

                if (id != null) // get id from session if not null
                    cust = CustomerDB.FindByID(this.db, (int)id);
            }
            catch
            {
                return View();
            }
            return View(cust);
        }
		/// <summary>
		/// Controller for customer edit page
		/// Written By Mustafa 
		/// </summary>
		/// <param name="id">id of customer</param>
		/// <returns>edit view with inputs filled in</returns>
		[Authorize]
        public ActionResult Edit(int id)
        {
            Customer? cust = null!;
            try
            {
                cust = CustomerDB.FindByID(this.db, id);
            }
            catch
            {
                return View();
            }
            return View(cust);
        }
		/// <summary>
		/// Controller for updateing the customer
		/// Written By Mustafa 
		/// </summary>
		/// <param name="cus">Customer to Update</param>
		/// <returns>Profile view with updated details</returns>
		[Authorize]
        [HttpPost]
        public ActionResult Edit(Customer cus)
        {
            try
            {
                CustomerDB.UpdateCustomer(this.db, cus);
            }
            catch
            {
                return View();
            }
            return View("Profile",cus);
        }
    }
}
