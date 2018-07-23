using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Sagas.MainModule.IRepositorios;
using Infrastructure.Data.Repositorios;

namespace Application.MainModule.UnitOfWork
{
    public class GenericUnitOfWork<TContext> : IGenericUnitOfWork<TContext>, IDisposable where TContext : DbContext, new()
    {
        public DbContext entities;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public GenericUnitOfWork()
        {
            entities = new TContext();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            // Validation: Valida la existencia de la entidad dentro del repositorio.
            //             En caso de existir no lo cargará de nuevo.
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new GenericRepository<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public IRepository<T> RepositoryWithoutValidation<T>() where T : class
        {
            // Validation out: No realiza la validación de existencia de la entidad en el repositorio
            IRepository<T> repo = new GenericRepository<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges()
        {
            entities.SaveChanges();
        }

        public int Save()
        {
            return entities.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}