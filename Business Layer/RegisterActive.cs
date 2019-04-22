using BusinessLayer.Interfaces;
using Common;
using Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using static Common.Helpers;

namespace BusinessLayer
{
    public class RegisterActive : IRegisterActive
    {
        private ILogService logService;
        private IErrorService errorService;
        private IList<IItemActive> itemList;

        int IRegisterActive.Count => itemList.Count;

        public RegisterActive(ILogService logService, IErrorService errorService)
        {
            this.logService = logService;
            this.errorService = errorService;
            itemList = new List<IItemActive>();
        }

        IItemActive IRegisterActive.this[int ID]
        {
            get
            {
                if (itemList.Count > 0)
                {
                    return itemList.Where(x => x.Id == ID).FirstOrDefault();
                }
                return null;
            }
        }

        bool IRegisterActive.Add(IItemActive item)
        {
            // TODO hibakezelés, ellenőrizni, megfelelő értékeket tartalmaz
            // a ItemActive

            bool ok = true;

            itemList.Add(item);

            return ok;
        }

        bool IRegisterActive.Remove(IItemActive item)
        {
            bool ok = false;
            // TODO: van e connection
            if (itemList.Remove(item))
            {
                ok = true;

                return ok;
            }
            else
            {
                IError error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Nem létezik ilyen item");

                errorService.Write(error);

                return ok;
            }
        }

        bool IRegisterActive.Remove(int id)
        {
            bool ok = false;
            // TODO: van e connection
            int index = GetIndex(id);
            if (index >= 0)
            {
                itemList.RemoveAt(index);

                ok = true;

                return ok;
            }
            else
            {
                IError error = Helpers.ErrorMessage(ErrorType.NoUniqueID,
                    "Nem létezik ilyen item");

                errorService.Write(error);

                return ok;
            }
        }
        private int GetIndex(int ID)
        {
            return itemList.IndexOf(itemList.Where(x => x.Id == ID).FirstOrDefault());
        }
    }
}
