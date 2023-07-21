namespace FishingMania.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    public class ShoppingCartProduct : BaseDeletableModel<string>
    {
        public ShoppingCartProduct()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ShoppingCartId { get; set; }

        [Required]
        [ForeignKey(nameof(ShoppingCartId))]
        public ShoppingCart ShoppingCart { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }
}
