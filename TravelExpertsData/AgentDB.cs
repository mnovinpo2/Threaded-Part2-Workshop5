using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TravelExpertsData
{
    public static class AgentDB
    {
        public static List<Agent> GetAllAgents(TravelExpertsContext db) // Retrieves a list of all agents from the database and orders them by first name
                                                                        // and then by last name.
        {
            List<Agent> agents = db.Agents.OrderBy(a => a.AgtFirstName).ThenBy(a => a.AgtLastName).ToList();
            return agents;
        }

        public static List<Agency> GetAgencies(TravelExpertsContext db)  // Retrieves a list of all agencies from the database.
        {
            List<Agency> agencies = db.Agencies.ToList();
            return agencies;
        }

        public static List<Agent> GetAgentsByAngecy(TravelExpertsContext db, int id) // Retrieves a list of agents associated with a specific agency
                                                                                     // based on the provided agency ID.Orders the agents by
                                                                                     // first name and then by last name.
        {
            List<Agent> agents = db.Agents.Where(a => a.AgencyId == id).OrderBy(a => a.AgtFirstName).ThenBy(a => a.AgtLastName).ToList();
            return agents;
        }
        public static Agency GetAgencyInfo(TravelExpertsContext db, int? id) // Retrieves information about a specific agency based on the provided agency ID.
        {
            if (id == null)
            {
                return null;
            }

            Agency selectedAgency = db.Agencies.FirstOrDefault(a => a.AgencyId == id);

            return selectedAgency;
        }
    }
}
