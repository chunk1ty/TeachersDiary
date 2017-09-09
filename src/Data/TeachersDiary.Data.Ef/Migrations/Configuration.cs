using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using TeachersDiary.Common.Constants;
using TeachersDiary.Data.Entities;

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
            if (!context.Schools.Any(x => x.Name == "СУ Димитър Благоев"))
            {
                context.Schools.Add(new SchoolEntity()
                {
                    Name = "СУ Димитър Благоев"
                });

                context.SaveChanges();
            }
        }

        private void CreateRoles(TeachersDiaryDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole()
                {
                    Name = ApplicationRole.Student
                });

                context.Roles.Add(new IdentityRole()
                {
                    Name = ApplicationRole.Teacher
                });

                context.Roles.Add(new IdentityRole()
                {
                    Name = ApplicationRole.SchoolAdministrator
                });

                context.Roles.Add(new IdentityRole()
                {
                    Name = ApplicationRole.Administrator
                });

                context.SaveChanges();
            } 
        }
    }
}
