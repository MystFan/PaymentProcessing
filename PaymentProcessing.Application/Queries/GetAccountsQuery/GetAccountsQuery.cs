using MediatR;
using PaymentProcessing.Application.Queries.GetCustomersQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Queries.GetAccountsQuery
{
    public record GetAccountsQuery(long? CustomerId = null) : IRequest<GetAccountsQueryResponse[]>;
}
