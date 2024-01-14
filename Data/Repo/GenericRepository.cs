using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Repo
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

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _tableRef;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
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

        public async Task<T> Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _tableRef;

            query = query.Where(expression);

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<PageResult<T>> GetPaginateList(
            int pageNo = 1, int pageSize = 10,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes
            )
        {
            IQueryable<T> query = _tableRef;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            int count = await query.CountAsync();

            int totalPages = (int)(count > 0 ? Math.Ceiling((double)count / pageSize) : 0);
            var skip = pageSize * (pageNo - 1);
            query = query.Skip(skip).Take(pageSize);

            var resultList = await query.AsNoTracking().ToListAsync();

            var pageResult = new PageResult<T>
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalRecords = count,
                TotalPages = totalPages,
                ResultList = resultList
            };

            return pageResult;
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
            if(entity != null)
            {
                _tableRef.Remove(entity);
            }

            await Task.FromResult(0);
        }

        public async void DeleteRange(IEnumerable<T> entities)
        {
            if(entities != null && entities.Any())
            {
                _tableRef.RemoveRange(entities);
            }
            await Task.FromResult(0);
        }
    }
}
