using MediatR;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Commands
{
    public class DeleteOrderCommand : IRequest<ResponseDto<object>>
    {
        public int Id { get; set; }
    }
}
