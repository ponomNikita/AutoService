using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Services
{
    public interface ISettingService
    {
        bool IsModuleActive(int Code);
        bool IsModuleActive(string moduleName);
        void SetModuleActive(int Code);
        void SetModuleActive(string moduleName);
    }
}
