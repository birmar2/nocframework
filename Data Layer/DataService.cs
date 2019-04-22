using Common;
using Common.Interfaces;
using Common.Models;
using DataLayer.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Helpers;

namespace DataLayer
{
    public class DataServiceProvider : IDataService
    {
        private bool isMySQL;
        private LiteRepository repo;
        private IErrorService errorService;

        public DataServiceProvider(bool isMySQL, LiteRepository repo, IErrorService errorService)
        {
            this.isMySQL = isMySQL;
            this.repo = repo;
            this.errorService = errorService;
        }

        IError IDataService.InsertItemActive(IItemActive item)
        {
            IError error = ErrorInit();

            try
            {
                repo.Insert((ItemActive)item);
                return error;
            }
            catch (Exception ex)
            {
                error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    ex.Message);
                errorService.Write(error);
                return error;
            }
        }

        IList<IItemActive> IDataService.GetItemActive()
        {
            IList<IItemActive> result = new List<IItemActive>();

            var result2 = repo.Query<ItemActive>().ToList();
            foreach (var item in result2)
            {
                result.Add(item);
            }

            return result;
        }

        IItemActive IDataService.GetItemActive(int id)
        {
            return repo.Query<ItemActive>().Where(x => x.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Gets a user from database by email, returns null if not found
        /// </summary>
        public User GetUser(string email)
        {
            return repo.Query<User>().ToList().SingleOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Gets a user from the database based on the user's id. Returns null if no user is found.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User</returns>
        public User GetUser(int id)
        {
            return repo.Query<User>().ToList().SingleOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Inserts a user to the database. Returns true if successful
        /// </summary>
        public bool InsertUser(User user)
        {
            IError error = ErrorInit();

            try
            {
                repo.Insert(user);
                return true;
            }
            catch (Exception ex)
            {
                error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    ex.Message);
                errorService.Write(error);
                return false;
            }
        }

        /// <summary>
        /// Updates a user in the database. Returns false if not found in the collection.
        /// </summary>
        public bool UpdateUser(User user)
        {
            return repo.Update(user);
        }

        /// <summary>
        /// Deletes a user from the database based on ID.
        /// </summary>
        public bool DeleteUser(User user)
        {
            return repo.Delete<User>(u => u.Id == user.Id) > 0;
        }

        IError IDataService.DeleteConnection(IConnection connection)
        {
            throw new NotImplementedException();
        }

        IError IDataService.DeleteConnectorWall(IConnectorWall connector)
        {
            throw new NotImplementedException();
        }

        IError IDataService.DeleteItemActive(IItemActive item)
        {
            throw new NotImplementedException();
        }

        IError IDataService.DeleteItemPassive(IItemPassive item)
        {
            throw new NotImplementedException();
        }

        IError IDataService.DeleteLocation(ILocation location)
        {
            throw new NotImplementedException();
        }

        IError IDataService.DeletePortActive(IPortActive port)
        {
            throw new NotImplementedException();
        }

        IError IDataService.DeletePortPassive(IPortPassive port)
        {
            throw new NotImplementedException();
        }

        IError IDataService.DeleteSymbol(ISymbol symbol)
        {
            throw new NotImplementedException();
        }

        IList<IConnection> IDataService.GetConnection()
        {
            IList<IConnection> result = new List<IConnection>();

            var result2 = repo.Query<Connection>().ToList();
            foreach (var item in result2)
            {
                result.Add(item);
            }

            return result;
        }

        IConnection IDataService.GetConnection(int id)
        {
            return repo.Query<Connection>().Where(x => x.Id == id).SingleOrDefault();
        }

        IList<IConnectorWall> IDataService.GetConnectorWall()
        {
            throw new NotImplementedException();
        }

        IConnectorWall IDataService.GetConnectorWall(int id)
        {
            throw new NotImplementedException();
        }

       

        IList<IItemPassive> IDataService.GetItemPassive()
        {
            throw new NotImplementedException();
        }

        IItemPassive IDataService.GetItemPassive(int id)
        {
            throw new NotImplementedException();
        }

        IList<ILocation> IDataService.GetLocation()
        {
            throw new NotImplementedException();
        }

        ILocation IDataService.GetLocation(int id)
        {
            throw new NotImplementedException();
        }

        IList<IPortActive> IDataService.GetPortActive()
        {
            IList<IPortActive> result = new List<IPortActive>();

            var result2 = repo.Query<PortActive>().ToList();
            foreach (var item in result2)
            {
                result.Add(item);
            }

            return result;
        }

        IPortActive IDataService.GetPortActive(int itemId, int portNumber)
        {
            return repo.Query<PortActive>().Where(x => x.ItemID == itemId && x.PortNumber == portNumber).SingleOrDefault();
        }

        IList<IPortPassive> IDataService.GetPortPassive()
        {
            throw new NotImplementedException();
        }

        IPortPassive IDataService.GetPortPassive(int itemId, int portNumber)
        {
            throw new NotImplementedException();
        }

        IList<ISymbol> IDataService.GetSymbol()
        {
            IList<ISymbol> result = new List<ISymbol>();

            List<Symbol> result2 = repo.Query<Symbol>().ToList();
           
            foreach (var item in result2)
            {
                result.Add(item);
            }

            return result;
        }

        ISymbol IDataService.GetSymbol(int id)
        {
            return repo.Query<Symbol>().Where(s => s.Id == id ).SingleOrDefault();
        }

        IError IDataService.InsertConnection(IConnection connection)
        {
            IError error = ErrorInit();

            try
            {
                repo.Insert((Connection)connection);
                return error;
            }
            catch (Exception ex)
            {
                error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    ex.Message);
                errorService.Write(error);
                return error;
            }
        }

        IError IDataService.InsertConnectorWall(IConnectorWall connector)
        {
            throw new NotImplementedException();
        }

        

        private static IError ErrorInit()
        {
            return Helpers.ErrorMessage(ErrorType.NoError);
        }

        IError IDataService.InsertItemPassive(IItemPassive item)
        {
            throw new NotImplementedException();
        }

        IError IDataService.InsertLocation(ILocation location)
        {
            throw new NotImplementedException();
        }

        IError IDataService.InsertPortActive(IPortActive port)
        {
            IError error = ErrorInit();

            try
            {
                repo.Insert((PortActive)port);
                return error;
            }
            catch (Exception ex)
            {
                error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    ex.Message);
                errorService.Write(error);
                return error;
            }
        }

        IError IDataService.InsertPortPassive(IPortPassive port)
        {
            throw new NotImplementedException();
        }

        IError IDataService.InsertSymbol(ISymbol symbol)
        {
            IError error = ErrorInit();

            try
            {
                repo.Insert((Symbol)symbol);
                return error;
            }
            catch (Exception ex)
            {
                error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    ex.Message);
                errorService.Write(error);
                return error;
            }
        }

        IError IDataService.UpdateConnection(IConnection connection)
        {
            throw new NotImplementedException();
        }

        IError IDataService.UpdateConnectorWall(IConnectorWall connector)
        {
            throw new NotImplementedException();
        }

        IError IDataService.UpdateItemActive(IItemActive item)
        {
            IError error = ErrorInit();
            try
            {
                repo.Update((ItemActive)item);
                return error;
            }
            catch (Exception ex)
            {
                error = Helpers.ErrorMessage(ErrorType.DatabaseError,
                    ex.Message);
                errorService.Write(error);
                return error;
            }
        }

        IError IDataService.UpdateItemPassive(IItemPassive item)
        {
            throw new NotImplementedException();
        }

        IError IDataService.UpdateLocation(ILocation location)
        {
            throw new NotImplementedException();
        }

        IError IDataService.UpdatePortActive(IPortActive port)
        {
            throw new NotImplementedException();
        }

        IError IDataService.UpdatePortPassive(IPortPassive port)
        {
            throw new NotImplementedException();
        }

        IError IDataService.UpdateSymbol(ISymbol symbol)
        {
            throw new NotImplementedException();
        }
    }
}
