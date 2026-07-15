using MediatR;
using PaymentProcessing.Application.Abstract.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Commands.CreateAccountCommand
{
    public record CreateAccountCommand(long CustomerId, decimal Balance, string Currency) : IRequest<EmptyResponse>;
}
