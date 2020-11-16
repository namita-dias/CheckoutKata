using SupermarketCheckout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout
{
    public class CheckoutService : ICheckoutService
    {
        private Basket _basket;

        public CheckoutService()
        {
            _basket = new Basket();
        }

        public List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    ProductId = "A",
                    Price = 50,
                    Offer = new Offer()
                    {
                        Quantity = 3,
                        Price = 130
                    }
                },
                new Product()
                {
                    ProductId = "B",
                    Price = 30,
                    Offer = new Offer()
                    {
                        Quantity = 2,
                        Price = 45
                    }
                },
                new Product()
                {
                    ProductId = "C",
                    Price = 20
                },
                new Product()
                {
                    ProductId = "D",
                    Price = 15
                }
            };
        }
        public Basket AddToBasket(Product product, int quantity)
        {
            if (_basket.Items == null)
                _basket.Items = new List<Item>();

            UpsertItem(product, quantity);
            _basket.BasketTotalPrice = CalculateBasketTotalPrice(_basket.Items);
            return _basket;
        }

        public decimal CalculateOfferPrice(Product product, int quantity)
        {
            if (quantity < product.Offer.Quantity)
            {
                return quantity * product.Price;
            }
            else
            { 
                int remainder;
                int divident = Math.DivRem(quantity, product.Offer.Quantity, out remainder);
                if (remainder == 0)
                {
                    return divident * product.Offer.Price;
                }
                else
                {
                    return (remainder * product.Price) + (divident * product.Offer.Price);
                }
            }
        }

        public void UpsertItem(Product product, int quantity)
        {
            Item existingItem = _basket.Items.Find(x => x.Product.ProductId == product.ProductId);
            if (existingItem == null)
            {
                Item item = new Item()
                {
                    Product = product,
                    Quantity = quantity
                };
                item.ProductTotalPrice = product.Offer != null ? CalculateOfferPrice(product, quantity) : product.Price;
                _basket.Items.Add(item);
            }
            else
            {
                //update quantity
                existingItem.Quantity = existingItem.Quantity + quantity;
                //re-calculate total price
                existingItem.ProductTotalPrice = product.Offer != null ? CalculateOfferPrice(product, existingItem.Quantity) 
                                                                        : (existingItem.ProductTotalPrice + product.Price);
            }
            
        }

        public decimal CalculateBasketTotalPrice(List<Item> items)
        {
            return items.Sum(x => x.ProductTotalPrice);
        }
    }
}
