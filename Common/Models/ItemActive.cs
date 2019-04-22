using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Helpers;

namespace Common.Models
{
    public class ItemActive : IItemActive
    {
        private int id;
        private int locationID;
        private int symbolID;
        private string deviceID;
        private string deviceName;
        private string notes;
        private IList<int> portNumberList = new List<int>();
        private IList<int> connectionIdList = new List<int>();

        // adatbázisból lekérni az eltárolt item id-ját
        // ha id = 0, akkor el kell tárolni az adatbázisba
        public int Id { get => id; set => id = value; } 

        public int LocationID { get => locationID; set => locationID = value; }

        public int SymbolID { get => symbolID; set => symbolID = value; }

        public string DeviceID
        {
            get => deviceID;
            set => deviceID = value;
        }
        public string DeviceName
        {
            get => deviceName;
            set => deviceName = value;
        }
        public string Notes
        {
            get => notes;
            set => notes = value;
        }

        public IList<int> PortNumberList {
            get => portNumberList;
            set => portNumberList = value;
        }

        public IList<int> ConnectionIdList
        {
            get => connectionIdList;
            set => connectionIdList = value;
        }

        public IError AddConnection(IConnection connection)
        {
            IError error = NoError();
            if (ConnectionIdList.Count > 0 && ConnectionIdList.Contains(connection.Id))
            {
                error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Már létezik ilyen id-val kapcsolat");
            }
            else
            {
                ConnectionIdList.Add(connection.Id);
            }

            return error;
        }

        public IError AddPort(IPortActive port)
        {
            IError error = NoError();
            if (PortNumberList.Count > 0 && PortNumberList.Contains(port.PortNumber))
            {
            error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Már létezik ilyen sorszámú port");
            }
            else
            {
            portNumberList.Add(port.PortNumber);
            }

            return error;
        }

        public IList<int> GetConnectionsIDList()
        {
            return connectionIdList;
        }

        public IList<int> GetPortsNumberList()
        {
            return portNumberList;
        }

        public IError RemoveConnection(IConnection connection)
        {
            throw new NotImplementedException();
        }

        public IError RemoveConnection(int id)
        {
            throw new NotImplementedException();
        }

        public IError RemovePort(IPortActive port)
        {
            throw new NotImplementedException();
        }

        public IPortActive GetPort(int number)
        {
            throw new NotImplementedException();
        }

        public IError RemovePort(int number)
        {
            throw new NotImplementedException();
        }

        private static IError NoError()
        {
            return Helpers.ErrorMessage(ErrorType.NoError);
        }
    }
}
