using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISP.DAL.Repositories;

namespace ISP.BLL.ModelActions
{
    public abstract class ModelActionBase<T> where T : class
    {
        public RepositoryBase<T> repository;

        public abstract T GetNotCanceled(Guid id);
        public abstract IEnumerable<T> GetAllNotCanceled();
        public abstract IEnumerable<T> GetAllNotCanceledOrderBy(System.Linq.Expressions.Expression<Func<T, object>> keySelector);
        public abstract IEnumerable<T> GetAllNotCanceledOrderByDescending(System.Linq.Expressions.Expression<Func<T, object>> keySelector);

        public virtual T Get(Guid id)
        {
            return repository.Get(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }
        public virtual IEnumerable<T> GetAllOrderBy(System.Linq.Expressions.Expression<Func<T, object>> keySelector)
        {
            return repository.GetAllOrderBy(keySelector);
        }
        public virtual IEnumerable<T> GetAllOrderByDescending(System.Linq.Expressions.Expression<Func<T, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(keySelector);
        }

        public virtual void Create(T item)
        {
            repository.Create(item);
        }
        public virtual void Edit(T item)
        {
            repository.Edit(item);
        }
        public abstract void Cancel(Guid id);
        public abstract void Cancel(T item);
        public abstract void Renew(Guid id);
        public abstract void Renew(T item);

        public abstract IEnumerable<T> Sort(IEnumerable<T> items, string sortBy, bool orderByDescending);
        public abstract void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending);
    }
}