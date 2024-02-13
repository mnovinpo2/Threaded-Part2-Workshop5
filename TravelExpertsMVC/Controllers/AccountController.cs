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
        public async Task<IActionResult> LoginAsync(Customer cust)
        {
            try
            {
                Customer cst = CustomerDB.Authenticate(db, cust.Username, cust.Password);

                if (cst == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View();
                }


                HttpContext.Session.SetInt32("CustomerId", cst.CustomerId);

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, cst.Username),
                    new Claim("FirstName", cst.CustFirstName),
                    new Claim("LastName", cst.CustLastName),
                    new Claim("CustomerId", cst.CustomerId.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred during authentication.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("CustomerId");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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