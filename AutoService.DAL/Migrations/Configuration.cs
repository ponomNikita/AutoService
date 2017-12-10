using System.Collections.Generic;

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

            // Добавление пользователей
            context.Users.AddOrUpdate(
                p => p.Id,
                new User
                {
                    Id = 1,
                    Login = "sys",
                    Email = "sys@mail.com",
                    FirstName = "System",
                    Password = "50b3f4751aa27e17c4bf6d3f85f68699",
                    Roles = new List<Role>
                    {
                        new Role
                        {
                            Code = 1, 
                            Name = "admin",
                            Description = "Администратор системы"
                        }
                    }
                },
                new User
                {
                    Id = 2,
                    Login = "user",
                    Email = "user@mail.com",
                    FirstName = "User",
                    LastName = "User",
                    Password = "50b3f4751aa27e17c4bf6d3f85f68699"
                }
            );
        }
    }
}
