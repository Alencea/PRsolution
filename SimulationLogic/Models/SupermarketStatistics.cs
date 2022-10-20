using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLogic.Models
{
    /// <summary>
    /// Implements <see cref="ISuperMarketStatistics"/>
    /// </summary>
    public class SuperMarketStatistics : ISupermarketStatistics
    {
        public int LongestLine { get; set; }
        public int CustomersArrived { get; set; }
        public int CustomersDeparted { get; set; }
        public decimal AverageCustomerTotal { get; set; }
        public decimal MinimumCustomerTotal { get; set; }
        public decimal MaximumCustomerTotal { get; set; }
        public decimal TotalSales { get; set; }
    }
}
