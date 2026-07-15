using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Queries.GetCustomersQuery
{
    public record GetCustomersQuery : IRequest<GetCustomersQueryResponse[]>;
}
