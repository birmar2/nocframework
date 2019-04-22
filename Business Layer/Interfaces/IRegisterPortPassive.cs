using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRegisterPortPassive
    {
        IPortPassive this[int ItemId, int portNumber] { get; }

        int Count { get; }

        bool Add(IPortPassive port);

        bool Remove(IPortPassive port);

        bool Remove(int itemId, int portNumber);
    }
}
