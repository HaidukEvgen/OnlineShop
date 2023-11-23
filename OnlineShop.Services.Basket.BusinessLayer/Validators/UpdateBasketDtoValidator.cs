using FluentValidation;
using FluentValidation.Results;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Basket.BusinessLayer.Validators
{
    public class UpdateBasketDtoValidator : AbstractBaseValidator<UpdateBasketDto>
    {
        public UpdateBasketDtoValidator()
        {
            RuleForEach(dto => dto.Items)
                 .SetValidator(new BasketItemDtoValidator());
        }

        public override ValidationResult Validate(ValidationContext<UpdateBasketDto> context)
        {
            return base.Validate(context);
        }
    }
}
