using System.Data.Entity.Migrations;
using System.Linq;
using System.Collections.Generic;
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
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TeachersDiaryDbContext context)
        {
            CreateRoles(context);
            AddShchools(context);
        }

        private void AddShchools(TeachersDiaryDbContext context)
        {
            if (!context.Schools.Any(x => x.Name == "СОУ Димитър Благоев"))
            {
                context.Schools.Add(new SchoolEntity()
                {
                    Name = "СОУ Димитър Благоев"
                });

                context.SaveChanges();
            }
        }

        private void CreateRoles(TeachersDiaryDbContext context)
        {
            if (context.Roles.Any())
                return;
            var roles = new List<RoleEntity>
            {
                new RoleEntity
                {
                    Name = ApplicationRole.Student,
                    IsVisible = true
                },
                new RoleEntity
                {
                    Name = ApplicationRole.Teacher,
                    IsVisible = true
                },
                new RoleEntity
                {
                    Name = ApplicationRole.SchoolAdministrator,
                    IsVisible = false
                },
                new RoleEntity
                {
                    Name = ApplicationRole.Administrator,
                    IsVisible = false
                }
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);

                context.SaveChanges(); ;
            }
        }
    }
}
