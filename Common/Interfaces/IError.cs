using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Helpers;

namespace Common.Interfaces
{
    public interface IError
    {
        string Message {get; set;}

        ErrorType Type {get; set;}

        bool IsError {get; set;}
    }
}
