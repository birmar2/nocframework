using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IPortActive : IPort
    {
        string PortConfig { get; set; }

        string IPAddress  {get; set; }

        string MacAddress {get; set; }

        string PortActiveTypeName { get; set; }
    }
}
