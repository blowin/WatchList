using FluentValidation.Results;

namespace WatchList.Domain.Extensions;

public static class ValidationResultExt
{
    public static void ThrowIfInvalid(this ValidationResult self)
    {
        if(self.IsValid)
            return;

        var errors = self.Errors.Select(err => new Exception(err.ErrorMessage));
        throw new AggregateException(errors);
    }
}