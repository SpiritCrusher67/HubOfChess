using FluentValidation;
using MediatR;

namespace HubOfChess.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponce> : 
        IPipelineBehavior<TRequest, TResponce> where TRequest : IRequest<TResponce>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
            _validators = validators;

        public Task<TResponce> Handle(TRequest request, 
            CancellationToken cancellationToken, RequestHandlerDelegate<TResponce> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failtures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failture => failture != null)
                .ToList();
            if (failtures.Count != 0)
                throw new ValidationException(failtures);
            return next();
        }
    }
}
