using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentProcessing.DataAccess.Repositories;
using PaymentProcessing.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Queries.GetAccountsQuery
{
    public sealed class GetAccountsQueryHandler(IRepository<Account> repository) : IRequestHandler<GetAccountsQuery, GetAccountsQueryResponse[]>
    {
        public async Task<GetAccountsQueryResponse[]> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll()
                .Where(a => request.CustomerId == null || request.CustomerId == a.CustomerId)
                .Select(a => new GetAccountsQueryResponse(a.Id, a.CustomerId, a.Balance, a.Currency))
                .ToArrayAsync(cancellationToken);
        }
    }
}
