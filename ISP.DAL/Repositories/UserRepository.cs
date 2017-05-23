using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository() : base() { }
        public UserRepository(ISPContext context) : base(context) { }

        public User GetById(string userId)
        {
            return context.Set<User>().Find(userId);
        }
    }
}