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
        public static List<Agent> GetAllAgents(TravelExpertsContext db) 
        {
            List<Agent> agents = db.Agents.ToList();
            return agents;
        }

        public static List<Agency> GetAgencies(TravelExpertsContext db) 
        {
            List<Agency> agencies = db.Agencies.ToList();
            return agencies;
        }

        public static List<Agent> GetAgentsByAngecy(TravelExpertsContext db, int id) 
        {
            List<Agent> agents = db.Agents.Where(a => a.AgencyId == id).ToList();
            return agents;
        }
    }
}
