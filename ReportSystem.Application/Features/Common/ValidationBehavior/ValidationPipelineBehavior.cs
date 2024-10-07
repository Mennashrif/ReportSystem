using FluentValidation;
using FluentValidation.Results;
using MediatR;
namespace ReportSystem.Application.Features.Common.ValidationBehavior
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest , TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationPipelineBehavior( IEnumerable<IValidator<TRequest>> validators )
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle( TRequest request , RequestHandlerDelegate<TResponse> next , CancellationToken cancellationToken )
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                ValidationResult [] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context , cancellationToken)));
                if (validationResults.SelectMany(r => r.Errors).Where(f => f != null).Count() != 0)
                    throw new ValidationException(validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList());
            }
            return await next();
        }
    }
}