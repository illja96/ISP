using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected ISPContext context;

        public RepositoryBase()
        {
            context = new ISPContext();
        }
        public RepositoryBase(ISPContext context)
        {
            this.context = context;
        }

        public virtual T Get(Guid id)
        {
            return context.Set<T>().Find(id);
        }
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().First(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public virtual IEnumerable<T> GetAllOrderBy(Expression<Func<T, object>> keySelector)
        {
            return context.Set<T>().OrderBy(keySelector);
        }
        public virtual IEnumerable<T> GetAllOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> keySelector)
        {
            return context.Set<T>().Where(predicate).OrderBy(keySelector);
        }

        public virtual IEnumerable<T> GetAllOrderByDescending(Expression<Func<T, object>> keySelector)
        {
            return context.Set<T>().OrderByDescending(keySelector);
        }
        public virtual IEnumerable<T> GetAllOrderByDescending(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> keySelector)
        {
            return context.Set<T>().Where(predicate).OrderByDescending(keySelector);
        }

        public virtual void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }
        public virtual void Edit(T item)
        {
            context.Entry<T>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public virtual int Count()
        {
            return context.Set<T>().Count();
        }
    }
}