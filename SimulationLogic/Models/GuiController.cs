using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLogic
{
    public class GuiController : IGuiController
    {
        /// <summary>
        /// Implements <see cref="IGuiController.UpdateUI(Supermarket, string[], List{string}[], ISupermarketStatistics)"/>
        /// </summary>
        public void UpdateUI(Supermarket supermarket, string[] queueLabels, List<string>[] queueOfCustomers, ISupermarketStatistics supermarketStatistics)
        {

            supermarketStatistics.LongestLine = supermarket.LongestLine; // shows the longest line from supermarket 
            supermarketStatistics.CustomersArrived = supermarket.CustumersArrived; // shows the customers that have arrived in supermarket 
            supermarketStatistics.CustomersDeparted = supermarket.CustomersServed; // shows the customers that have been surved in the  supermarket 
            supermarketStatistics.AverageCustomerTotal = Convert.ToDecimal(supermarket.AverageCustomerTotal); // shows the average total for customers in supermarket also converts double to decimal
            supermarketStatistics.MaximumCustomerTotal = Convert.ToDecimal(supermarket.MaximumCustomerTotal); // shows the maximum total for customers in supermarket also converts double to decimal
            supermarketStatistics.MinimumCustomerTotal = Convert.ToDecimal(supermarket.MinimumCustomerTotal); // shows the minimum total for customers supermarket also converts double to decimal
            
            supermarketStatistics.TotalSales = Convert.ToDecimal(supermarket.TotalSales); // shows the total salse for the supermarket also converts double to decimal

            for (int i = 0; i < supermarket.NumOfReg; i++) // loops through the registers of th supermarket
            {
                queueLabels[i] = Convert.ToString(i + 1) + ":" + supermarket.NumOfItemInCart.ToString(); // sets the name of the registers and shows how many items are in the curent customers cart
            }
            
        }
    }
}
