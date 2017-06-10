using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using TeachersDiary.Data.Ef.Models;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Contracts
{
    public interface ITeachersDiaryDbContext
    {
        IDbSet<UserEntity> Users { get; set; }

        IDbSet<TeacherEntity> Teachers { get; set; }

        IDbSet<SchoolEntity> Schools { get; set; }

        IDbSet<ClassEntity> Classes { get; set; }

        IDbSet<StudentEntity> Students { get; set; }

        IDbSet<AbsenceEntity> Absences { get; set; }

        IDbSet<SchoolAdminEntity> SchoolAdmins { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
