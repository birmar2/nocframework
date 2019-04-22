using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IPort
    {
        int Id { get; set; }
        int ItemID { get; set; }

        int PortNumber { get; set; }

        string PortID { get; set; }

        string PortName { get; set; }

        string PortPhysicalType { get; set; }

        int SymbolID { get; set; }

        string PhysicalLocation { get; set;}
    }
}
