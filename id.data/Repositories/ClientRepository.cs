using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using IdentityServer4.EntityFramework.Interfaces;

namespace id.data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        IConfigurationDbContext _db;
        public ClientRepository(IConfigurationDbContext db)
        {
            _db = db;
        }

        public void Add(Client entity)
        {
            _db.Clients.Add(entity);
        }

        public IConfigurationDbContext DbContext()
        {
            return _db;
        }

        public void Delete(object id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        public void Delete(Client entity)
        {
            _db.Clients.Remove(entity);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Edit(Client entity)
        {
            _db.Clients.Attach(entity);
        }

        public IQueryable<Client> FindBy(Expression<Func<Client, bool>> predicate)
        {
            return _db.Clients.Where(predicate);
        }

        public Client Get(object id)
        {
            return _db.Clients.Find(id);
        }

        public IQueryable<Client> GetAll()
        {
            return _db.Clients;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        DbContext IGenericRepository<Client>.DbContext()
        {
            throw new NotImplementedException();
        }
    }
}
