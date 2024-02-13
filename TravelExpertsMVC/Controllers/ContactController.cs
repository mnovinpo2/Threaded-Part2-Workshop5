using TravelExpertsData;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace TravelExpertsMVC.Controllers
{
    public class ContactController : Controller
    {
        private TravelExpertsContext db { get; set; }

        public ContactController(TravelExpertsContext db) 
        {
            this.db = db;
        }

        public ActionResult Index() 
        {
            List<Agent> agents = AgentDB.GetAllAgents(db);
            return View(agents);
        }

        //[HttpGet]
        public ActionResult AgentsByAgency() 
        {
            List <Agency> agencies = AgentDB.GetAgencies(db);
            var list = new SelectList(agencies, "AgencyId", "AgncyCity").ToList();
            list.Insert(0, new SelectListItem("All", "All"));
            ViewBag.Agencies = list;

            List<Agent> agents = AgentDB.GetAllAgents(db);
            return View(agents);
        }

        [HttpPost]
        public ActionResult AgentsByAgency(string id = "All") 
        {
            List<Agency> agencies = AgentDB.GetAgencies(db);
            var list = new SelectList(agencies, "AgencyId", "AgncyCity").ToList(); 
            list.Insert(0, new SelectListItem("All", "All"));

            foreach (var item in list) 
            {
                if (item.Value == id) 
                {
                    if (item.Value == id) 
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            
            List<Agent> agents;
            if (id == "All")
            {
                agents = AgentDB.GetAllAgents(db);
            }
            else 
            {
                agents = AgentDB.GetAgentsByAngecy(db, Convert.ToInt32(id));
            }
            return View(agents);
        }

    }
}
