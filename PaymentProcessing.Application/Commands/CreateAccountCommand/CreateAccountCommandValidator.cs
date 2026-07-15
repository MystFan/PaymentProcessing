using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PaymentProcessing.Application.Commands.CreateAccountCommand
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>, IFluentValidator
    {
        private static readonly HashSet<string> ValidCurrencyCodes = CultureInfo
            .GetCultures(CultureTypes.SpecificCultures)
            .Select(c => new RegionInfo(c.Name).ISOCurrencySymbol)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);

            RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Must(code => ValidCurrencyCodes.Contains(code))
                .WithMessage("Currency must be a valid currency code.");
        }
    }
}
