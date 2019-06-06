using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Sagas.MainModule.IRepositorios
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        IEnumerable<T> GetAll();
        T GetSingleByID(object id);
        T GetSingle(Func<T, bool> where);
        int Count();
        void Attach(T entity);
        void Insert(T entity);
        void Delete(object id);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}
