using System.Data.Entity;

using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Migrations;

namespace TeachersDiary.Clients.Mvc
{
    public class DbConfig
    {
        public static void RegisterDb()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TeachersDiaryDbContext, Configuration>());
        }
    }
}