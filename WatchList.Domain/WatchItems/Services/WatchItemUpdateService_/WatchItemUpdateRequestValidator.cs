using FluentValidation;
using WatchList.Domain.WatchItems.Entity;

namespace WatchList.Domain.WatchItems.Services.WatchItemUpdateService_;

public sealed class WatchItemUpdateRequestValidator : AbstractValidator<WatchItemUpdateRequest>, ISingleton
{
    public WatchItemUpdateRequestValidator()
    {
        RuleFor(e => e.Id)
            .Must(e => e != Guid.Empty)
            .WithMessage("Should have id");
        
        RuleFor(e => e.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(128);
        
        RuleFor(e => e.Status)
            .Must(status => status != Status.Unknown)
            .WithMessage("Status should not be 'Unknown'");
        
        RuleFor(e => e.Type)
            .Must(status => status != WatchItemType.Unknown)
            .WithMessage("Type should not be 'Unknown'");

        RuleFor(e => e.Genre)
            .NotNull()
            .Must(e => e.Count > 0).WithMessage("Should be at least 1 genre");
    }
}