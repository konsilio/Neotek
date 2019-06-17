using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using Sagas.MainModule.IRepositorios;

namespace Infrastructure.Data.Repositorios
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbContext entities = null;
        protected IDbSet<T> _objectSet;

        public GenericRepository(DbContext _entities)
        {
            entities = _entities;
            entities.ChangeTracker.HasChanges();
            _objectSet = entities.Set<T>();
        }
        public IEnumerable<T> Get(
                    Expression<Func<T, bool>> filter,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    string includeProperties = "")
        {
            IQueryable<T> query = _objectSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public IEnumerable<T> GetAll()
        {
            return _objectSet.AsEnumerable();
        }
        public T GetSingleByID(object id)
        {
            return _objectSet.Find(id);
        }
        public T GetSingle(Func<T, bool> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
        public int Count()
        {
            return _objectSet.Count();
        }
        public void Attach(T entity)
        {
            _objectSet.Attach(entity);
        }
        public void Insert(T entity)
        {
            _objectSet.Add(entity);
        }
        public void Delete(object id)
        {
            T entity = _objectSet.Find(id);
            _objectSet.Remove(entity);
        }
        public void Delete(T entity)
        {
            if (entities.Entry(entity).State == EntityState.Detached)            
                _objectSet.Attach(entity);            
            _objectSet.Remove(entity);
        }
        public void Update(T entity)
        {
            _objectSet.Attach(entity);
            entities.Entry(entity).State = EntityState.Modified;
        }
        public void Save()
        {
            entities.SaveChanges();
        }

    }
}
