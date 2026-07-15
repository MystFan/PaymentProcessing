using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaymentProcessing.Application.Abstract.Implementation;
using PaymentProcessing.Application.Commands.CreateAccountCommand;
using PaymentProcessing.Application.Commands.CreateCustomerCommand;
using PaymentProcessing.Application.Commands.TransferAmountCommand;
using PaymentProcessing.Application.Queries.GetAccountsQuery;
using PaymentProcessing.Application.Queries.GetCustomersQuery;
using PaymentProcessing.Web.Accounts;
using System.ComponentModel.Design;

namespace PaymentProcessing.Web
{
    public static class Endpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/customers", Task<GetCustomersQueryResponse[]> ([FromServices] IMediator mediator) =>
            {
                return mediator.Send(new GetCustomersQuery());
            });

            app.MapPost("/customers", Task<EmptyResponse> ([FromBody] CreateCustomerCommand command, [FromServices] IMediator mediator) =>
            {
                return mediator.Send(command);
            });

            app.MapGet("/accounts", Task<GetAccountsQueryResponse[]> ([AsParameters] GetAccountsModel model, [FromServices] IMediator mediator) =>
            {
                return mediator.Send(new GetAccountsQuery(model.CustomerId));
            });

            app.MapPost("/accounts", Task<EmptyResponse> ([FromBody] CreateAccountCommand command, [FromServices] IMediator mediator) =>
            {
                return mediator.Send(command);
            });

            app.MapPost("/transfers", Task<EmptyResponse> ([FromBody] TransferAmountCommand command, [FromServices] IMediator mediator) =>
            {
                return mediator.Send(command);
            });
        }
    }
}
