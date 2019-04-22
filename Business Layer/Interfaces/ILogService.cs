using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BusinessLayer.Interfaces
{
    public interface ILogService
    {
        void Create(string message, string userName = "Anonymous");

        void ErrorMessage(string message, string userName = "Anonymous");

    }
}
