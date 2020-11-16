using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketCheckout.Models
{
    public class Basket
    {
        public List<Item> Items { get; set; }
        public decimal BasketTotalPrice { get; set; }
    }
}
