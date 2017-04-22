using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TeacherDiary.Data.Ef.Models;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Ef
{
    public class TeacherDiaryDbContext : IdentityDbContext<AspNetUser>
    {
        public TeacherDiaryDbContext()
            : base("DefaultConnection", false)
        {
        }

        public virtual IDbSet<Class> Classes { get; set; }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Absence> Absences { get; set; }

        public virtual IDbSet<Month> Months { get; set; }

        public static TeacherDiaryDbContext Create()
        {
            return new TeacherDiaryDbContext();
        }
    }
}