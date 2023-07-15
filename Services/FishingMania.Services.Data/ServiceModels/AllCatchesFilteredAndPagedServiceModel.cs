namespace FishingMania.Services.Data.ServiceModels
{
    using System.Collections.Generic;

    using FishingMania.Web.ViewModels.Catch;

    public class AllCatchesFilteredAndPagedServiceModel
    {
        public AllCatchesFilteredAndPagedServiceModel()
        {
            this.Catches = new HashSet<CatchViewModel>();
        }

        public int TotalCatches { get; set; }

        public IEnumerable<CatchViewModel> Catches { get; set; }
    }
}
