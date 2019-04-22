using BusinessLayer.Interfaces;
using Serilog;
using System;

namespace BusinessLayer
{
    public class LogService : ILogService
    {
        public LogService()
        {
            Log.Information("Application startup.");
        }
        void ILogService.Create(string message, string userName="Anonymous")
        {
                DateTime time = DateTime.Now;
                Log.Information("{Message} user: {UserName}", message, userName);
        }

        void ILogService.ErrorMessage(string message, string userName = "Anonymous")
        {
            DateTime time = DateTime.Now;
            Log.Warning("{Message} user: {UserName}", message, userName);
        }
    }
}
