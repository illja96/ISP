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
    public class ContractAddressActions : ModelActionBase<ContractAddress>
    {
        public ContractAddressActions()
        {
            ISPContext context = new ISPContext();
            repository = new ContractAddressRepository(context);
        }

        public override void Cancel(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Cancel(ContractAddress item)
        {
            throw new NotImplementedException();
        }
        public override void Renew(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Renew(ContractAddress item)
        {
            throw new NotImplementedException();
        }

        public override ContractAddress GetNotCanceled(Guid id)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<ContractAddress> GetAllNotCanceled()
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<ContractAddress> GetAllNotCanceledOrderBy(Expression<Func<ContractAddress, object>> keySelector)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<ContractAddress> GetAllNotCanceledOrderByDescending(Expression<Func<ContractAddress, object>> keySelector)
        {
            throw new NotImplementedException();
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<ContractAddress> Sort(IEnumerable<ContractAddress> items, string sortBy, bool orderByDescending)
        {
            throw new NotImplementedException();
        }
    }
}