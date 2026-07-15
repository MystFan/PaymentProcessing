using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentProcessing.Application.Abstract.Implementation;
using PaymentProcessing.DataAccess.Repositories;
using PaymentProcessing.Domain;
using PaymentProcessing.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Commands.TransferAmountCommand
{
    public sealed class TransferAmountCommandHandler(IRepository<Account> accountRepository, IRepository<Transaction> transactionRepository) : IRequestHandler<TransferAmountCommand, EmptyResponse>
    {
        public async Task<EmptyResponse> Handle(TransferAmountCommand request, CancellationToken cancellationToken)
        {
            Account? fromAccount = await accountRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Id == request.FromAccountId, cancellationToken);

            Account? toAccount = await accountRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Id == request.ToAccountId, cancellationToken);

            if (fromAccount == null || toAccount == null)
            {
                throw new DomainException(ExceptionReasonCode.AccountNotFound, "Account not found.");
            }

            Transaction transaction = fromAccount.TransferTo(toAccount, request.Amount, DateTime.UtcNow);

            await transactionRepository.CreateAsync(transaction, cancellationToken);

            return EmptyResponse.Instance;
        }
    }
}
