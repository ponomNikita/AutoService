using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL.Models;

namespace AutoService.DAL
{
    public interface IAutoServiceUnitOfWork : IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Module> Modules { get; }
        IRepository<Application> Applications { get; }
        IRepository<CoordinationRequest> CoordinationRequests { get; }
        IRepository<CoordinationResponse> CoordinationResponses { get; }
    }
}
