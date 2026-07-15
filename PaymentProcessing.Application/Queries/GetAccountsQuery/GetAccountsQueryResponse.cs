using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Queries.GetAccountsQuery
{
    public record GetAccountsQueryResponse(long Id, long CustomerId, decimal Balance, string Currency);
}
