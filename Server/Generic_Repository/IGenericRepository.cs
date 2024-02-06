using System.Linq.Expressions;

namespace Server.Generic_Repository
{
    public interface IGenericRepository
    {
        Task<IQueryable<T>> GetFromMutlipleTableWithPagination<T>(int pageIndex, int pageSize, params Expression<Func<T, object>>[] includes) where T : class;

        Task<T> Get<T>(Expression<Func<T, bool>> filters) where T : class;

        Task<T> Get<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includeProperties) where T : class;

        Task<IEnumerable<T>> GetAll<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;

        Task<IEnumerable<T>> GetAll<T>() where T : class;

        Task<T> Post<T>(T entity) where T : class;

        Task<T> Put<T>(T entity) where T : class;

        Task<bool> Delete<T>(T entity) where T : class;
    }
}
