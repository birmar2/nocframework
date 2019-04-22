using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ValidationResult
    {
        public bool IsValid
        {
            get
            {
                return !Errors.Any();
            }
        }

        public List<string> Errors
        {
            get; set;
        }

        public ValidationResult()
        {
            Errors = new List<string>();
        }
    }
}
