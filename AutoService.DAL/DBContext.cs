using System.Data.Entity;
using AutoService.DAL.Models;

namespace AutoService.DAL
{
    public class DBContext : DbContext
    {
        private static DBContext instance;
        public DBContext() : base("AutoService")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<CoordinationRequest> CoordinationRequests { get; set; }
        public DbSet<CoordinationResponse> CoordinationResponses { get; set; }

        public static DBContext GetDBContext()
        {
            if (instance == null)
            {
                instance = new DBContext();
                return instance;
            }
            
            return instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoordinationRequest>()
                        .HasRequired(s => s.Application)
                        .WithMany(s => s.CoordinationRequests);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();
        }
    }
}
