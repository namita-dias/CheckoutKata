using SupermarketCheckout.Models;
using System.Collections.Generic;

namespace SupermarketCheckout
{
    public interface ICheckoutService
    {
        List<Product> GetProducts();
        Basket AddToBasket(Product product, int quantity);
        decimal CalculateOfferPrice(Product product, int quantity);
        void UpsertItem(Product product, int quantity);
    }
}