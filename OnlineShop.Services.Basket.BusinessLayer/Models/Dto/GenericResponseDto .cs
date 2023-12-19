namespace OnlineShop.Services.Basket.BusinessLayer.Models.Dto
{
    public class ResponseDto<T> : ResponseDto
    {
        public T? Result { get; set; }
    }
}
