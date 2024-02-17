using FluentValidation.Results;

namespace MainMarket.Services.CouponAPI.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<KeyValuePair<string, List<string>>> Errors { get; }

        public CustomValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<KeyValuePair<string, List<string>>>();
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
             Errors = failures
            .GroupBy(e => e.PropertyName)
            .Select(g => new KeyValuePair<string, List<string>>(g.Key, g.Select(e => e.ErrorMessage).ToList()))
            .ToList();
        }
    }
}