using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeachersDiary.Data.Ef.GenericRepository.Contracts
{
    public interface IEntityFrameworkGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> All(QuerySettings<TEntity> includes = null);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entity);
    }
}
