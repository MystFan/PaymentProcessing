using FluentValidation;
using PaymentProcessing.Domain;

namespace PaymentProcessing.Application.Commands.CreateCustomerCommand
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>, IFluentValidator
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.Name).MaximumLength(Constants.Customer.NameMaxLength).NotEmpty();
            RuleFor(c => c.Email).EmailAddress().MaximumLength(Constants.Customer.EmailMaxLength).NotEmpty();
        }
    }
}
