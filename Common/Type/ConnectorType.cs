using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using static Common.Helpers;

namespace Common.Models
{
    public class ConnectorType
    {
        private IDictionary<string, string> manufacturer = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                return manufacturer[name];
            }
        }

        public int Length => manufacturer.Count;

        public KeyValuePair<string,string>[] ToArray()
        {
            return manufacturer.ToArray();
        }

        IError AddPortType(string connectorTypeName, string manufacturerName)
        {
            string item;
            if (manufacturer.TryGetValue(connectorTypeName, out item))
            {
                return Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Ilyen típusú port már létezik");
            }
            else
            {
                manufacturer.Add(connectorTypeName, manufacturerName);
                return Helpers.ErrorMessage(ErrorType.NoError);
            }
        }

        IError RemovePortType(string connectorTypeName)
        {
            string item;
            if (manufacturer.TryGetValue(connectorTypeName, out item))
            {
                manufacturer.Remove(connectorTypeName);
                return Helpers.ErrorMessage(ErrorType.NoError);
            }
            else
            {
                return Helpers.ErrorMessage(ErrorType.NoItem,
                    "Nincs ilyen elem");
            }
        }

        List<string> GetKeys()
        {
            return manufacturer.Keys.ToList();
        }
    }
}
