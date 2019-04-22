using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using static Common.Helpers;

namespace Common.Models
{
    public class LocationType
    {
        private HashSet<string> locationName = new HashSet<string>();

        public int Length => locationName.Count;

        public HashSet<string> Name
        {
            get
            {
                return locationName;
            }
        }

        public string[] ToArray()
        {
            return locationName.ToArray();
        }

        public IError AddLocationType(string locationTypeName)
        {
            
            if (locationName.Contains(locationTypeName))
            {
                return Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Ilyen típusú ingatlan/ helyiség létezik");
            }
            else
            {
                locationName.Add(locationTypeName);
                return Helpers.ErrorMessage(ErrorType.NoError);
            }
        }

        public IError RemoveLocationType(string locationTypeName)
        {

            if (locationName.Contains(locationTypeName))
            {
                locationName.RemoveWhere(item => item == locationTypeName);
                return Helpers.ErrorMessage(ErrorType.NoError);
            }
            else
            {
                return Helpers.ErrorMessage(ErrorType.NoItem,
                    "Nincs ilyen elem");
            }
        }
    }
}
