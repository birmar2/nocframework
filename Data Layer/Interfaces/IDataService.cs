using Common.Interfaces;
using Common.Models;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    public interface IDataService
    {
        User GetUser(string email);

        User GetUser(int id);

        bool InsertUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        IError InsertItemActive(IItemActive item);

        IError InsertItemPassive(IItemPassive item);

        IError InsertPortActive(IPortActive port);

        IError InsertPortPassive(IPortPassive port);

        IError InsertConnection(IConnection connection);

        IError InsertLocation(ILocation location);

        IError InsertConnectorWall(IConnectorWall connector);

        IError InsertSymbol(ISymbol symbol);

        IError UpdateItemActive(IItemActive item);

        IError UpdateItemPassive(IItemPassive item);

        IError UpdatePortActive(IPortActive port);

        IError UpdatePortPassive(IPortPassive port);

        IError UpdateConnection(IConnection connection);

        IError UpdateLocation(ILocation location);

        IError UpdateConnectorWall(IConnectorWall connector);

        IError UpdateSymbol(ISymbol symbol);

        IError DeleteItemActive(IItemActive item);

        IError DeleteItemPassive(IItemPassive item);

        IError DeletePortActive(IPortActive port);

        IError DeletePortPassive(IPortPassive port);

        IError DeleteConnection(IConnection connection);

        IError DeleteLocation(ILocation location);

        IError DeleteConnectorWall(IConnectorWall connector);

        IError DeleteSymbol(ISymbol symbol);

        IList<IItemActive> GetItemActive();

        IItemActive GetItemActive(int id);

        IList<IItemPassive> GetItemPassive();

        IItemPassive GetItemPassive(int id);


        IList<IPortActive> GetPortActive();

        IList<IPortPassive> GetPortPassive();

        IList<IConnection> GetConnection();

        IList<ILocation> GetLocation();

        IList<IConnectorWall> GetConnectorWall();

        IList<ISymbol> GetSymbol();

        IPortActive GetPortActive(int itemId, int portNumber);

        IPortPassive GetPortPassive(int itemId, int portNumber);

        IConnection GetConnection(int id);

        ILocation GetLocation(int id);

        IConnectorWall GetConnectorWall(int id);

        ISymbol GetSymbol(int id);
    }
}
