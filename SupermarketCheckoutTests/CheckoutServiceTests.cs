using NUnit.Framework;
using SupermarketCheckout;
using SupermarketCheckout.Models;
using System.Collections.Generic;

namespace CheckoutTests
{
    public class Tests
    {
        ICheckoutService _checkoutService;
        List<Product> _products;

        [SetUp]
        public void SetUp()
        {
            _checkoutService = new CheckoutService();
            _products = _checkoutService.GetProducts();
        }

        [Test]
        public void GetProductsTest()
        {
            Assert.IsTrue(_products.Count > 0);
        }

        [TestCase("A", 1, 50)]
        [TestCase("A", 3, 130)]
        [TestCase("A", 5, 230)]
        public void CalculateOfferPriceTest(string productId, int quantity, decimal expectedProductTotalPrice)
        {
            Product product = _products.Find(x => x.ProductId == productId);
            decimal actualProductTotalPrice = _checkoutService.CalculateOfferPrice(product, quantity);
            Assert.AreEqual(expectedProductTotalPrice, actualProductTotalPrice);
        }

        [TestCase((object)new[] { "C", "D" }, 1, 2, 35)]
        [TestCase((object)new[] { "A" }, 3, 1, 130)]
        [TestCase((object)new[] { "B", "C", "B" }, 1, 2, 65)]
        [TestCase((object)new[] { "B", "C", "B", "B" }, 1, 2, 95)]
        public void AddToBasketTest(string[] productIds, int quantity, int expectedBasketItemCount,decimal expectedBasketTotalPrice)
        {
            Basket actual = null;
            foreach (string productId in productIds)
            {
                Product product = _products.Find(x => x.ProductId == productId);
                actual = _checkoutService.AddToBasket(product, quantity);
            }
            Assert.AreEqual(expectedBasketTotalPrice, actual.BasketTotalPrice);
            Assert.AreEqual(expectedBasketItemCount, actual.Items.Count);
        }
    }
}