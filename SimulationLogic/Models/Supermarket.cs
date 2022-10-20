using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLogic
{
    public class Supermarket : ISupermarket
    {

        Item[] items = new Item[30]; // array of items
        public string[] ItemNames = {"Skim Milk", "1% Milk", "2% Milk", "Whole Milk", "American Cheese",
                "Mozzorela Cheese", "Eggs 6", "Eggs 12", "Canned soda", "Bottled soda", "Steak", "Ground Beef",
                "Chicken Breast", "Ground chicken", "Half Chicken", "Whole Chicken", "Ground Pork", "Pork Chops",
                "Rack of Ribbs", "Baccon", "Turky Beast", "Ground Turky", "Dry Cat Food", "canned Cat Food",
                "Dry Dog Food", "Canned Dog Food", "Bleach", "Window Cleaner", "Chocolet Candy Bar", "Peanut Candy Bar", }; // array of item names
        List<Register> Registers = new List<Register>(); // list of registers
        public int LongestLine = 0; // start of longest line
        public int ShortestLine = 1; // start of shortes line
        public int NumOfReg; // counts the number of registers
        Customer[][] SetOfCustumers = new Customer[10][]; // jagged array of customers
        public int CustumersArrived = 0; // counts customers that have arrived
        public int CustomersServed = 0; // counts customers that have been surved
        public double TotalSales = 0; // counts the total sales
        public double AverageCustomerTotal = 0; // is the average customer total sale
        public double MinimumCustomerTotal = 3000; // is the minimum customer sale
        public double MaximumCustomerTotal = 0; // is the maximum customer sale
        public int NumOfItemInCart; // is the number of items in a customers cart
        /// <summary>
        /// runs the simulation
        /// </summary>
        public void Run()
        {

            for (int i = 0; i < ItemNames.Length; i++) // goes through the items names
            {
                Item item = new Item(); // makes a new item
                item.name = ItemNames[i]; // sets the item name
                item.price = randDouble(.50, 100.00); // gives it a random price
                items[i] = item; // sets it in the array of items
            }

            NumOfReg = rand(5, 16); // gets a random for the number of registers between 5 and 15 
            int NumOfCustomersers = rand(50, 501); // gets a random number of customers between 50 and 500

            for (int i = 1; i <= NumOfReg; i++) // run through the registers
            {
                Registers.Add(openRegister(i)); // adds a register to the list of registers
            }

            int num = NumOfCustomersers; // sets num to the number of customers
            int j = 0; // sets j up
            int RandNum = 0; // sets RandNum up
            for (int i = 0; i < SetOfCustumers.Length; i++) // for loop that goes through SetOfCustmers
            {
                if (num > 0) // if that sees if num is greater than 0
                {
                    RandNum = rand(1, num + 1); //give RandNum a random number between 1 and num+1
                    num -= RandNum; // removes RandNum from num
                    CustumersArrived += RandNum; // adds RandNum to CustumersArrived
                }
                Customer[] NumOfCustumers = new Customer[RandNum]; // sets up a new array of customer
                int tempNum = RandNum; // sets up a temperary number equal to RandNUm
                while (RandNum > 0) // dose the code below while RandNum is greater then zero
                {
                    int NumOfItems = rand(5, 31); // sets up a random number of items between 5 and 30
                    Customer person = new Customer(); // sets up a new custumer person
                    person.ID = j + 1; // gives the person an ID
                    for (int k = 0; k < NumOfItems; k++) // dose code below while k is less the NumOfItems
                    {
                        int randItem = rand(1, 31); // gives a randon number between 1 and 30 to see what item a person is getting
                        person.AddToCart(items[randItem - 1]); // adds item to the persons cart
                    }
                    NumOfCustumers[tempNum - RandNum] = person; // adds person to an array of sustomers
                    j++; // increases j
                    RandNum--;// reduces RandNum
                }
                SetOfCustumers[i] = NumOfCustumers; // adds NumOfCustomers to SetOfCustomers
            }

            foreach (Customer[] custumerSet in SetOfCustumers) // goes through SetOfCustumers
            {
                foreach (Customer customer in custumerSet) // goes through customerSet
                {
                    Registers[FindShortestLine(Registers) - 1].JoinLine(customer); // finds the shortes line and adds the customer to it
                    if (Registers[FindLongestLine(Registers) - 1].line.Count() > LongestLine) // find the longest line
                    {
                        LongestLine = Registers[FindLongestLine(Registers) - 1].line.Count(); // sets longestLine to the longest line
                    }
                    if (Registers[FindShortestLine(Registers) - 1].line.Count() < ShortestLine) // finds the shortest line
                    {
                        ShortestLine = Registers[FindShortestLine(Registers) - 1].line.Count(); // sets ShortestLine to the shortes line
                    }
                }

                for (int i = 0; i < LongestLine; i++) // continous while i is less than the longestLine
                {
                    for (int k = 0; k < Registers.Count; k++) // continous while k is less than the number of registers
                    {
                        if (Registers[k].line.Count > 0) // gets the length of the line at register k
                        {
                            Customer person = Registers[k].CheckOut(); // gets the first person in register ks line
                            double total = 0; // set up the total for the person
                            NumOfItemInCart = person.cart.Count(); // counts the number of items in the persons cart
                            while (person.cart.Count > 0) // continous while the person still has items in their cart
                            {
                                Item thing = person.RemoveFromCart(); // revomes the last item from thier cart
                                Registers[k].TotalSales += thing.price; // adds the price of the item to the registers TotalSales
                                total += thing.price; // adds the price of the item to the total
                            }

                            if (total < MinimumCustomerTotal) // determins if the total is less then the minimum customer total
                            {
                                MinimumCustomerTotal = total; // sets the minimum customer total to total
                             }
                            if (total > MaximumCustomerTotal) // determins if the total is greater then the maximum customer total
                            {
                                MaximumCustomerTotal = total; // sets the maximum customer total to total
                            }
                            Registers[k].CustomersServerd++; // increases the registers CustomersServered
                            CustomersServed++; // increases CustomersServed
                            TotalSales += Registers[k].TotalSales; // intcreases the registers TotalSales
                            AverageCustomerTotal = TotalSales / CustomersServed; // gets teh average of all of the sores sales
                        }

                    }
                    Thread.Sleep(200); // pauses the code for 200 miliseconds
                    if (LongestLine > 90) // sees if LongestLine is greater than 90
                    {
                        while (Registers.Count() < 15) // continous while there are less than 15 registers
                        {
                            int RegID = Registers.Count(); // sets up an ID
                            Registers.Add(openRegister(RegID)); // opens a new register with the ID above

                            while (Registers[RegID].line.Count <= Registers[FindShortestLine(Registers)].line.Count) // continous while the line of the new register is less than the shortest register
                            {
                                Customer person = Registers[FindLongestLine(Registers)].CheckOut(); // gets a person out of the ongest line
                                Registers[RegID].JoinLine(person); // adds a person to the new register
                            }
                            
                        }
                    } 
                    else if (LongestLine > 50) // sees if LongestLine is greater than 50
                    {
                        int newRegCount = Registers.Count() + 2; // sets up a number greater than the number of registers
                        while (Registers.Count() < newRegCount && newRegCount > 15) // continous while there are less than the number of registers + 2 and there are less than 15 registers
                        {
                            int RegID = Registers.Count(); // sets up an ID
                            Registers.Add(openRegister(RegID)); // opens a new register with the ID above

                            while (Registers[RegID].line.Count <= Registers[FindShortestLine(Registers)].line.Count) // continous while the line of the new register is less than the shortest register
                            {
                                Customer person = Registers[FindLongestLine(Registers)].CheckOut(); //gets a person out of the ongest line
                                Registers[RegID].JoinLine(person); // adds a person to the new register
                            }
                            
                        }
                    }
                }

            }
        }
        int rand(int minX, int maxX) // sets up rand method for random numbers between minX and maxX
        {
            Random random = new Random(); // sets up a new random element
            int num = 0; // sets up int num
            num = random.Next(minX, maxX); // set num to a random number between minX and maxX
            return num; // returns num
        }

        double randDouble(double minX, double maxX) // sets up randDouble method for random numbers between minX and maxX that is a doulbe
        {
            Random random = new Random(); // sets up a new random element
            double num = 0; // sets up double num
            num = random.NextDouble() * (maxX - minX) + minX;  // set num to a random number between minX and maxX
            return num; // returns num
        }

        Register openRegister(int ID) // sets up openRegister method
        {
            Register reg = new Register(); // sets up a new register
            reg.ID = ID; // sets up an ID for the register
            return reg; // returns teh register
        }

        int FindLongestLine(List<Register> Registers) // set up method to find the longest line
        {
            int LLine = 1; // sets u[ int LLine as 1
            for (int i = 0; i < Registers.Count() -1; i++) // continous while i is less than the number of registers
            {
                if (Registers[LLine - 1].line.Count < Registers[i].line.Count) // sees if the line at i is greater than the line at LLine - 1
                {
                    LLine = Registers[i].ID; // sets LLine the the ith registers ID
                }

            }

            return LLine; // returns LLine
        }

        int FindShortestLine(List<Register> Registers) // set up method to find the shortst line
        {
            int SLine = 1; // sets u[ int SLine as 1
            for (int i = 0; i < Registers.Count() - 1; i++) // continous while i is less than the number of registers
            {
                if (Registers[SLine - 1].line.Count > Registers[i].line.Count) // sees if the line at i is less than the line at SLine - 1
                {
                    SLine = Registers[i].ID; // sets SLine the the ith registers ID
                }

            }

            return SLine; // sets SLine the the ith registers ID
        }
    }
}

