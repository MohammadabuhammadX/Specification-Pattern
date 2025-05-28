using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        //public List<Expression<Func<T, object>>> ThenIncludes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        protected void AddInclude(Expression<Func<T, Object>> includeExpresstion)
        {
            Includes.Add(includeExpresstion);
        }
        //protected void AddThenIncludes(Expression<Func<T, Object>> thenIncludeExpresstion)
        //{
        //    ThenIncludes.Add(thenIncludeExpresstion);
        //}
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpresstion)
        {
            OrderByDescending = orderByDescExpresstion;
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpresstion)
        {
            OrderBy = orderByExpresstion;
        }
        protected void ApplyPaging(int skip ,  int take)
        {
            Skip = skip;
            Take = take;
        }

    }
}
