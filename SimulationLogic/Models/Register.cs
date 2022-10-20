using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLogic
{
    /// <summary>
    /// sets up the interface for the line
    /// </summary>
    interface line
    {
        void JoinLine(Customer customer); // method to add a customer to line
        Customer CheckOut(); // method to remove customer from line
    }
    /// <summary>
    /// sets up the regester class
    /// </summary>
    internal class Register
    {
        public int ID; // register ID
        public Queue<Customer> line = new Queue<Customer>(); // queue line 
        public double TotalSales; // double to count total sales
        public int CustomersServerd; // int to count customers served
        /// <summary>
        /// method to add customes to line
        /// </summary>
        /// <param name="customer"></param>
        public void JoinLine(Customer customer)
        {
            line.Enqueue(customer); // adds customers to line
        }
        /// <summary>
        /// method to remove customers from line
        /// </summary>
        /// <returns></returns>
        public Customer CheckOut()
        {
            return line.Dequeue(); // removes customers from line
        }
    }
}
