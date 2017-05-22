using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class TVChannelPackageRepository : RepositoryBase<TVChannelPackage>
    {
        public TVChannelPackageRepository(): base() { }
        public TVChannelPackageRepository(ISPContext context) : base(context) { }
    }
}