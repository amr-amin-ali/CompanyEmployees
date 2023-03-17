using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext) => _repositoryContext = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges) 
            =>
            trackChanges ? _repositoryContext.Set<T>() :
            _repositoryContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
            => 
            trackChanges ? _repositoryContext.Set<T>().Where(expression) :
            _repositoryContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity)=> _repositoryContext.Set<T>().Add(entity);

        public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);

        public void Update(T entity)=> _repositoryContext.Set<T>().Update(entity);
    }
}
