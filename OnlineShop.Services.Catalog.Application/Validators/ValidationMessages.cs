namespace OnlineShop.Services.Catalog.Application.Validators
{
    public static class ValidationMessages
    {
        public static string StringLessThan(int symbolsAmount)
        {
            return $"must be not less than {symbolsAmount}";
        }

        public static string IntGreaterThan(int min = 0)
        {
            return $"must be greater than {min}";
        }

        public const string notEmpty = "must not be empty";
    }
}
