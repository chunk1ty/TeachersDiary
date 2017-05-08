using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using TeachersDiary.Data.Ef.Entities;

namespace TeachersDiary.Data.Ef.Contracts
{
    public interface ITeachersDiaryDbContext
    {
        IDbSet<UserEntity> Users { get; set; }

        // TODO WHY is not working ??
        //IDbSet<RoleEntity> Roles { get; set; }

        IDbSet<TeacherEntity> Teachers { get; set; }

        IDbSet<SchoolEntity> Schools { get; set; }

        IDbSet<ClassEntity> Classes { get; set; }

        IDbSet<StudentEntity> Students { get; set; }

        IDbSet<AbsenceEntity> Absences { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Insert<T>(IEnumerable<T> entities) where T : class;

        void Update<T>(IEnumerable<T> entities) where T : class;

        void BulkSave();

        void SaveChanges();
    }
}
