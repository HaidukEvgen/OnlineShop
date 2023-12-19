namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
    public static class ValidatorMessage
    {
        public const string PhoneOnlyDigits = "Phone number should consist only of digits.";
        public const string PhoneLength = "Phone number should not exceed 15 digits.";
        public const string ValidStatus = "Status must be 0, 1, 2, or 3";
    }
}
