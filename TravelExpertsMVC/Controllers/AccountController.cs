using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExpertsData;
using NuGet.Protocol.Plugins;
using System.Runtime.Intrinsics.X86;

namespace TravelExpertsMVC.Controllers
{
    public class AccountController : Controller
    {
        private TravelExpertsContext? db { get; set; }

        public AccountController(TravelExpertsContext db)
        {
            this.db = db;
        }

        public IActionResult Login(string returnUrl = "") //Responds to HTTP GET requests to the Login action.
                                                          
                                                          
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["ReturnUrl"] = returnUrl; //If a returnUrl is provided, it stores it in TempData for later use.
            }
            return View(); //Renders the associated Login view.
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer cust) // Responds to HTTP POST requests to the Login action.
        {
            try
            {
                Customer cst = CustomerDB.Authenticate(db, cust.Username, cust.Password);

                if (cst == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    TempData["Message"] = "Error, Incorrect Username or Password. Please Try Again.";
                    TempData["IsError"] = true;
                    return View();
                }


                HttpContext.Session.SetInt32("CustomerId", cst.CustomerId); // Stores the authenticated user's ID in the session.

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

                TempData["Message"] = $"Logged in Successfully, hello {cst.Username}";

                return RedirectToAction("Index", "Home"); // Redirects the user to the original requested URL or the home page.
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred during authentication."); // If authentication fails, it adds an error message to the ModelState.
                TempData["Message"] = "Error Logging in, Please Try Again";
                TempData["IsError"] = true;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //Signs the user out using
                                                                                               //CookieAuthenticationDefaults.AuthenticationScheme.
            HttpContext.Session.Remove("CustomerId");
            HttpContext.Session.Clear();
            TempData["Message"] = "Logged Out Successfully";
            return RedirectToAction("Index", "Home"); // Redirects the user to the home page.
        }

        [HttpGet]
        public ActionResult Register() //Responds to HTTP GET requests to the Register action.
        {
            return View(); // Renders the associated Register view.
        }

        [HttpPost]
        public ActionResult Register(Customer customer) // Responds to HTTP POST requests to the Register action.
        {
            try
            {
                db.Customers.Add(customer); // dd the new customer to the database using db.Customers.Add(customer) 
                db.SaveChanges();
                TempData["Message"] = $"Thank You For Registering {customer.CustFirstName}, Please Sign In";
                return Redirect("Login");
            }
            catch (Exception)
            {
                TempData["Message"] = "Error Registering, Please Try Again";
                TempData["IsError"] = true;
                return RedirectToAction("Index", "Home"); // If an error occurs, it redirects the user to the home page with an error message.
            }
        }
    }
}