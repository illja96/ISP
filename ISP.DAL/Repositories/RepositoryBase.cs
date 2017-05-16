using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        public ISPContext context;

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
        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }
        public virtual IEnumerable<T> GetAllOrderBy(System.Linq.Expressions.Expression<Func<T, object>> keySelector)
        {
            return context.Set<T>().OrderBy(keySelector);
        }
        public virtual IEnumerable<T> GetAllOrderByDescending(System.Linq.Expressions.Expression<Func<T, object>> keySelector)
        {
            return context.Set<T>().OrderByDescending(keySelector);
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
    }
}