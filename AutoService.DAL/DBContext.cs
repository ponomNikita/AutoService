using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoService.DAL.Models;

namespace AutoService.DAL
{
    public class DBContext : DbContext
    {
        private static DBContext instance;
        private DBContext() : base("AutoService")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Application> Applications { get; set; }

        public static DBContext GetDBContext()
        {
            if (instance == null)
            {
                instance = new DBContext();
                return instance;
            }
            else
            {
                return instance;
            }
        }
    }
}
