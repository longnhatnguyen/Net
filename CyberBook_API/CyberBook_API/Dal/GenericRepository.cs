using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CyberBook_API.Dal
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbset;
        private readonly AppDbContext _entities;
        public GenericRepository()
        {
            this._entities = new AppDbContext();
            _dbset = _entities.Set<T>();
        }
        public GenericRepository(AppDbContext _context)
        {
            this._entities = _context;
            _dbset = _context.Set<T>();
        }

        //public void Delete(object id)
        //{
        //    T existing = _dbset.Find(id);
        //    _dbset.Remove(existing);
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return _dbset.ToList();
        //}

        //public T GetById(object id)
        //{
        //    return _dbset.Find(id);
        //}

        //public void Insert(T obj)
        //{
        //    _dbset.Add(obj);
        //}

        //public void Save()
        //{
        //    _entities.SaveChanges();
        //}

        //public void Update(T obj)
        //{
        //    _dbset.Attach(obj);
        //    _entities.Entry(obj).State = EntityState.Modified;
        //}

        //private readonly DbSet<T> _dbset;
        //private readonly IUnitOfWork _entities;
        //protected GenericRepository(IUnitOfWork entities)
        //{
        //    _entities = entities;
        //    _dbset = _entities.Set<T>();
        //}

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => (IEnumerable<T>)_dbset.Where(predicate).AsEnumerable());
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.Run(() => _dbset.AsEnumerable<T>());
        }

        public async Task<T> Create(T entity)
        {
            _dbset.Add(entity);
            await Save();
            return entity;
        }

        public async Task<int> Update(T entity, int key)
        {
            T existing = _entities.Set<T>().Find(key);
            if (existing != null)
            {
                _entities.Entry(existing).CurrentValues.SetValues(entity);
            }
            return await Save();
        }

        public async Task<int> Delete(T entity)
        {
            _dbset.Remove(entity);
            return await Save();
        }

        public async Task<int> Save()
        {
            try
            {
                return await _entities.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
                //return _entities.Commit();
            }
        }
    }
}
