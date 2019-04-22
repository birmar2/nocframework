using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRegisterActive
    {
        IItemActive this[int ID] { get; }

        int Count { get; }

        bool Add(IItemActive item);

        bool Remove(IItemActive item);

        bool Remove(int id);
    }
}
