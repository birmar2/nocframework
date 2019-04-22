using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Interfaces
{
    public interface IConnection
    {
        int Id { get; set; }

        string Name { get; set;}

        int SourceItemId { get; set; }

        int SourcePortNumber { get; set; }

        int DestinationItemId { get; set; }

        int DestinationPortNumber { get; set; }

    }
}
