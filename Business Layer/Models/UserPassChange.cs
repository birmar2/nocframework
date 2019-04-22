using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class UserPassChange
    {
        public int Id
        {
            get; set;
        }

        public string PasswordSalt
        {
            get; set;
        }

        public Queue<string> OldPasswords
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string NewPassword
        {
            get; set;
        }

        public string HashedNewPassword
        {
            get; set;
        }

    }
}
