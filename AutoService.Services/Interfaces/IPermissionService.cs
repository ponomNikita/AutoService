using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Services.Interfaces
{
    public interface IPermissionService
    {
        bool HasRole(int roleCode, string currentUserLogin);
        string[] GetUserRoles(string login);
    }
}
