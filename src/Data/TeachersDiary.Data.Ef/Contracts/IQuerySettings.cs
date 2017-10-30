using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TeachersDiary.Data.Ef.Contracts
{
    public interface IQuerySettings<TEntity>
        where TEntity : class
    {
        void Where(Expression<Func<TEntity, bool>> filter);

        IQuerySettings<TEntity> Include(Expression<Func<TEntity, object>> path);

        bool ReadOnly { get; set; }

        Expression<Func<TEntity, bool>> WhereFilter { get; }

        IEnumerable<Expression<Func<TEntity, object>>> IncludePaths { get; }
    }
}