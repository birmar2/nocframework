using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IConnectorWall
    {
        int Id { get; set; }

        string Name { get; set;}

        int LocationID { get; set; }

        int Number { get; set;}

        string Tag { get; set;}

        bool IsWorking { get; set;}

        /// <summary>
        /// if there is no connection,
        /// than it is empty
        /// </summary>
        bool IsEmpty { get; set;}

        /// <summary>
        /// ConnectorTypeName has to be unique, 
        /// because it is the index at LocationType class
        /// </summary>
        string ConnectorTypeName { get; set;}

        ConnectorType Type { get; set;}

        int ParentLocationID { get; set;}

        int SymbolID { get; set;}
        
    }
}
