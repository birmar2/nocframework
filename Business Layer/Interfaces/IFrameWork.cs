using Common.Interfaces;
using System;
using Common.Models;
using Common;

namespace BusinessLayer.Interfaces
{
    public interface IFrameWork
    {
        /// <summary>
        /// Get Service, e. g. UIFactory
        /// e. g.:
        /// GetService(typeof(IUIFactory))
        /// this method gives back the service
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetService(Type type);

        bool LoadDatabase();

        bool AddItemActive(IItemActive item);

        bool AddItemPassive(IItemPassive item);

        IItemActive GetItemActive(int id);

        /// <summary>
        /// Gets the list of ItemActive
        /// </summary>
        /// <returns></returns>
        bool GetItemActive();

        IItemPassive GetItemPassive(int id);

        /// <summary>
        /// Gets the list of ItemPassive
        /// </summary>
        /// <returns></returns>
        IItemPassive GetItemPassive();

        bool ModifyItemActive(IItemActive item);

        bool ModifyItemPassive(IItemActive item);

        bool RemoveItemActive(IItemActive item);

        bool RemoveItemPassive(IItemPassive item);

        bool RemoveItem(int id);

        bool RemoveItemPassive(int id);
        
        IItemActive SearchItem(int id);

        IItemPassive SearchItemPassive(int id);

        IItemActive SearchItemActive(string search);

        bool AddPortActive(int itemID, IPortActive portActive);

        bool AddPortPassive(int itemID, IPortPassive portPassive);

        IPortActive GetPortActive(int itemID, int portNumber);

        IPortPassive GetPortPassive(int itemID, int portNumber);

        int? GetFreePortOfActiveItem(int itemId);

        bool RemovePort(int itemID, int portNumber);

        bool ModifyPortActive(int itemID, IPortActive portActive);

        bool AddConnection(IConnection connection);

        IConnection  GetConnection(int connectionID);

        bool ModifyConnection(IConnection connection);

        bool AddLocation(ILocation location);

        ILocation GetLocation(int itemID);

        bool ModifyLocation(ILocation location);

        bool AddSymbol(ISymbol symbol);

        ISymbol GetSymbol(int id);

        ISymbol GetSymbol(Helpers.SymbolName symbolName);

        ISymbol GetSymbol(string name);

        bool ModifySymbol(ISymbol symbol);

        bool RemoveSymbol(ISymbol symbol);

        int ItemActiveCount { get; }

        int ItemPassiveCount { get; }

        int ConnectionCount { get; }

        int ConnectorWallCount { get; }

        int LocationCount { get; }

        int PortActiveCount { get; }

        int PortPassiveCount { get; }

        int SymbolCount { get; }
    }
}