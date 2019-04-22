using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Connection : IConnection
    {
        private int id;
        private string name;
        private int sourceItemId;
        private int sourcePortNumber;
        private int destinationItemId;
        private int destinationPortNumber;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int SourceItemId { get => sourceItemId; set => sourceItemId = value; }
        public  int SourcePortNumber { get => sourcePortNumber; set => sourcePortNumber = value; }
        public  int DestinationItemId { get => destinationItemId; set => destinationItemId = value; }
        public  int DestinationPortNumber { get => destinationPortNumber; set => destinationPortNumber = value; }
    }
}
