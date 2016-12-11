using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL;

namespace AutoService.Services
{
    public class SettingService : ISettingService
    {
        private IAutoServiceUnitOfWork uow;

        public SettingService()
        {
            uow = new AutoServiceUnitOfWork();
        }
        public bool IsModuleActive(string moduleName)
        {
            var module = uow.Modules.GetAll().FirstOrDefault(t => t.Name == moduleName);
            return module != null && module.IsActive;
        }

        public bool IsModuleActive(int Code)
        {
            var module = uow.Modules.GetAll().FirstOrDefault(t => t.Code == Code);
            return module != null && module.IsActive;
        }

        public void SetModuleActive(string moduleName)
        {
            throw new NotImplementedException();
        }

        public void SetModuleActive(int Code)
        {
            throw new NotImplementedException();
        }
    }
}
