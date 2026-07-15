using MediatR;
using PaymentProcessing.Application.Abstract.Implementation;
using PaymentProcessing.DataAccess.Repositories;
using PaymentProcessing.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PaymentProcessing.Application.Commands.CreateCustomerCommand
{
    public sealed class CreateCustomerCommandHandler(IRepository<Customer> repository) : IRequestHandler<CreateCustomerCommand, EmptyResponse>
    {
        public async Task<EmptyResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            await repository.CreateAsync(customer);

            return EmptyResponse.Instance;
        }
    }
}
