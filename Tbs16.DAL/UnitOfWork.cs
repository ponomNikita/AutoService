using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoService.DAL.Models;

namespace AutoService.DAL
{
    public class UnitOfWork : IDisposable
    {
        private DBContext db = new DBContext();
        private IRepository<User> userRepositrory;
        private IRepository<Role> roleRepositrory;
        private IRepository<User_Role> user_RoleRepositrory;
        private IRepository<Item> itemRepositrory;
        private IRepository<Attachment> attachmentRepositrory;

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

        public IRepository<Item> Items
        {
            get
            {
                if (itemRepositrory == null)
                    itemRepositrory = new Repository<Item>(db);
                return itemRepositrory;
            }
        }

        public IRepository<Attachment> Attachments
        {
            get
            {
                if (attachmentRepositrory == null)
                    attachmentRepositrory = new Repository<Attachment>(db);
                return attachmentRepositrory;
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