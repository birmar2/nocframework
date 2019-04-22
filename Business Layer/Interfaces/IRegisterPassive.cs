using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRegisterPassive
    {
        IItemPassive this[int ID] { get; }

        int Count { get; }

        bool Add(IItemPassive item);

        bool Remove(IItemPassive item);

        bool Remove(int id);
    }
}
