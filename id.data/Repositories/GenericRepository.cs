using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace id.data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext _db;
        private DbSet<T> table = null;
        public GenericRepository(DbContext db)
        {
            _db = db;
            table = _db.Set<T>();
        }

        public DbContext DbContext()
        {
            return _db;
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }

        public virtual T Get(object id)
        {
            var entity = table.Find(id);
            return entity;
        }

        public virtual void Add(T entity)
        {
            table.Add(entity);
        }


        public virtual void Delete(T entity)
        {
            //var collectionProperties = entity.GetType().GetProperties().Where(x=>x.PropertyType.Name.Contains("ICollection"));
            //foreach (var property in collectionProperties)
            //{
            //    _db.Entry(entity).Collection(property.Name).Load();
            //}
            //_db.Entry(entity).Collection("").Load();
            table.Remove(entity);

        }

        public virtual void Delete(object id)
        {
            var entity = Get(id);
            Delete(entity);
            _db.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void Edit(T entity)
        {
            table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _db.SaveChanges();
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
        
    }
}
