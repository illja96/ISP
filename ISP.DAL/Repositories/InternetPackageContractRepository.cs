﻿using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class InternetPackageContractRepository : RepositoryBase<InternetPackageContract>
    {
        public InternetPackageContractRepository() : base() { }
        public InternetPackageContractRepository(ISPContext context) : base() { }
    }
}