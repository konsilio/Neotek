using System;
using System.Data.Entity;
using Sagas.MainModule.IRepositorios;

namespace Application.MainModule.UnitOfWork
{
    public interface IGenericUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IRepository<T> Repository<T>() where T : class;
        void SaveChanges();
    }
}
