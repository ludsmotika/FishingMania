namespace FishingMania.Web.ViewModels.Catch
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FishingMania.Data.Models;
    using FishingMania.Web.ViewModels.Catch.Enums;

    using static FishingMania.Common.GlobalConstants;

    public class AllCatchesQueryViewModel
    {
        public AllCatchesQueryViewModel()
        {
            this.CurrentPage = DefaultPage;
            this.CatchesPerPage = EntitiesPerPage;

            this.Types = new HashSet<FishType>();
            this.Catches = new HashSet<CatchViewModel>();
        }

        public FishType? Type { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Catches By")]
        public CatchesSorting CatchesSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Catches Per Page")]
        public int CatchesPerPage { get; set; }

        public int TotalCatches { get; set; }

        public IEnumerable<FishType> Types { get; set; }

        public IEnumerable<CatchViewModel> Catches { get; set; }
    }
}
