using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL.Models;

namespace AutoService.Services.Interfaces
{
    public interface IAccountService
    {
        User GetCurrentUser();
    }
}
