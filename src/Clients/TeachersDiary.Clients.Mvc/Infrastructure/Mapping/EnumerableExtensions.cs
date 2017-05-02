using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Mapping
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TDestination> To<TDestination>(this IEnumerable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            // TODO implement new mapping strategy
            return source.AsQueryable().ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}
