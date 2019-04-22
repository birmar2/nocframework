using BusinessLayer.Interfaces;
using Common;
using Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using static Common.Helpers;

namespace BusinessLayer
{
    public class RegisterPortActive : IRegisterPortActive
    {
        private ILogService logService;
        private IErrorService errorService;
        private IList<IPortActive> portList;

        int IRegisterPortActive.Count => portList.Count;

        public RegisterPortActive(ILogService logService, IErrorService errorService)
        {
            this.logService = logService;
            this.errorService = errorService;
            portList = new List<IPortActive>();
        }

        IPortActive IRegisterPortActive.this[int ItemId, int portNumber]
        {
            get
            {
                if (portList.Count > 0)
                {
                    return portList.Where(x => x.ItemID == ItemId && x.PortNumber == portNumber).FirstOrDefault();
                }
                return null;
            }
        }

        bool IRegisterPortActive.Add(IPortActive port)
        {
            // TODO hibakezelés, ellenőrizni, megfelelő értékeket tartalmaz
            // a ItemActive

            bool ok = true;

            portList.Add(port);

            return ok;
        }

        bool IRegisterPortActive.Remove(IPortActive port)
        {
            bool ok = false;
            // TODO: van e connection
            if (portList.Remove(port))
            {
                ok = true;

                return ok;
            }
            else
            {
                IError error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Nem létezik ilyen port");

                errorService.Write(error);

                return ok;
            }
        }

        bool IRegisterPortActive.Remove(int itemId, int portNumber)
        {
            bool ok = false;
            int index = GetIndex(itemId, portNumber);
            if (index >= 0)
            {
                portList.RemoveAt(index);

                ok = true;

                return ok;
            }
            else
            {
                IError error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Nem létezik ilyen port");

                errorService.Write(error);

                return ok;
            }
        }
        private int GetIndex(int ItemId, int portNumber)
        {
            return portList.IndexOf(portList.Where(x => x.ItemID == ItemId && x.PortNumber == portNumber).FirstOrDefault());
        }
    }
}
