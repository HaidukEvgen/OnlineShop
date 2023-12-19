namespace OnlineShop.Services.Catalog.Application.Models.Dto
{
    public class ResponseDto<T> : ResponseDto
    {
        public T? Result { get; set; }
    }
}
