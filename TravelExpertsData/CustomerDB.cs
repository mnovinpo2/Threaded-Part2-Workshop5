using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class CustomerDB
    {
        public static Customer Authenticate(TravelExpertsContext db, string username, string password)
        {
            var customer = db.Customers.SingleOrDefault(cst => cst.Username == username && cst.Password == password);
            return customer;
        }
    }
}