using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.Services.Interfaces;
using AutoService.Services.Services;
using System.Security.Principal;
using AutoService.DAL;
using AutoService.DAL.Models;

namespace AutoService.Services
{
    public static class ServicesFactory
    {
        public static IAccountService CreateAccountService(IPrincipal user)
        {
            return new AccountService(user);
        }

        public static IApplicationService CreateApplicationService(IRepository<Application> _repository, IDateTimeProvider _timeProvider, User _currentUser)
        {
            return new ApplicationService(new Repository<Application>(DBContext.GetDBContext()), _timeProvider, _currentUser);
        }

        public static IDateTimeProvider CreateDateTimeProvider()
        {
            return new DateTimeProvider();
        }

        public static IPermissionService CreatePermissionService()
        {
            return new PermissionService();
        }

        public static ISettingService CreateSettingService()
        {
            return new SettingService();
        }
    }
}
