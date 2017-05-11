using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ISP.DAL.Repositories
{
    public class TVChannelRepo : IRepository<TVChannel>
    {
        private ISPContext context;

        public TVChannelRepo() { context = new ISPContext(); }
        public TVChannelRepo(ISPContext context) { this.context = context; }

        public void Create(TVChannel item)
        {
            item.Id = Guid.NewGuid();
            context.TVChannels.Add(item);
            context.SaveChanges();
        }
        public void Edit(TVChannel item)
        {
            context.Entry<TVChannel>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Cancel(TVChannel item)
        {
            item.IsCanceled = true;
            context.Entry<TVChannel>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Renew(TVChannel item)
        {
            item.IsCanceled = false;
            context.Entry<TVChannel>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public TVChannel Get(Guid id)
        {
            return context.TVChannels.First(tvChannel => tvChannel.IsCanceled == false && tvChannel.Id == id);
        }
        public IEnumerable<TVChannel> GetAll()
        {
            return context.TVChannels.Where(tvChannel => tvChannel.IsCanceled == false);
        }
        public IEnumerable<TVChannel> GetAllOrderBy(Expression<Func<TVChannel, object>> keySelector)
        {
            return context.TVChannels.Where(tvChannel => tvChannel.IsCanceled == false).OrderBy(keySelector);
        }
        public IEnumerable<TVChannel> GetAllOrderByDescending(Expression<Func<TVChannel, object>> keySelector)
        {
            return context.TVChannels.Where(tvChannel => tvChannel.IsCanceled == false).OrderByDescending(keySelector);
        }

        public TVChannel GetCancelled(Guid id)
        {
            return context.TVChannels.First(tvChannel => tvChannel.Id == id);
        }
        public IEnumerable<TVChannel> GetAllCancelled()
        {
            return context.TVChannels;
        }
        public IEnumerable<TVChannel> GetAllCancelledOrderBy(Expression<Func<TVChannel, object>> keySelector)
        {
            return context.TVChannels.OrderBy(keySelector);
        }
        public IEnumerable<TVChannel> GetAllCancelledOrderByDescending(Expression<Func<TVChannel, object>> keySelector)
        {
            return context.TVChannels.OrderByDescending(keySelector);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}