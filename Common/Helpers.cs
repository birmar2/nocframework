using Common.Interfaces;
using Common.Models;
using System.Collections.Generic;

namespace Common
{
    public static class Helpers
    {
        private static IList<string> symbolNameList = new List<string>
            {
                "router","switch","pc","server","wirelessrouter",
                "linevertical","linehorizontal",
                "accesspoint","bridge","hub","multilayerswitch",
                "opticalcrossconnect","opticalrouter", 
                "turnleft", "turnright"
            };

    public static IError ErrorMessage(ErrorType errorType, string errorMessage = "")
        {
            IError error = new Error(errorType, errorMessage);
            return error;
        }

        public static IList<string> GetSymbolNames()
        {
            return symbolNameList;
        }

        public static int GetSymbolIndex(string name)
        {
            int index = symbolNameList.IndexOf(name);
            return index + 1;
        }

        public enum SymbolName
        {
            Router=1,
            Switch,
            Pc,
            Server,
            Wirelessrouter,
            Linevertical,
            Linehorizontal,
            Accesspoint,
            Bridge,
            Hub,
            Multilayerswitch,
            Opticalcrossconnect,
            Opticalrouter,
            Turnleft,
            Turnright
        }

        public enum ErrorType
        {
            NoError = 0,
            DatabaseError,
            InputError,
            NoUniqueID,
            NoItem
        }

        public enum ConnectorType
        {
            Single,
            Double,
            Rated
        }
    }
}
