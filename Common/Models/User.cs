using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class User
    {
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public List<string> OldPasswords
        {
            get; set;
        }

        public string PasswordSalt
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public int RequestTimeout
        {
            get; set;
        }

        public string TimeZoneId
        {
            get; set;
        }

        public User()
        {
            this.OldPasswords = new List<string>();
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}, RequestTimeout: {RequestTimeout}, TimeZone: {TimeZoneId}";
        }
        public string ToShortString()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}";
        }
    }
}
