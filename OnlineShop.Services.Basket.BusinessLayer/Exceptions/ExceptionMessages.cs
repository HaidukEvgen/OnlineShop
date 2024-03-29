﻿namespace OnlineShop.Services.Basket.BusinessLayer.Exceptions
{
    public static class ExceptionMessages
    {
        public const string InvalidBasket = "Basket consists of invalid items";

        public static string BasketNotFound(string id)
        {
            return $"Basket of user with id = {id} was not found in database";
        }

        public static string OrderCreationFailed(string statusCode)
        {
            return $"Error creating order. Status code: {statusCode}";
        }
    }
}
