using System;
using System.Collections.Generic;

namespace Common.Exceptions
{
    public class InternalValidationException : Exception
    {
        public InternalValidationException(List<string> errors)
        {
            Errors = errors;
        }

        public InternalValidationException(string error)
        {
            Errors = new List<string> { error };
        }

        public List<string> Errors { get; }
    }
}
