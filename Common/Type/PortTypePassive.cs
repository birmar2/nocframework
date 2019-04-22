using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using static Common.Helpers;

namespace Common.Models
{
    class PortTypePassive
    {
        private HashSet<string> portName = new HashSet<string>();

        public int Length => portName.Count;

        public HashSet<string> Name
        {
            get
            {
                return portName;
            }
        }

        public string[] ToArray()
        {
            return portName.ToArray();
        }

        IError AddPortType(string portTypeName)
        {

            if (portName.Contains(portTypeName))
            {
                return Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Ilyen típusú port már létezik");
            }
            else
            {
                portName.Add(portTypeName);
                return Helpers.ErrorMessage(ErrorType.NoError);
            }
        }

        IError RemovePortType(string portTypeName)
        {

            if (portName.Contains(portTypeName))
            {
                portName.RemoveWhere(item => item == portTypeName);
                return Helpers.ErrorMessage(ErrorType.NoError);
            }
            else
            {
                return Helpers.ErrorMessage(ErrorType.NoItem,
                    "Nincs ilyen elem");
            }
        }
    }
}
