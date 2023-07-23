namespace FishingMania.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    public class OrderProduct : BaseDeletableModel<string>
    {
        public OrderProduct()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string OrderId { get; set; }

        [Required]
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

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
