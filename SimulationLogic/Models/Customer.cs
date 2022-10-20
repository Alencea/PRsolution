using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLogic
{
    /// <summary>
    /// sets up the cart interface
    /// </summary>
    interface Cart
    {
        void AddToCart(Item item); // methode to add item to cart
        Item RemoveFromeCart(); // methode to remove item from cart
    }
    /// <summary>
    /// sets up customer class
    /// </summary>
    internal class Customer
    {
        public int ID; // customer ID
        public Stack<Item> cart = new Stack<Item>(); // cart that is a stack
        /// <summary>
        /// method to add item to cart
        /// </summary>
        /// <param name="item"></param>
        public void AddToCart(Item item)
        {
            cart.Push(item); // pushes the item onto the stack cart
        }
        /// <summary>
        /// methode to remve item from cart
        /// </summary>
        /// <returns></returns>
        public Item RemoveFromCart()
        {
            return cart.Pop(); // pops the item off the stack cart
        }
    }
}
