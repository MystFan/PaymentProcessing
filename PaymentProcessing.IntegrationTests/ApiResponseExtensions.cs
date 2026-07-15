using Microsoft.Playwright;
using PaymentProcessing.Domain.Exceptions;

namespace PaymentProcessing.IntegrationTests
{
    public static class ApiResponseExtensions
    {
        public static async Task<string?> GetValidationErrorMessageAsync(this IAPIResponse response, string member)
        {
            var errorsElement = await response.JsonAsync();

            string? errorMessage = null;
            if (errorsElement?.TryGetProperty("additionalData", out var additionalData) == true
                && additionalData.TryGetProperty("validationErrors", out var validationErrors)
                && validationErrors.TryGetProperty(member, out var name))
            {
                errorMessage = name.GetString();
            }

            return errorMessage;
        }

        public static async Task<ExceptionReasonCode?> GetValidationErrorCodeAsync(this IAPIResponse response)
        {
            var errorsElement = await response.JsonAsync();

            ExceptionReasonCode? errorCode = null;
            if (errorsElement?.TryGetProperty("code", out var code) == true)
            {
                errorCode = (ExceptionReasonCode)Enum.Parse(typeof(ExceptionReasonCode), code.GetString()!);
            }

            return errorCode;
        }
    }
}
