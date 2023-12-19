using FluentValidation;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(dto => dto.UserId).NotEmpty();

            RuleForEach(dto => dto.ProductIds).NotEmpty();

            RuleFor(dto => dto.Total).GreaterThanOrEqualTo(0);

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d+$")
                .WithMessage(ValidatorMessage.PhoneOnlyDigits)
                .MaximumLength(15)
                .WithMessage(ValidatorMessage.PhoneLength);

            RuleFor(dto => dto.DeliveryAddress).NotEmpty();
        }
    }
}
