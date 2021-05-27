using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAV.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
