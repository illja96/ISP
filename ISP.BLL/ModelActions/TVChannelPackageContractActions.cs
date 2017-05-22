using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ISP.DAL.Repositories;
using ISP.DAL;

namespace ISP.BLL.ModelActions
{
    public class TVChannelPackageContractActions : ModelActionBase<TVChannelPackageContract>
    {
        private RepositoryBase<TVChannelContract> tvChannelContractRepository;
        private RepositoryBase<TVChannelPackage> tvChannelPackageRepository;
        private RepositoryBase<ContractAddress> contractAddressRepository;

        public TVChannelPackageContractActions()
        {
            context = new ISPContext();
            repository = new TVChannelPackageContractRepository(context);
            tvChannelContractRepository = new TVChannelContractRepository(context);
            tvChannelPackageRepository = new TVChannelPackageRepository(context);
            contractAddressRepository = new ContractAddressRepository(context);
        }

        /// <summary>
        /// Create new TVChannelPackageContract and cancel all TVChannelContracts constains in that package
        /// </summary>
        public override void Create(TVChannelPackageContract item)
        {
            ContractAddress contractAddress = contractAddressRepository.Get(item.ContractAddressId);
            IEnumerable<TVChannel> tvChannelsInPackage = tvChannelPackageRepository.Get(item.TVChannelPackageId).Channels.ToArray();
            IEnumerable<TVChannelContract> tvChannelContracts = contractAddress.TVChannelContracts.Where(tvChannel => !tvChannel.IsCanceled).ToArray();
            foreach(TVChannelContract tvChannelContract in tvChannelContracts)
            {
                if(tvChannelsInPackage.Count(tvChannel => tvChannel.Id == tvChannelContract.TVChannelId) != 0)
                {
                    tvChannelContract.IsCanceled = true;
                    tvChannelContractRepository.Edit(tvChannelContract);
                }
            }

            base.Create(item);
        }
        public override void Edit(TVChannelPackageContract item)
        {
            throw new NotImplementedException();
        }

        public override void Cancel(Guid id)
        {
            TVChannelPackageContract item = Get(id);
            Cancel(item);
        }
        public override void Cancel(TVChannelPackageContract item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Renew(TVChannelPackageContract item)
        {
            throw new NotImplementedException();
        }

        public override TVChannelPackageContract GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<TVChannelPackageContract> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<TVChannelPackageContract> GetAllNotCanceledOrderBy(Expression<Func<TVChannelPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<TVChannelPackageContract> GetAllNotCanceledOrderByDescending(Expression<Func<TVChannelPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<TVChannelPackageContract> Sort(IEnumerable<TVChannelPackageContract> items, string sortBy, bool orderByDescending)
        {
            throw new NotImplementedException();
        }
    }
}