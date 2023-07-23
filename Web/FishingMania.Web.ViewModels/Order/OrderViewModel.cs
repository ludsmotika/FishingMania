namespace FishingMania.Web.ViewModels.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using FishingMania.Data.Models;
    using FishingMania.Services.Mapping;
    using FishingMania.Web.ViewModels.Product;

    public class OrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string Address { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}
