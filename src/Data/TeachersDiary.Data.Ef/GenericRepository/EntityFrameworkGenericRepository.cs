using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.Extensions;

namespace TeachersDiary.Data.Ef.GenericRepository
{
    public class EntityFrameworkGenericRepository<TEntity> : IEntityFrameworkGenericRepository<TEntity> where TEntity : class 
    {
        private readonly ITeachersDiaryDbContext _teachersDiaryDbContext;

        public EntityFrameworkGenericRepository(ITeachersDiaryDbContext teachersDiaryDbContext)
        {
            if (teachersDiaryDbContext == null)
            {
                throw new ArgumentNullException(nameof(teachersDiaryDbContext));
            }

            _teachersDiaryDbContext = teachersDiaryDbContext;
            DbSet = _teachersDiaryDbContext.Set<TEntity>();
        }

        protected IDbSet<TEntity> DbSet { get; set; }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IQuerySettings<TEntity> setting = null)
        {
            IQueryable<TEntity> query = _teachersDiaryDbContext.Set<TEntity>();

            if (setting != null)
            {
                if (setting.IncludePaths != null)
                {
                    foreach (var path in setting.IncludePaths)
                    {
                        query = query.Include(path);
                    }
                }

                if (setting.WhereFilter != null)
                {
                    query = query.Where(setting.WhereFilter);
                }

                if (setting.ReadOnly)
                {
                    query = query.AsNoTracking();
                }
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _teachersDiaryDbContext.Set<TEntity>().FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            var entry = _teachersDiaryDbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            _teachersDiaryDbContext.Set<TEntity>().AddRange(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _teachersDiaryDbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            var entry = _teachersDiaryDbContext.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            _teachersDiaryDbContext.Set<TEntity>().RemoveRange(entity);
        }
    }
}
