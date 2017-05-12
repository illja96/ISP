using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    interface IRepository<T> : IDisposable where T : class
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllOrderBy(System.Linq.Expressions.Expression<Func<T, object>> keySelector);
        IEnumerable<T> GetAllOrderByDescending(System.Linq.Expressions.Expression<Func<T, object>> keySelector);

        T GetCancelled(Guid id);
        IEnumerable<T> GetAllCancelled();
        IEnumerable<T> GetAllCancelledOrderBy(System.Linq.Expressions.Expression<Func<T, object>> keySelector);
        IEnumerable<T> GetAllCancelledOrderByDescending(System.Linq.Expressions.Expression<Func<T, object>> keySelector);

        void Create(T item);
        void Edit(T item);
        void Cancel(Guid id);
        void Cancel(T item);
        void Renew(Guid id);
        void Renew(T item);
    }
}