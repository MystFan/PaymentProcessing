using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentProcessing.Application.Abstract.Implementation;
using PaymentProcessing.DataAccess.Repositories;
using PaymentProcessing.Domain;
using PaymentProcessing.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Commands.CreateAccountCommand
{
    public sealed class CreateAccountCommandHandler(IRepository<Account> accountRepository, IRepository<Customer> customerRepository) : IRequestHandler<CreateAccountCommand, EmptyResponse>
    {
        public async Task<EmptyResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await customerRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken);

            if (customer == null) 
            {
                throw new DomainException(ExceptionReasonCode.CustomerNotFound, "Customer not found.");
            }

            var account = new Account()
            {
                CustomerId = customer.Id,
                Customer = customer,
                Balance = request.Balance,
                Currency = request.Currency
            };

            await accountRepository.CreateAsync(account);

            return EmptyResponse.Instance;
        }
    }
}
