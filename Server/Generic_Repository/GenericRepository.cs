using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Linq.Expressions;

namespace Server.Generic_Repository
{
    public class GenericRepository: IGenericRepository
    {
        private readonly CollegeDataContext _context;

        public GenericRepository(CollegeDataContext context)
        {
            _context = context;

        }

        public async Task<IQueryable<T>> GetFromMutlipleTableWithPagination<T>(int pageIndex, int pageSize, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query, (current, includes) => current.Include(includes)).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> filters) where T : class
        {
            return await _context.Set<T>().Where(filters).FirstOrDefaultAsync();
        }


        public async Task<T> Get<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includeProperties) where T : class
        {

            var filteredData = _context.Set<T>().Where(filters);
            if (includeProperties != null)
                foreach (var property in includeProperties)
                    await filteredData.Include(property).LoadAsync();


            return await filteredData.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            var data = _context.Set<T>();
            foreach (var property in includeProperties)
                await data.Include(property).LoadAsync();

            return await data.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Post<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Put<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(true);
            return entity;
        }

    }
}
