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
        public bool IsModuleActive(string moduleName)
        {
            using (var uow = new UnitOfWork())
            {
                var module = uow.Modules.GetAll().FirstOrDefault(t => t.Name == moduleName);
                return module != null ? module.IsActive : false;
            }
        }

        public bool IsModuleActive(int Code)
        {
            using (var uow = new UnitOfWork())
            {
                var module = uow.Modules.GetAll().FirstOrDefault(t => t.Code == Code);
                return module != null ? module.IsActive : false;
            }
        }

        public void SetModuleActive(string moduleName)
        {
            throw new NotImplementedException();
        }

        public void SetModuleActive(int id)
        {
            throw new NotImplementedException();
        }
    }
}
