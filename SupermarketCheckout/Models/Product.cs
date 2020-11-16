using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketCheckout.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public int Price { get; set; }
        public Offer Offer { get; set; }
    }
}
