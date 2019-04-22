using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    class Groupmenu
    {
        public int GroupMenuId
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Groups groups
        {
            get; set;
        }

        public List Menus
        {
            get; set;
        }
    }
}
