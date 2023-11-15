using FluentValidation;
using FluentValidation.Results;

namespace OnlineShop.Services.Catalog.Application.Validators
{
    public abstract class AbstractBaseValidator<T> : AbstractValidator<T> where T : class
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                var arr = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage} ");
                var message = "Validation failed: " + string.Join(", ", arr);
                throw new ValidationException(message);
            }

            return validationResult;
        }
    }
}
