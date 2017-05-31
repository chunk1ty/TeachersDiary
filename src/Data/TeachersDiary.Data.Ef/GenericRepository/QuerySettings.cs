using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeachersDiary.Data.Ef.GenericRepository
{
    public class QuerySettings<TEntity> : IQuerySettings<TEntity>
        where TEntity : class
    {
        private readonly List<Expression<Func<TEntity, object>>> _includes = new List<Expression<Func<TEntity, object>>>();

        public IEnumerable<Expression<Func<TEntity, object>>> IncludePaths
        {
            get { return _includes.ToArray(); }
        }

        public bool ReadOnly { get; set; }

        public Expression<Func<TEntity, bool>> WhereFilter { get; set; }

        public IQuerySettings<TEntity> Include(Expression<Func<TEntity, object>> path)
        {
            _includes.Add(path);

            return this;
        }

        public void Where(Expression<Func<TEntity, bool>> filter)
        {
            WhereFilter = filter;
        }
    }

    // properties ??
    public interface IQuerySettings<TEntity>
        where TEntity : class
    {
        void Where(Expression<Func<TEntity, bool>> filter);

        IQuerySettings<TEntity> Include(Expression<Func<TEntity, object>> path);

        bool ReadOnly { get; set; }

        Expression<Func<TEntity, bool>> WhereFilter { get; set; }

        IEnumerable<Expression<Func<TEntity, object>>> IncludePaths { get; }
    }
}
