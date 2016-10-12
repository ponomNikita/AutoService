using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoService.DAL.Models;

namespace AutoService.DAL
{
    class DBContext : DbContext
    {
        public DBContext() : base("AutoService")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Module> Modules { get; set; }
    }
}
