using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRegisterPortActive
    {
        IPortActive this[int ItemId, int portNumber] { get; }

        int Count { get; }

        bool Add(IPortActive port);

        bool Remove(IPortActive port);

        bool Remove(int itemId, int portNumber);
    }
}
