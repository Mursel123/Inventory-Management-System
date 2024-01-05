using InventoryManagementSystem.Application.Exceptions;
using System.Net;
using System.Text;

namespace InventoryManagementSystem.Api
{
    internal sealed class Error
    {
        public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.InternalServerError;
        public string Result { get; private set; } = "An unexpected error has occurred. Please contact support.";

        public Error(Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                var stringBuilder = new StringBuilder();
                validationException.ReadErrors().ForEach(x => stringBuilder.AppendLine(x));

                Result = stringBuilder.ToString();
                StatusCode = HttpStatusCode.BadRequest;
            }
            else if (exception is NotFoundException)
            {
                Result = exception.Message;
                StatusCode = HttpStatusCode.NotFound;
            }
        }
    }
}
