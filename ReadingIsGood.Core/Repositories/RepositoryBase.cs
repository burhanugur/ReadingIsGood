using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ReadingIsGood.Core.Context;
using ReadingIsGood.Core.Repositories.Interface;
using ReadingIsGood.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseModel
    {
        protected MainDbContext _context { get; }

        private bool disposed = false;

        public RepositoryBase(MainDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync<T>(entity);
            await this.SaveAsync();
            return entity;
        }

        public T Create(T entity)
        {
            _context.Add<T>(entity);
            this.Save();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            T currentEntity = await _context.Set<T>().FindAsync(entity.Id);

            if (currentEntity != null)
            {
                entity.ModifyDate = DateTime.UtcNow;
                _context.Entry(currentEntity).State = EntityState.Modified;
                _context.Entry(currentEntity).CurrentValues.SetValues(entity);
            }

            await this.SaveAsync();
            return currentEntity;
        }

        public T Update(T entity)
        {
            T currentEntity = _context.Set<T>().Find(entity.Id);

            if (currentEntity != null)
            {
                entity.ModifyDate = DateTime.UtcNow;
                _context.Entry(currentEntity).State = EntityState.Modified;
                _context.Update<T>(entity);
            }

            this.Save();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            T currentEntity = await _context.Set<T>().FindAsync(entity.Id);

            if (currentEntity != null)
                _context.Set<T>().Remove(entity);

            await this.SaveAsync();
            return true;
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            this.Save();
            return true;
        }

        public async Task<T> GetAsync(Guid id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result;
        }

        public T Get(Guid id)
        {
            var result = _context.Set<T>().Find(id);
            return result;
        }

        public async Task<long> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public long Count()
        {
            return _context.Set<T>().Count();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            return await query.Where(predicate).ToListAsync();
        }
    }
}
