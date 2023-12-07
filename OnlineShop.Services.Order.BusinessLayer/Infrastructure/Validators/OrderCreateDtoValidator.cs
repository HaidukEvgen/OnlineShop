using FluentValidation;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
    public class OrderCreateDtoValidator : BaseValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(dto => dto.UserId).NotEmpty();
            RuleForEach(dto => dto.ProductIds).NotEmpty();
            RuleFor(dto => dto.Total).GreaterThanOrEqualTo(0);
            RuleFor(dto => dto.PhoneNumber).NotEmpty();
            RuleFor(dto => dto.DeliveryAddress).NotEmpty();
        }
    }
}
