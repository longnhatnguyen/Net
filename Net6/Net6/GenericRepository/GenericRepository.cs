using Microsoft.EntityFrameworkCore;
using Net6.Models;
using System;
using System.Linq.Expressions;

namespace Net6.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly DbSet<T> _dbset;
        private readonly Learn_DBContext _DBContext;

        public GenericRepository(DbSet<T> dbset, Learn_DBContext dBContext)
        {
            _dbset = dbset ?? throw new ArgumentNullException(nameof(dbset));
            _DBContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
        }

        public Task<T> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.Run(() => _dbset.AsEnumerable<T>());
        }

        public Task<int> Save()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(T entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}
