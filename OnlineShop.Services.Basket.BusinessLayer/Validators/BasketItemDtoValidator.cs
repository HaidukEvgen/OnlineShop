using FluentValidation;
using FluentValidation.Results;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Basket.BusinessLayer.Validators
{
    public class BasketItemDtoValidator : AbstractBaseValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.IntGreaterThan(0));

            RuleFor(item => item.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.notEmpty);

            RuleFor(item => item.Price)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.IntGreaterThan(0));

            RuleFor(item => item.ProductId)
                .NotEmpty()
                .WithMessage(ValidationMessages.notEmpty);
        }

        public override ValidationResult Validate(ValidationContext<BasketItemDto> context)
        {
            return base.Validate(context);
        }
    }
}
