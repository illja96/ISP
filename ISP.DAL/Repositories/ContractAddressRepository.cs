﻿using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class ContractAddressRepository : RepositoryBase<ContractAddress>
    {
        public ContractAddressRepository() : base() { }
        public ContractAddressRepository(ISPContext context) : base(context) { }
    }
}