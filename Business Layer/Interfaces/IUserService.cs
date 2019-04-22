using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Models;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        bool CreateUser(User toBeCreated);

        bool DeleteUser(User toDelete);

        bool LoginUser(string username, string password);

        DisplayableUserData GetUserData(int id);

        DisplayableUserData GetUserData(string email);

        ValidationResult ChangePassword(string email, string newPassword);

        bool UpdateUser(User toBeUpdated);

        bool UpdateUserSetting(DisplayableUserData toBeUpdatedSettings);
    }
}
