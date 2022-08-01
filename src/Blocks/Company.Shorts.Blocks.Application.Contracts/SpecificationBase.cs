namespace Company.Shorts.Blocks.Application.Contracts
{
    using System.Linq.Expressions;

    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        public SpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public List<string> IncludeStrings { get; } = new();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public Expression<Func<T, object>>? GroupBy { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected virtual void AddInclude(Expression<Func<T, object>> expression)
        {
            Includes.Add(expression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected virtual void ApplyPaging(int skip, int take)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = true;
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> expression)
        {
            OrderBy = expression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> expression)
        {
            OrderByDescending = expression;
        }

        protected virtual void ApplyGroupBy(Expression<Func<T, object>> expression)
        {
            GroupBy = expression;
        }
    }
}