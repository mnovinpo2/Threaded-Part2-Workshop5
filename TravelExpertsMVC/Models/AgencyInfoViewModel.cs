using TravelExpertsData;

namespace TravelExpertsMVC.Models
{
    public class AgencyInfoViewModel
    {
        public List<Agency> Agency { get; set; }
        public List<Agent> Agents { get; set; }
        public int SelectedAgencyId { get; set; } // Added property
    }
}