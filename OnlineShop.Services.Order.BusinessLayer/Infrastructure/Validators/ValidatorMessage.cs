namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
    public static class ValidatorMessage
    {
        public const string EmptyUserId = "User id must be specified.";
        public const string EmptyProductIds = "Order must contain products.";
        public const string TotalLessThanZero = "Total price of the order must not be less than 0.";
        public const string EmptyPhoneNumber = "Phone number must be specified.";
        public const string EmptyDeliveryAddress = "Address must be specified.";
        public const string ActualDeliveryDateGreaterThanNow = "Actual delivery date must not be greater than current date.";
    }
}
