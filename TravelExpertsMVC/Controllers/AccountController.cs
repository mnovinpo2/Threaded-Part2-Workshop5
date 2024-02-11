using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class AccountController : Controller
    {
        private TravelExpertsContext? db { get; set; }
        
        public AccountController(TravelExpertsContext db)
        {
            this.db = db;
        }
        public IActionResult Login(string returnUrl = "")
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer cust) // data collected from the form
        {
            Customer cst = CustomerDB.Authenticate(db, cust.Username, cust.Password);
            if (cst == null) // no matching username/password
            {
                return View();
            }
            //TempData["CustomerId"] = cst.ID;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cst.Username),
                new Claim("FirstName", cst.CustFirstName),
                new Claim("LastName", cst.CustLastName),
                new Claim("CustomerId", cst.CustomerId.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme); // cookies authentication
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal);

            string? returnUrl = TempData["ReturnUrl"]?.ToString();
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("CustomerId");
            HttpContext.Session.Clear();
            return RedirectToAction( "Index", "Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Redirect("Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");

            }

        }
    }
}
