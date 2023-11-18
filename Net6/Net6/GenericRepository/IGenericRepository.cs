using System.Linq.Expressions;

namespace Net6.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //IEnumerable<T> GetAll();
        //T GetById(object id);
        //void Insert(T obj);
        //void Update(T obj);
        //void Delete(object id);
        //void Save();
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
        Task<int> Update(T entity, int key);
        Task<int> Delete(T entity);
        Task<int> Save();
    }
}
