using System.Drawing;

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

        public static class Comment
        {
            public const int ContentMinLength = 15;
            public const int ContentMaxLength = 500;
        }

        public static class ProductCategory
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }

        public static class Manufacturer
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
            public const int CountryMinLength = 4;
            public const int CountryMaxLength = 60;
        }

        public static class Product
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
            public const string MinPrice = "0.01";
            public const string MaxPrice = "10000.00";
            public const int MinAmountInStock = 0;
            public const int MaxAmountInStock = 1000;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;
        }

        public static class Order
        {
            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 100;
        }
    }
}
