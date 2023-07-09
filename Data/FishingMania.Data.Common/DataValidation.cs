namespace FishingMania.Data.Common
{
    public static class DataValidation
    {
        public static class FishingSpot
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;
            public const int DescriptionMinLength = 40;
            public const int DescriptionMaxLength = 500;
        }

        public static class FishSpecies
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public static class Catch
        {
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 150;
            public const string MinFishWeight = "0.05";
            public const string MaxFishWeight = "250.00";
        }
    }
}
