using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Helpers;

namespace Common
{
    public class ErrorMessageEventArgs : System.EventArgs
    {
        public readonly string message;
        public readonly ErrorType errorType;

        public ErrorMessageEventArgs(string message, ErrorType errorType)
        {
            this.message = message;
            this.errorType = errorType;
        }
    }
}
