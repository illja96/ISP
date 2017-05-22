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
    public class InternetPackageContractActions : ModelActionBase<InternetPackageContract>
    {
        private RepositoryBase<ContractAddress> contractAddressRepository;

        public InternetPackageContractActions()
        {
            context = new ISPContext();
            repository = new InternetPackageContractRepository(context);
            contractAddressRepository = new ContractAddressRepository(context);
        }

        /// <summary>
        /// Create new InternetPackageContract. Cancel all others InternetPackageContract
        /// </summary>
        public override void Create(InternetPackageContract item)
        {
            ContractAddress contractAddress = contractAddressRepository.Get(item.ContractAddressId);
            IEnumerable<InternetPackageContract> intenetPackageContracts = contractAddress.InternetPackageContracts.Where(internetContract => !internetContract.IsCanceled);
            foreach(InternetPackageContract internetPackageContract in intenetPackageContracts)
            {
                Cancel(internetPackageContract);
            }
            base.Create(item);
        }
        public override void Edit(InternetPackageContract item)
        {
            throw new NotImplementedException();
        }

        public override void Cancel(Guid id)
        {
            InternetPackageContract item = Get(id);
            Cancel(item);
        }
        public override void Cancel(InternetPackageContract item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Renew(InternetPackageContract item)
        {
            throw new NotImplementedException();
        }

        public override InternetPackageContract GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<InternetPackageContract> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<InternetPackageContract> GetAllNotCanceledOrderBy(Expression<Func<InternetPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<InternetPackageContract> GetAllNotCanceledOrderByDescending(Expression<Func<InternetPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<InternetPackageContract> Sort(IEnumerable<InternetPackageContract> items, string sortBy, bool orderByDescending)
        {
            throw new NotImplementedException();
        }
    }
}