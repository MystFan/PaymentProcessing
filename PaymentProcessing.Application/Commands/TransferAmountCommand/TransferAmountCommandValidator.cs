using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Commands.TransferAmountCommand
{
    public class TransferAmountCommandValidator : AbstractValidator<TransferAmountCommand>, IFluentValidator
    {
        public TransferAmountCommandValidator()
        {
            RuleFor(x => x.FromAccountId).GreaterThan(0);

            RuleFor(x => x.ToAccountId).GreaterThan(0);

            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
