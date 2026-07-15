using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentProcessing.DataAccess.Repositories;
using PaymentProcessing.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application.Queries.GetCustomersQuery
{
    public sealed class GetCustomersQueryHandler(IRepository<Customer> repository) : IRequestHandler<GetCustomersQuery, GetCustomersQueryResponse[]>
    {
        public async Task<GetCustomersQueryResponse[]> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            GetCustomersQueryResponse[] customers = await repository.GetAll()
                .Select(r => new GetCustomersQueryResponse
                {
                    Email = r.Email,
                    Name = r.Name,
                    Id = r.Id
                })
                .ToArrayAsync();

            return customers;
        }
    }
}
