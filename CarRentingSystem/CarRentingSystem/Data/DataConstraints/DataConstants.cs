namespace CarRentingSystem.Data.DataConstraints
{
    public static class DataConstants
    {
        public class User
        {
            public const int FullNameMaxLength = 30;
        }

        public class Car
        {
            public const int BrandMaxLength = 20;

            public const int ModelMaxLength = 50;

            public const int DescriptionMaxLength = 10000;

            public const int YearMinValue = 1980;

            public const int YearMaxValue = 2021;
        }

        public class Category
        {
            public const int MaxLength = 20;
        }

        public class Dealer
        {
            public const int NameMaxLength = 20;

            public const int NameMinLength = 2;

            public const int PhoneNumberMaxLength = 30;

            public const int PhoneNumberMinLength = 2;
        }
    }
}
