using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Lib.Infrastructure
{
    public class ValidationResult
    {
        public bool ValidationSuccesful { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

       
    }
    public class ValidationResult<T> : ValidationResult
    {
        public T ValidatedResult { get; set; }
    }
}
