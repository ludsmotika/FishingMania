namespace FishingMania.Web.ViewModels.Cart
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;

    public class ShoppingCartViewModel : IMapFrom<ShoppingCart>
    {
        public string Id { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new HashSet<ShoppingCartProduct>();
    }
}
