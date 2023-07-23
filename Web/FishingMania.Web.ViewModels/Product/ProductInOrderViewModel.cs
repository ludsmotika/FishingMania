namespace FishingMania.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;

    public class ProductInOrderViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
