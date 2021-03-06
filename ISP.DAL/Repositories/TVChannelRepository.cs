﻿using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class TVChannelRepository : RepositoryBase<TVChannel>
    {
        public TVChannelRepository() : base() { }
        public TVChannelRepository(ISPContext context) : base(context) { }
    }
}