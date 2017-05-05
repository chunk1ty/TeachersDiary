using System.Collections.Generic;
using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;

using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Ef
{
    public class TeachersDiaryDbContext : IdentityDbContext<AspNetUser>, ITeachersDiaryDbContext, IUnitOfWork
    {
        public TeachersDiaryDbContext()
            : base("DefaultConnection", false)
        {
        }

        public virtual IDbSet<TeacherEntity> Teachers { get; set; }

        public virtual IDbSet<SchoolEntity> Schools { get; set; }

        public virtual IDbSet<ClassEntity> Classes { get; set; }

        public virtual IDbSet<StudentEntity> Students { get; set; }

        public virtual IDbSet<AbsenceEntity> Absences { get; set; }

        public static TeachersDiaryDbContext Create()
        {
            return new TeachersDiaryDbContext();
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

        public void Insert<T>(IEnumerable<T> entities) where T : class
        {
            Create().BulkInsert(entities);
        }

        public void Update<T>(IEnumerable<T> entities) where T : class
        {
            Create().BulkUpdate(entities);
        }

        public void BulkSave()
        {
            Create().BulkSaveChanges();
        }
    }
}