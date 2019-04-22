using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ILocation
    {
        int Id { get; set; }

        string Name {get; set;}

        /// <summary>
        /// LocationTypeName has to be unique, 
        /// because it is the index at LocationType class
        /// </summary>
        string LocationTypeName { get; set; }

        int ParentID {get; set;}

        int SymbolID { get; set; }

        string Description {get; set;}

        string Remarks {get; set;}

        /// <summary>
        /// Get the list of IDs of Location childs
        /// </summary>
        /// <returns></returns>
        IList<int> GetChildIDList();

        IError AddChild(ILocation child);

        IError RemoveChild(ILocation child);
    }
}
