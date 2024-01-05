using FluentValidation.Results;

namespace InventoryManagementSystem.Application.Exceptions
{
     public class ValidationException : Exception
    {
        private List<string> ValidationErrors { get; } = new();

        public ValidationException(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentException("Validation error can not be empty!");

            ValidationErrors.Add(error);
        }
        public ValidationException(ValidationResult validationResult)
        {
            if (validationResult is null)
                throw new ArgumentException("Validation result is null!");

            foreach (var validationError in validationResult.Errors)
            {
                ValidationErrors.Add(validationError.ErrorMessage);
            }
        }

        public List<string> ReadErrors()
        {
            return ValidationErrors;
        }
    }
}
