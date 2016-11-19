using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoService.DAL.Models;
using Tbs16.DAL;

namespace AutoService.DAL
{
    public class AutoServiceUnitOfWork : IAutoServiceUnitOfWork
    {
        private DBContext db = DBContext.GetDBContext();
        private IRepository<User> userRepositrory;
        private IRepository<Role> roleRepositrory;
        private IRepository<User_Role> user_RoleRepositrory;
        private IRepository<Module> moduleRepositrory;
        private IRepository<Application> applicationRepository;
        private IRepository<CoordinationRequest> coordinationRequestRepository;
        private IRepository<CoordinationResponse> coordinationResponseRepository;

        public IRepository<User> Users
        {
            get
            {
                if (userRepositrory == null)
                    userRepositrory = new Repository<User>(db);
                return userRepositrory;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepositrory == null)
                    roleRepositrory = new Repository<Role>(db);
                return roleRepositrory;
            }
        }

        public IRepository<User_Role> User_Roles
        {
            get
            {
                if (user_RoleRepositrory == null)
                    user_RoleRepositrory = new Repository<User_Role>(db);
                return user_RoleRepositrory;
            }
        }

        public IRepository<Module> Modules
        {
            get
            {
                if (moduleRepositrory == null)
                    moduleRepositrory = new Repository<Module>(db);
                return moduleRepositrory;
            }
        }

        public IRepository<Application> Applications
        {
            get
            {
                if (applicationRepository == null)
                    applicationRepository = new Repository<Application>(db);
                return applicationRepository;
            }
        }

        public IRepository<CoordinationRequest> CoordinationRequests
        {
            get
            {
                if (coordinationRequestRepository == null)
                    coordinationRequestRepository = new Repository<CoordinationRequest>(db);
                return coordinationRequestRepository;
            }
        }

        public IRepository<CoordinationResponse> CoordinationResponses
        {
            get
            {
                if (coordinationResponseRepository == null)
                    coordinationResponseRepository = new Repository<CoordinationResponse>(db);
                return coordinationResponseRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}