using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DisplayableUserData
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

        public int RequestTimeout
        {
            get; set;
        }

        public string TimeZoneId
        {
            get; set;
        }

        public static DisplayableUserData Default
        {
            get
            {
                return new DisplayableUserData
                {
                    Id = 0,
                    Email = "default@gmail.com",
                    Name = "Default User",
                    RequestTimeout = 0,
                    TimeZoneId = TimeZoneInfo.Local.Id
                };
            }
        }

    }
}
