using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using Common.Models;

namespace BusinessLayer.Interfaces
{
    public interface IMapper
    {
        void Map(User user, UserPassChange userPassChange);

        void Map(UserPassChange userPassChange, User user);
    }
}
