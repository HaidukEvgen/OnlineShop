namespace OnlineShop.Services.Catalog.Application.Exceptions
{
    public static class ExceptionMessages
    {
        public static string ProductNotFound(string id)
        {
            return $"Product with id = {id} was not found in database";
        }
    }
}
