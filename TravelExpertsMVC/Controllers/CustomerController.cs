using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExpertsData;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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

            // GET: CustomerController/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
