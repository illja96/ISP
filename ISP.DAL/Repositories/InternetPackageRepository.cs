using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class InternetPackageRepository : RepositoryBase<InternetPackage>
    {
        public InternetPackageRepository() : base() { }
        public InternetPackageRepository(ISPContext context) : base(context) { }
    }
}