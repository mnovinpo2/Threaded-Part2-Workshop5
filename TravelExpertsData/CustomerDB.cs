using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class CustomerDB
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Customer Authenticate(TravelExpertsContext db, string username, string password)
        {
            var customer = db.Customers.SingleOrDefault(cst => cst.Username == username && cst.Password == password);
            return customer;
        }

        /// <summary>
        /// Method to find Customer by ID
        /// Written By Mustafa
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="id">Customer Id</param>
        /// <returns>Customer</returns>
        public static Customer FindByID(TravelExpertsContext db, int id)
        {
            Customer? cust = null!;
            try
            {
                cust = db.Customers.Find(id);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cust!;
        }

        /// <summary>
        /// Method for updating Customers Info
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="customer">customer entity to update</param>
        public static void UpdateCustomer(TravelExpertsContext db, Customer customer)
        {
            try
            {
                db.Customers.Update(customer);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}