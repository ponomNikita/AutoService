using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL.Models;

namespace AutoService.DAL
{
    public class Repository<T> : IRepository<T>
        where T : TEntity
    {
        DBContext db;
        DbSet<T> dbSet;

        public Repository(DBContext _db)
        {
            typeof(T).ToString();
            this.db = _db;
            this.dbSet = this.db.Set<T>();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Any(predicate);
        }

        public void Create(T item)
        {
            dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var entity = dbSet.FirstOrDefault(o => o.id == id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public T Get(int id)
        { 
            return dbSet.FirstOrDefault(o => o.id == id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public IQueryable<T> Include(Expression<Func<T, object>> predicate)
        {
            return dbSet.Include(predicate);
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}
