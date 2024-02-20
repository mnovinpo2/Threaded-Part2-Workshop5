using TravelExpertsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TravelExpertsMVC.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TravelExpertsMVC.Controllers
{
    public class ContactController : Controller
    {
        private TravelExpertsContext db { get; set; }

        public ContactController(TravelExpertsContext db)
        {
            this.db = db;
        }

        public ActionResult Index() // Responds to HTTP GET requests to the Index action.
        {
            List<Agent> agents = AgentDB.GetAllAgents(db);
            return View(agents); // Passes the list of agents to the "Index" view.
        }

        public ActionResult AgentsByAgency(string id = "All") // Responds to HTTP GET requests to the AgentsByAgency action.
        {
            List<Agency> agencies = AgentDB.GetAgencies(db);
            var list = new SelectList(agencies, "AgencyId", "AgncyCity").ToList();
            list.Insert(0, new SelectListItem("All", "All"));
            ViewBag.Agencies = list;

            // Declare agencyInfo outside the conditional statement
            Agency agencyInfo = null;

            // Check if id is "All" and handle it accordingly
            if (id != "All") // If the provided id is not "All," it fetches information about the selected agency using AgentDB.GetAgencyInfo.
            {
                foreach (var item in list)
                {
                    if (item.Value == id)
                    {
                        item.Selected = true;
                        break;
                    }
                }

                agencyInfo = AgentDB.GetAgencyInfo(db, Convert.ToInt32(id));
            }

            List<Agent> agents;
            if (id == "All")
            {
                agents = AgentDB.GetAllAgents(db);
                agencyInfo = new Agency();
            }
            else
            {
                agents = AgentDB.GetAgentsByAngecy(db, Convert.ToInt32(id));
            }

            // Create an instance of AgencyInfoViewModel and set the properties
            var viewModel = new AgencyInfoViewModel
            {
                Agency = new List<Agency>(),
                Agents = agents
            };

            if (id != null && id.ToLower() != "all")
            {
                viewModel.Agency.Add(agencyInfo);
                viewModel.SelectedAgencyId = Convert.ToInt32(id);
            }

            // Pass the viewModel to the view
            return View("AgentsByAgency", viewModel); //Creates an instance of AgencyInfoViewModel to pass both the agency and agents to the view.
        }
        [HttpGet]
        public IActionResult GetAgencyInfo(int? id) // Responds to HTTP POST requests to the AgencyInfo action.
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedAgency = db.Agencies.FirstOrDefault(a => a.AgencyId == id); // Accepts an id parameter representing the agency ID.

            if (selectedAgency == null)
            {
                return NotFound();
            }

            var viewModel = new AgencyInfoViewModel
            {
                Agency = new List<Agency> { selectedAgency },
                Agents = db.Agents.Where(a => a.AgencyId == id).ToList()
            };

            TempData["SelectedAgencyId"] = (int)id;  // Stores the selected agency ID in TempData for later use.
            return View(viewModel); 
        }

        [HttpPost]
        public IActionResult AgencyInfo(int? id) // Responds to HTTP POST requests to the AgencyInfo action.
                                                 // Accepts an id parameter representing the agency ID.
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedAgency = db.Agencies.FirstOrDefault(a => a.AgencyId == id); 

            if (selectedAgency == null)
            {
                return NotFound();
            }

            var viewModel = new AgencyInfoViewModel // Creates an instance of AgencyInfoViewModel to pass both the agency and agents to the view.
            {
                Agency = new List<Agency> { selectedAgency }, 
                Agents = db.Agents.Where(a => a.AgencyId == id).ToList()
            };

            TempData["SelectedAgencyId"] = (int)id; // Explicitly cast id to int
            return View("AgentsByAgency", viewModel);
        }
    }
}
