using BusinessLayer.Interfaces;
using Common;
using Common.Interfaces;

namespace BusinessLayer
{
    public class ErrorService : IErrorService
    {
        private ILogService logService;

        private EventMediator eventMediator;

        public ErrorService(ILogService logService, EventMediator eventMediator)
        {
            this.logService = logService;
            this.eventMediator = eventMediator;
        }

        void IErrorService.Write(IError error)
        {
            eventMediator.Send(error.Message, error.Type);

            // TODO Username az ErrorMessage metódus hívásnál
            logService.ErrorMessage(error.Message);
        }
    }
}
