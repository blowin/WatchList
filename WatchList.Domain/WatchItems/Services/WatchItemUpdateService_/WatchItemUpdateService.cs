using FluentValidation;
using FluentValidation.Results;
using WatchList.Domain.Extensions;
using WatchList.Domain.WatchItems.Entity;
using WatchList.Domain.WatchItems.Repository;

namespace WatchList.Domain.WatchItems.Services.WatchItemUpdateService_
{
    public class WatchItemUpdateService : IScoped
    {
        private readonly IWatchItemRepository _db;
        private readonly IEnumerable<IValidator<WatchItemUpdateRequest>> _validators;

        public WatchItemUpdateService(IWatchItemRepository db, IEnumerable<IValidator<WatchItemUpdateRequest>> validators)
        {
            _db = db;
            _validators = validators;
        }

        public async Task<WatchItemUpdateRequest> CreateRequestAsync(Guid watchItemId, CancellationToken cancellationToken = default)
        {
            var (validationResult, entity) = await GetWatchItemAsync(watchItemId, cancellationToken);
            validationResult.ThrowIfInvalid();
            return new WatchItemUpdateRequest(entity!);
        }

        public async Task<ValidationResult> UpdateAsync(WatchItemUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var errors = _validators.SelectMany(e => e.Validate(request).Errors);
            var validation = new ValidationResult(errors);
            if (!validation.IsValid)
                return validation;

            var (result, entity) = await GetWatchItemAsync(request.Id, cancellationToken);
            if (!result.IsValid || entity == null)
            {
                validation.Errors.AddRange(result.Errors);
                return validation;
            }
            
            request.Apply(entity);
            await _db.UpdateAsync(entity, cancellationToken);
            return validation;
        }

        private async Task<(ValidationResult Result, WatchItem? Entity)> GetWatchItemAsync(Guid watchItemId, CancellationToken cancellationToken = default)
        {
            var validationResult = new ValidationResult();
            var entity = await _db.GetByIdAsync(watchItemId);
            if (entity == null) 
                validationResult.Errors.Add(new ValidationFailure("Entity", $"Not found entity with id '{watchItemId}'"));
            return (validationResult, entity);

        }
    }
}
