using BuyandRentHomeWebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BuyRentHomeDbContext _dbContext;
        private readonly DbSet<T> _tableRef;

        public GenericRepository(BuyRentHomeDbContext dbContext)
        {
            _dbContext = dbContext;
            _tableRef = _dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _tableRef;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _tableRef;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task Insert(T entity)
        {
            await _tableRef.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _tableRef.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _tableRef.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _tableRef.UpdateRange(entities);
        }

        public async Task Delete(int id)
        {
            var entity = await _tableRef.FindAsync(id);
            _tableRef.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _tableRef.RemoveRange(entities);
        }
    }
}
