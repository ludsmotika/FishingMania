namespace FishingMania.Web.ViewModels.Product
{
    using System.Collections.Generic;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class ProductInShoppingCartViewModel
    {
        public int Id { get; set; }

        public string ShoppingCartProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
    }
}
