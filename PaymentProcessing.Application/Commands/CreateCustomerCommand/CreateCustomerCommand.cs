using MediatR;
using PaymentProcessing.Application.Abstract.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Commands.CreateCustomerCommand
{
    public record class CreateCustomerCommand(string Name, string Email) : IRequest<EmptyResponse>;
}
