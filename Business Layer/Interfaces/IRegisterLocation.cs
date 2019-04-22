using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRegisterLocation
    {
        ILocation this[int ID] { get; }

        int Count { get; }

        bool Add(ILocation location);

        bool Remove(ILocation location);

        bool Remove(int id);
    }
}
