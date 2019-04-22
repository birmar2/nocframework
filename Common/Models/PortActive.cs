using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;

namespace Common
{
    public class PortActive : IPortActive
    {
        private int id;
        private string ipAddress;
        private string macAddress;
        private string portActiveTypeName;
        private string portConfig;
        private int itemID;
        private int portNumber;
        private string portID;
        private string portName;
        private string portPhysicalType;
        private int symbolID;
        private string physicalLocation;

        public int Id { get => id; set => id = value; }

        public string MacAddress
        {
            get => macAddress;
            set => macAddress = value;
        }
        public string IPAddress
        {
            get => ipAddress;
            set => ipAddress = value;
        }
        public string PortConfig
        {
            get => portConfig;
            set => portConfig = value;
        }
        public int ItemID
        {
            get => itemID;
            set => itemID = value;
        }
        public int PortNumber
        {
            get => portNumber;
            set => portNumber = value;
        }
        public string PortID
        {
            get => portID;
            set => portID = value;
        }
        public string PortName
        {
            get => portName;
            set => portName = value;
        }
        public string PortPhysicalType
        {
            get => portPhysicalType;
            set => portPhysicalType = value;
        }
        public int SymbolID
        {
            get => symbolID;
            set => symbolID = value;
        }
        public string PhysicalLocation
        {
            get => physicalLocation;
            set => physicalLocation = value;
        }
        public string PortActiveTypeName
        {
            get => portActiveTypeName;
            set => portActiveTypeName = value;
        }
    }
}
