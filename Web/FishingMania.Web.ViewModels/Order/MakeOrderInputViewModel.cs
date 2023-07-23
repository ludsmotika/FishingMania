namespace FishingMania.Web.ViewModels.Order
{
    using System.ComponentModel.DataAnnotations;

    using static FishingMania.Data.Common.DataValidation.Order;

    public class MakeOrderInputViewModel
    {
        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        [Required]
        public string ShoppingCartId { get; set; }
    }
}
