using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Common;
using Common.Interfaces;
using DataLayer;
using DataLayer.Interfaces;
using LiteDB;
using Serilog;


namespace BusinessLayer
{
    public class FactorySupport
    {
        public IFrameWork Create(bool isMySQL, LiteRepository repo, EventMediator eventMediator)
        {
            ILogService logService = new LogService();
            IErrorService errorService = new ErrorService(logService, eventMediator);
            IDataService dataService = new DataServiceProvider(isMySQL, repo, errorService);
            IMapper mapper = new Mapper();
            IUserService userService = new UserService(dataService, mapper, logService);
            FrameWork frameWork = new FrameWork(dataService, logService, errorService, userService);
            return frameWork;
        }
    }
}
