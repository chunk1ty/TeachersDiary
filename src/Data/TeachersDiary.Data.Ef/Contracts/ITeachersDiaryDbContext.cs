using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Contracts
{
    public interface ITeachersDiaryDbContext
    {
        IDbSet<Teacher> Teachers { get; set; }

        IDbSet<School> Schools { get; set; }

        IDbSet<Class> Classes { get; set; }

        IDbSet<Student> Students { get; set; }

        IDbSet<Absence> Absences { get; set; }

        IDbSet<Month> Months { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Insert<T>(IEnumerable<T> entities) where T : class;

        void Update<T>(IEnumerable<T> entities) where T : class;

        void BulkSave();

        void SaveChanges();
    }
}
