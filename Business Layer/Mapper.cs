using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Common.Models;

namespace BusinessLayer
{
    public class Mapper : IMapper
    {
        void IMapper.Map(User user, UserPassChange userPassChange)
        {
            userPassChange.Id = user.Id;
            userPassChange.PasswordSalt = user.PasswordSalt ;
            foreach (string item in user.OldPasswords)
            {
                userPassChange.OldPasswords.Enqueue(item);
            }
            userPassChange.Password = user.Password;
        }

        void IMapper.Map(UserPassChange userPassChange, User user)
        {
            user.Id = userPassChange.Id;
            user.PasswordSalt = userPassChange.PasswordSalt;
            foreach (string item in userPassChange.OldPasswords)
            {
                user.OldPasswords.Add(item);
            }
            user.Password = userPassChange.Password;
        }
    }
}
