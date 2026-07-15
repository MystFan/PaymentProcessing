using PaymentProcessing.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.Application
{
    public class RequestValidationException : DomainException
    {
        public RequestValidationException(IDictionary<string, string> errors) : base(ExceptionReasonCode.InvalidRequest, "Request data is invalid.")
        {
            this.AdditionalData.ValidationErrors = errors;
        }
    }
}
