using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class PackageDB
    {
        public static List<Package> GetPackages(TravelExpertsContext db)
        {
            List<Package> packages = db.Packages.ToList();
            return packages;
        }
       
    }
}
