using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class TVChannelContractRepository : RepositoryBase<TVChannelContract>
    {
        public TVChannelContractRepository() : base() { }
        public TVChannelContractRepository(ISPContext context) : base(context) { }
    }
}