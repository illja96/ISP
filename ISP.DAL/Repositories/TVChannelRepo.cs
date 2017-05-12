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
            if(item.IsTV == false && item.IsIPTV == false)
            {
                item.IsCanceled = true;
            }
            context.TVChannels.Add(item);
            context.SaveChanges();
        }
        public void Edit(TVChannel item)
        {
            if (item.IsTV == false && item.IsIPTV == false)
            {
                item.IsCanceled = true;
            }
            context.Entry<TVChannel>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Cancel(Guid id)
        {
            TVChannel item = GetCancelled(id);
            item.IsCanceled = true;
            context.Entry<TVChannel>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Cancel(TVChannel item)
        {
            item.IsCanceled = true;
            context.Entry<TVChannel>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Renew(Guid id)
        {
            TVChannel item = GetCancelled(id);
            if (item.IsTV == false && item.IsIPTV == false)
            {
                item.IsTV = true;
                item.IsIPTV = true;
            }
            item.IsCanceled = false;
            context.Entry<TVChannel>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Renew(TVChannel item)
        {
            if (item.IsTV == false && item.IsIPTV == false)
            {
                item.IsTV = true;
                item.IsIPTV = true;
            }
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

        public IEnumerable<TVChannel> Sort(IEnumerable<TVChannel> tvChannels, string sortBy, bool orderByDescending)
        {
            switch (sortBy)
            {
                case "Name":
                    if (orderByDescending)
                        return tvChannels.OrderByDescending(tvChannel => tvChannel.Name);
                    else
                        return tvChannels.OrderBy(tvChannel => tvChannel.Name);

                case "Price":
                    if (orderByDescending)
                        return tvChannels.OrderByDescending(tvChannel => tvChannel.Price);
                    else
                        return tvChannels.OrderBy(tvChannel => tvChannel.Price);

                case "IsTV":
                    if (orderByDescending)
                        return tvChannels.OrderByDescending(tvChannel => tvChannel.IsTV);
                    else
                        return tvChannels.OrderBy(tvChannel => tvChannel.IsTV);

                case "IsIPTV":
                    if (orderByDescending)
                        return tvChannels.OrderByDescending(tvChannel => tvChannel.IsIPTV);
                    else
                        return tvChannels.OrderBy(tvChannel => tvChannel.IsIPTV);

                case "IsCanceled":
                    if (orderByDescending)
                        return tvChannels.OrderByDescending(tvChannel => tvChannel.IsCanceled);
                    else
                        return tvChannels.OrderBy(tvChannel => tvChannel.IsCanceled);

                default:
                    return tvChannels;
            }
        }
        public void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            sortBy = new Dictionary<string, string>()
            {
                {"По умолчанию", "Id" },
                {"По названию", "Name" },
                { "По наличию TV", "IsTV" },
                { "По наличию IPTV","IsIPTV" },
                { "По цене","Price" },
                { "По состоянию","IsCanceled" }
            };

            orderByDescending = new Dictionary<string, bool>()
            {
                { "По возрастанию", false },
                { "По убыванию", true }
            };
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}