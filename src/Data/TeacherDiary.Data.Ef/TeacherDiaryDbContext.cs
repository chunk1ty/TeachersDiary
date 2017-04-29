using System.Collections.Generic;
using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;

using TeacherDiary.Data.Ef.Contracts;
using TeacherDiary.Data.Ef.Models;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Ef
{
    public class TeacherDiaryDbContext : IdentityDbContext<AspNetUser>, ITeacherDiaryDbContext, ITeacherDiaryDbContextSaveChanges
    {
        public TeacherDiaryDbContext()
            : base("DefaultConnection", false)
        {
        }

        public virtual IDbSet<Teacher> Teachers { get; set; }

        public virtual IDbSet<School> Schools { get; set; }

        public virtual IDbSet<Class> Classes { get; set; }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Absence> Absences { get; set; }

        public virtual IDbSet<Month> Months { get; set; }

        public static TeacherDiaryDbContext Create()
        {
            return new TeacherDiaryDbContext();
        }

        public new IDbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}