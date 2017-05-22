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
    public class TVChannelContractActions : ModelActionBase<TVChannelContract>
    {
        private RepositoryBase<ContractAddress> contractAddressRepository;

        public TVChannelContractActions()
        {
            context = new ISPContext();
            repository = new TVChannelContractRepository(context);
            contractAddressRepository = new ContractAddressRepository(context);
        }

        /// <summary>
        /// Create new TVChannelContract. Throw Exception if exist TVChannelPackageContract with that channel
        /// </summary>
        public override void Create(TVChannelContract item)
        {
            ContractAddress contractAddress = contractAddressRepository.Get(item.ContractAddressId);
            IEnumerable<TVChannelPackageContract> tvChannelPackageContracts = contractAddress.TVChannelPackageContracts.ToArray();
            foreach(TVChannelPackageContract tvChannelPackageContract in tvChannelPackageContracts)
            {
                if(tvChannelPackageContract.TVChannelPackage.Channels.Count(tvChannel => tvChannel.Id == item.TVChannelId) != 0)
                {
                    throw new Exception();
                }
            }

            base.Create(item);
        }
        public override void Edit(TVChannelContract item)
        {
            throw new NotImplementedException();
        }

        public override void Cancel(Guid id)
        {
            TVChannelContract item = Get(id);
            Cancel(item);
        }
        public override void Cancel(TVChannelContract item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Renew(TVChannelContract item)
        {
            throw new NotImplementedException();
        }

        public override TVChannelContract GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<TVChannelContract> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<TVChannelContract> GetAllNotCanceledOrderBy(Expression<Func<TVChannelContract, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<TVChannelContract> GetAllNotCanceledOrderByDescending(Expression<Func<TVChannelContract, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            throw new NotImplementedException();
        }     
        public override IEnumerable<TVChannelContract> Sort(IEnumerable<TVChannelContract> items, string sortBy, bool orderByDescending)
        {
            throw new NotImplementedException();
        }
    }
}