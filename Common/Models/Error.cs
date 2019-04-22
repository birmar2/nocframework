using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using static Common.Helpers;

namespace Common.Models
{
    public class Error : IError
    {
        string message;
        ErrorType errorType;
        bool isError;

        string IError.Message
        {
            get => message;
            set => message = value;
        }
        ErrorType IError.Type
        {
            get => errorType;
            set => errorType = value;
        }
        bool IError.IsError
        {
            get => isError;
            set => isError = value;
        }

        public Error(ErrorType errorType, string message = "")
        {
            this.message = message;
            this.errorType = errorType;
            if (errorType == ErrorType.NoError)
            {
                isError = false;
            }
            else
            {
                isError = true;
            }
        }
    }
}
