﻿namespace FishingMania.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Common.Models;

    using static FishingMania.Data.Common.DataValidation.Product;

    public class Product : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Range(MinAmountInStock, MaxAmountInStock)]
        public int AmountInStock { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [ForeignKey(nameof(ManufacturerId))]
        public Manufacturer Manufacturer { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public ProductCategory ProductCategory { get; set; }

        [Required]
        public IEnumerable<OrderProduct> OrdersProduct { get; set; } = new HashSet<OrderProduct>();

        [Required]
        public IEnumerable<Image> Images { get; set; } = new HashSet<Image>();

        [Required]
        public IEnumerable<ShoppingCartProduct> ShoppingCartProduct { get; set; } = new HashSet<ShoppingCartProduct>();
    }
}
