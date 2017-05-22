using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class TVChannelPackageContractRepository : RepositoryBase<TVChannelPackageContract>
    {
        public TVChannelPackageContractRepository() : base() { }
        public TVChannelPackageContractRepository(ISPContext context) : base(context) { }
    }
}