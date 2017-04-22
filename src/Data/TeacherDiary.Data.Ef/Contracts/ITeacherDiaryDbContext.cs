using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Ef.Contracts
{
    public interface ITeacherDiaryDbContext
    {
        IDbSet<Class> Classes { get; set; }

        IDbSet<Student> Students { get; set; }

        IDbSet<Absence> Absences { get; set; }

        IDbSet<Month> Months { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
