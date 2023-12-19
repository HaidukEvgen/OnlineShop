namespace OnlineShop.Services.Auth.BusinessLayer.Models.Dto
{
    public class ResponseDto<T> : ResponseDto
    {
        public T? Result { get; set; }
    }
}
