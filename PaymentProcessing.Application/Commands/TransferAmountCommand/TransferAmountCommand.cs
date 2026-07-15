using MediatR;
using PaymentProcessing.Application.Abstract.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Commands.TransferAmountCommand
{
    public record TransferAmountCommand(long FromAccountId, long ToAccountId, decimal Amount) : IRequest<EmptyResponse>;
}
