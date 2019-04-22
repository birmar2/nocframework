using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    interface IValidationAttribute<T>
    {
        ValidationResult Validate(T input);
    }
}
