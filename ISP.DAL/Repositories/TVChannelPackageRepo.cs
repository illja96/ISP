using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ISP.DAL.Repositories
{
    public class TVChannelPackageRepo : IRepository<TVChannelPackage>
    {
        private ISPContext context;

        public TVChannelPackageRepo() { context = new ISPContext(); }
        public TVChannelPackageRepo(ISPContext context) { this.context = context; }

        public void Create(TVChannelPackage item)
        {
            context.TVChannelPackages.Add(item);
            context.SaveChanges();
        }
        public void Edit(TVChannelPackage item)
        {
            context.Entry<TVChannelPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Cancel(Guid id)
        {
            TVChannelPackage item = Get(id);
            item.IsCanceled = true;
            context.Entry<TVChannelPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Cancel(TVChannelPackage item)
        {
            item.IsCanceled = true;
            context.Entry<TVChannelPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Renew(Guid id)
        {
            TVChannelPackage item = Get(id);
            item.IsCanceled = false;
            context.Entry<TVChannelPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Renew(TVChannelPackage item)
        {
            item.IsCanceled = false;
            context.Entry<TVChannelPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public TVChannelPackage GetNotCanceled(Guid id)
        {
            return context.TVChannelPackages.First(tvChannelPackage => tvChannelPackage.Id == id && tvChannelPackage.IsCanceled == false);
        }
        public IEnumerable<TVChannelPackage> GetAllNotCanceled()
        {
            return context.TVChannelPackages.Where(tvChannelPackage => tvChannelPackage.IsCanceled == false);
        }
        public IEnumerable<TVChannelPackage> GetAllNotCanceledOrderBy(Expression<Func<TVChannelPackage, object>> keySelector)
        {
            return context.TVChannelPackages.Where(tvChannelPackage => tvChannelPackage.IsCanceled == false).OrderBy(keySelector);
        }
        public IEnumerable<TVChannelPackage> GetAllNotCanceledOrderByDescending(Expression<Func<TVChannelPackage, object>> keySelector)
        {
            return context.TVChannelPackages.Where(tvChannelPackage => tvChannelPackage.IsCanceled == false).OrderByDescending(keySelector);
        }

        public TVChannelPackage Get(Guid id)
        {
            return context.TVChannelPackages.First(tvChannelPackage => tvChannelPackage.Id == id);
        }
        public IEnumerable<TVChannelPackage> GetAll()
        {
            return context.TVChannelPackages;
        }
        public IEnumerable<TVChannelPackage> GetAllOrderBy(Expression<Func<TVChannelPackage, object>> keySelector)
        {
            return context.TVChannelPackages.OrderBy(keySelector);
        }
        public IEnumerable<TVChannelPackage> GetAllOrderByDescending(Expression<Func<TVChannelPackage, object>> keySelector)
        {
            return context.TVChannelPackages.OrderByDescending(keySelector);
        }

        public IEnumerable<TVChannelPackage> Sort(IEnumerable<TVChannelPackage> items, string sortBy, bool orderByDescending)
        {
            switch(sortBy)
            {
                case "Name":
                    if (orderByDescending)
                        return items.OrderByDescending(tvChannelPackage => tvChannelPackage.Name);
                    else
                        return items.OrderBy(tvChannelPackage => tvChannelPackage.Name);

                case "Channels":
                    if (orderByDescending)
                        return items.OrderByDescending(tvChannelPackage => tvChannelPackage.Channels.Count());
                    else
                        return items.OrderByDescending(tvChannelPackage => tvChannelPackage.Channels.Count());

                case "Price":
                    if (orderByDescending)
                        return items.OrderByDescending(tvChannelPackage => tvChannelPackage.Price);
                    else
                        return items.OrderBy(tvChannelPackage => tvChannelPackage.Price);

                case "IsCanceled":
                    if (orderByDescending)
                        return items.OrderByDescending(tvChannelPackage => tvChannelPackage.IsCanceled);
                    else
                        return items.OrderBy(tvChannelPackage => tvChannelPackage.IsCanceled);

                default:
                    return items;
            }
        }
        public void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            sortBy = new Dictionary<string, string>()
            {
                { "По умолчанию", "Id" },
                { "По названию", "Name" },
                { "По количеству каналов", "Channels" },
                { "По цене", "Price" },
                { "По состоянию", "IsCanceled" }
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

        //Custom methods
        public void AddChannelToPackage(Guid tvChannelPackageId, Guid tvChannelId)
        {
            TVChannelPackage tvChannelPackageToAddChannel = context.TVChannelPackages.First(tvChannelPackage => tvChannelPackage.Id == tvChannelPackageId);
            if (tvChannelPackageToAddChannel.Channels.Count(tvChannel => tvChannel.Id == tvChannelId) == 0)
            {
                TVChannel tvChannelToAdd = context.TVChannels.First(tvChannel => tvChannel.Id == tvChannelId);
                tvChannelPackageToAddChannel.Channels.Add(tvChannelToAdd);
                context.Entry<TVChannelPackage>(tvChannelPackageToAddChannel).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void RemoveChannelFromPackage(Guid tvChannelPackageId, Guid tvChannelId)
        {
            TVChannelPackage tvChannelPackageToRemoveChannel = context.TVChannelPackages.First(tvChannelPackage => tvChannelPackage.Id == tvChannelPackageId);
            if (tvChannelPackageToRemoveChannel.Channels.Count(tvChannel => tvChannel.Id == tvChannelId) != 0)
            {
                TVChannel tvChannelToRemove = context.TVChannels.First(tvChannel => tvChannel.Id == tvChannelId);
                tvChannelPackageToRemoveChannel.Channels.Remove(tvChannelToRemove);
                context.Entry<TVChannelPackage>(tvChannelPackageToRemoveChannel).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public IEnumerable<TVChannel> GetAllChannelsExceptChannelsInPackage(Guid tvChannelPackageId)
        {
            IEnumerable<TVChannel> allTVChannels = context.TVChannels.ToArray();
            IEnumerable<TVChannel> tvChannelsFromPackage = context.TVChannelPackages.First(tvChannelPackage => tvChannelPackage.Id == tvChannelPackageId).Channels.ToArray();
            return allTVChannels.Except(tvChannelsFromPackage);
        }
    }
}