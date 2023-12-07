using FluentValidation;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
    public class OrderUpdateDtoValidator : BaseValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(dto => dto.ActualDeliveryDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .When(dto => dto.ActualDeliveryDate != null);
            RuleFor(dto => dto.Status)
             .Must(status => ((int)status >= 0 && (int)status <= 3))
             .WithMessage("Status must be 0, 1, 2, or 3");
        }
    }
}
