using MediatR;
using PaymentProcessing.DataAccess;

namespace PaymentProcessing.Application
{
    public class CommitBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommitBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
