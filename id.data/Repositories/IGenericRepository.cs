using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace id.data.Repositories
{
    public interface IGenericRepository<T>: IDisposable where T : class
    {
        DbContext DbContext();
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Get(object id);
        void Add(T entity);
        void Delete(T entity);
        void Delete(object id);
        void Edit(T entity);
        void Save();
    }
}
