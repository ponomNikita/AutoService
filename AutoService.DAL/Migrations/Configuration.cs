namespace Tbs16.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using AutoService.DAL.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoService.DAL.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "AutoService.DAL.DBContext";
        }

        protected override void Seed(AutoService.DAL.DBContext context)
        {
            // Добавление модулей
            context.Modules.AddOrUpdate(
                p => p.Code,
              new Module
              {
                  Code = 2,
                  Name = "AddingSearchToNavigationBar",
                  Description = "Добавляет поиск по сайту"
              }
            );
                   
            context.Roles.AddOrUpdate(
                p => p.Code,
                new Role
                {
                    id = 1,
                    Code = 1,
                    Name = "admin",
                    Description = "Администратор системы"
                }
            );

            context.Users.AddOrUpdate(
                p => p.id,
                new User
                {
                    id = 1,
                    Login = "sys",
                    Email = "sys@mail.com",
                    FirstName = "System",
                    Password = "50b3f4751aa27e17c4bf6d3f85f68699"
                },
                new User
                {
                    id = 2,
                    Login = "user",
                    Email = "user@mail.com",
                    FirstName = "User",
                    LastName = "User",
                    Password = "50b3f4751aa27e17c4bf6d3f85f68699"
                }
            );

            context.User_Roles.AddOrUpdate(
                p => p.id,
                new User_Role
                {
                    roleId = 1,
                    userId = 1
                }
            );
        }
    }
}
