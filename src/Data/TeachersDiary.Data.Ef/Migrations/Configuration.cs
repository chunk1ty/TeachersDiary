using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Ef.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<TeachersDiaryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(TeachersDiaryDbContext context)
        {
            CreateRoles(context);
        }

        private void CreateRoles(TeachersDiaryDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<string>
                {
                    Roles.Student,
                    Roles.Teacher,
                    Roles.SchoolAdministrator,
                    Roles.Administrator
                };

                foreach (var roleName in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    var role = new IdentityRole { Name = roleName };

                    roleManager.Create(role);
                }
            }
        }
    }
}
