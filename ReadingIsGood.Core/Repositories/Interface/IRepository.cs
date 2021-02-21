using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories.Interface
{
    public interface IRepository<T>
    {
        void Dispose();

        void Save();

        Task<T> CreateAsync(T entity);
        T Create(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);
        bool Delete(T entity);

        Task<T> GetAsync(Guid id);
        T Get(Guid id);

        Task<long> CountAsync();
        long Count();

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}
