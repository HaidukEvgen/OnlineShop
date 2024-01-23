using FluentValidation;
using FluentValidation.Results;

namespace OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage} ");
                var message = "Validation failed: " + string.Join(", ", errorMessages);
                throw new ValidationException(message);
            }

            return validationResult;
        }
    }
}
