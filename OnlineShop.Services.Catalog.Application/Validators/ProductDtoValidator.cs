using FluentValidation;
using FluentValidation.Results;
using OnlineShop.Services.Catalog.Application.Models.Dto;

namespace OnlineShop.Services.Catalog.Application.Validators
{
    public class ProductDtoValidator : AbstractBaseValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage(ValidationMessages.notEmpty)
                .MaximumLength(50).WithMessage(ValidationMessages.StringLessThan(50));

            RuleFor(dto => dto.Price)
                .NotEmpty().WithMessage(ValidationMessages.notEmpty)
                .GreaterThan(0).WithMessage(ValidationMessages.IntGreaterThan(0));

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage(ValidationMessages.notEmpty)
                .MaximumLength(200).WithMessage(ValidationMessages.StringLessThan(200));

            RuleFor(dto => dto.Category)
                .NotEmpty().WithMessage(ValidationMessages.notEmpty)
                .MaximumLength(20).WithMessage(ValidationMessages.StringLessThan(20));

            RuleForEach(dto => dto.OptionalFields.Keys)
                .NotEmpty().WithMessage(ValidationMessages.notEmpty)
                .MaximumLength(50).WithMessage(ValidationMessages.StringLessThan(50));

            RuleForEach(dto => dto.OptionalFields.Values)
                .NotEmpty().WithMessage(ValidationMessages.notEmpty)
                .MaximumLength(100).WithMessage(ValidationMessages.StringLessThan(100));
        }

        public override ValidationResult Validate(ValidationContext<ProductDto> context)
        {
            return base.Validate(context);
        }
    }
}
