using System;
using System.Linq;
using System.Linq.Expressions;
using AutoService.DAL.Models;

namespace AutoService.DAL
{
    public interface IRepository<T> where T: TEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        bool Any(Expression<Func<T,bool>> predicate);
        IQueryable<T> Include(Expression<Func<T, object>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
