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
    public class TVChannelContractActions : ModelActionBase<TVChannelContract>
    {
        private RepositoryBase<User> userRepository;
        private RepositoryBase<ContractAddress> contractAddressRepository;
        private RepositoryBase<TVChannel> tvChannelRepository;

        public TVChannelContractActions()
        {
            context = new ISPContext();
            repository = new TVChannelContractRepository(context);
            userRepository = new UserRepository(context);
            contractAddressRepository = new ContractAddressRepository(context);
            tvChannelRepository = new TVChannelRepository(context);
        }

        /// <summary>
        /// Create new TVChannelContract and user change balance
        /// </summary>
        public void Create(string userId, Guid contractAddressId, Guid tvChannelId)
        {
            User user = (userRepository as UserRepository).GetById(userId);
            ContractAddress contractAddress = contractAddressRepository.Get(contractAddressId);
            TVChannel tvChannel = tvChannelRepository.Get(tvChannelId);

            TVChannelContract tvChannelContract = new TVChannelContract()
            {
                Number = repository.Count().ToString(),
                DoS = DateTime.UtcNow,
                SubscriberId = user.Id,
                ContractAddressId = contractAddress.Id,
                TVChannelId = tvChannel.Id,
            };
            Create(tvChannelContract);
        }
        /// <summary>
        /// Create new TVChannelContract and user change balance
        /// </summary>
        public override void Create(TVChannelContract item)
        {
            TVChannel tvChannel = tvChannelRepository.Get(item.TVChannelId);

            double monthPrice = CalculatePrice(tvChannel.Price);
            double price = CalculatePrice(monthPrice);
            User user = (userRepository as UserRepository).GetById(item.SubscriberId);

            if (user.Balance < price)
            {
                throw new Exception();
            }

            base.Create(item);

            user.Balance -= price;
            userRepository.Edit(user);
        }

        public override void Cancel(Guid id)
        {
            TVChannelContract item = Get(id);
            Cancel(item);
        }
        public override void Cancel(TVChannelContract item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Renew(TVChannelContract item)
        {
            throw new NotImplementedException();
        }

        public override TVChannelContract GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<TVChannelContract> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<TVChannelContract> GetAllNotCanceledOrderBy(Expression<Func<TVChannelContract, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<TVChannelContract> GetAllNotCanceledOrderByDescending(Expression<Func<TVChannelContract, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<TVChannelContract> Sort(IEnumerable<TVChannelContract> items, string sortBy, bool orderByDescending)
        {
            throw new NotImplementedException();
        }

        public bool CanSubscribe(Guid contractAddressId, Guid tvChannelId)
        {
            ContractAddress contractAddress = contractAddressRepository.Get(contractAddressId);
            IEnumerable<TVChannelPackageContract> tvChannelPackageContracts = contractAddress.TVChannelPackageContracts.Where(item => !item.IsCanceled).ToArray();
            foreach (TVChannelPackageContract tvChannelPackageContract in tvChannelPackageContracts)
            {
                if (tvChannelPackageContract.TVChannelPackage.Channels.Count(item => item.Id == tvChannelId) != 0)
                {
                    return false;
                }
            }

            if (contractAddress.TVChannelContracts.Count(item => item.TVChannelId == tvChannelId && !item.IsCanceled) != 0)
            {
                return false;
            }

            User user = (userRepository as UserRepository).GetById(contractAddress.SubscriberId);
            TVChannel tvChannel = tvChannelRepository.Get(tvChannelId);
            double price = CalculatePrice(tvChannel.Price);

            return user.Balance >= price;
        }
        private double CalculatePrice(double monthPrice)
        {
            int currentYear = DateTime.UtcNow.Year;
            int currentMonth = DateTime.UtcNow.Month;
            int currentDay = DateTime.UtcNow.Day;

            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysLeftInMonth = daysInMonth - currentDay;

            double pricePerDay = monthPrice / daysInMonth;
            double price = pricePerDay * daysLeftInMonth;
            return price;
        }
    }
}