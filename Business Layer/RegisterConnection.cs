using BusinessLayer.Interfaces;
using Common;
using Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using static Common.Helpers;

namespace BusinessLayer
{
    public class RegisterConnection : IRegisterConnection
    {
        private ILogService logService;
        private IErrorService errorService;
        private IList<IConnection> connectionList;

        int IRegisterConnection.Count => connectionList.Count;

        public RegisterConnection(ILogService logService, IErrorService errorService)
        {
            this.logService = logService;
            this.errorService = errorService;
            connectionList = new List<IConnection>();
        }
        IConnection IRegisterConnection.this[int ID]
        {
            get
            {
                if (connectionList.Count > 0)
                {
                    return connectionList.Where(x => x.Id == ID).FirstOrDefault();
                }
                return null;
            }
        }

        

        bool IRegisterConnection.Add(IConnection connection)
        {
            // TODO hibakezelés, ellenőrizni, megfelelő értékeket tartalmaz
            // a IConnection

            bool ok = true;

            connectionList.Add(connection);

            return ok;
        }

        bool IRegisterConnection.Remove(IConnection connection)
        {
            bool ok = false;
            // TODO: van e connection
            if (connectionList.Remove(connection))
            {
                ok = true;

                return ok;
            }
            else
            {
                IError error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Nem létezik ilyen kapcsolat");

                errorService.Write(error);

                return ok;
            }
        }

        bool IRegisterConnection.Remove(int id)
        {
            bool ok = false;
            // TODO: van e connection
            int index = GetIndex(id);
            if (index >= 0)
            {
                connectionList.RemoveAt(index);

                ok = true;

                return ok;
            }
            else
            {
                IError error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Nem létezik ilyen kapcsolat");

                errorService.Write(error);

                return ok;
            }
        }

        private int GetIndex(int ID)
        {
            return connectionList.IndexOf(connectionList.Where(x => x.Id == ID).FirstOrDefault());
        }
    }
}
