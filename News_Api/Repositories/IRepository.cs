using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> Get();
        Task<bool> Save(T entity);
        Task<bool> Delete(int id);
        Task<bool> Update(int id,T entity);

    }
}
