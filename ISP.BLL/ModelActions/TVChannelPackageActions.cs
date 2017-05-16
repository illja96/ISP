using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ISP.DAL.Repositories;

namespace ISP.BLL.ModelActions
{
    public class TVChannelPackageActions : ModelActionBase<TVChannelPackage>
    {
        private RepositoryBase<TVChannel> tvChannelRepository;

        public TVChannelPackageActions()
        {
            repository = new TVChannelPackageRepository();
            tvChannelRepository = new TVChannelRepository();
        }

        public override void Cancel(Guid id)
        {
            TVChannelPackage item = Get(id);
            Cancel(item);
        }
        public override void Cancel(TVChannelPackage item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            TVChannelPackage item = Get(id);
            Renew(item);
        }
        public override void Renew(TVChannelPackage item)
        {
            item.IsCanceled = false;
            Edit(item);
        }

        public override TVChannelPackage GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<TVChannelPackage> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<TVChannelPackage> GetAllNotCanceledOrderBy(Expression<Func<TVChannelPackage, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<TVChannelPackage> GetAllNotCanceledOrderByDescending(Expression<Func<TVChannelPackage, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
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
        public override IEnumerable<TVChannelPackage> Sort(IEnumerable<TVChannelPackage> items, string sortBy, bool orderByDescending)
        {
            switch (sortBy)
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

        public void AddChannelToPackage(Guid tvChannelPackageId, Guid tvChannelId)
        {
            TVChannelPackage tvChannelPackage = Get(tvChannelPackageId);
            if (tvChannelPackage.Channels.Count(item => item.Id == tvChannelId) == 0)
            {
                TVChannel tvChannel = tvChannelRepository.Get(tvChannelId);
                tvChannelPackage.Channels.Add(tvChannel);
                Edit(tvChannelPackage);
            }
        }
        public void RemoveChannelFromPackage(Guid tvChannelPackageId, Guid tvChannelId)
        {
            TVChannelPackage tvChannelPackage = Get(tvChannelPackageId);
            if (tvChannelPackage.Channels.Count(item => item.Id == tvChannelId) != 0)
            {
                TVChannel tvChannel = tvChannelRepository.Get(tvChannelId);
                tvChannelPackage.Channels.Remove(tvChannel);
                Edit(tvChannelPackage);
            }
        }
        public IEnumerable<TVChannel> GetAllChannelsExceptChannelsInPackage(Guid tvChannelPackageId)
        {
            IEnumerable<TVChannel> allTVChannels = tvChannelRepository.GetAll().ToArray();
            IEnumerable<TVChannel> tvChannelsFromPackage = Get(tvChannelPackageId).Channels.ToArray();
            return allTVChannels.Except(tvChannelsFromPackage);
        }
    }
}