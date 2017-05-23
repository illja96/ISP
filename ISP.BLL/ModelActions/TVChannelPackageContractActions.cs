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
    public class TVChannelPackageContractActions : ModelActionBase<TVChannelPackageContract>
    {
        private RepositoryBase<User> userRepository;
        private RepositoryBase<ContractAddress> contractAddressRepository;
        private RepositoryBase<TVChannel> tvChannelRepository;
        private RepositoryBase<TVChannelContract> tvChannelContractRepository;
        private RepositoryBase<TVChannelPackage> tvChannelPackageRepository;

        public TVChannelPackageContractActions()
        {
            context = new ISPContext();
            userRepository = new UserRepository(context);
            contractAddressRepository = new ContractAddressRepository(context);
            tvChannelRepository = new TVChannelRepository(context);
            tvChannelContractRepository = new TVChannelContractRepository(context);
            tvChannelPackageRepository = new TVChannelPackageRepository(context);
            repository = new TVChannelPackageContractRepository(context);
        }

        /// <summary>
        /// Create new TVChannelPackageContract, cancel all TVChannelContracts constains in that package and change user balance
        /// </summary>
        public void Create(string userId, Guid contractAddressId, Guid tvChannelPackageId)
        {
            User user = (userRepository as UserRepository).GetById(userId);
            ContractAddress contractAddress = contractAddressRepository.Get(contractAddressId);
            TVChannelPackage tvChannelPackage = tvChannelPackageRepository.Get(tvChannelPackageId);

            TVChannelPackageContract tvChannelPackageContract = new TVChannelPackageContract()
            {
                Number = repository.Count().ToString(),
                DoS = DateTime.UtcNow,
                SubscriberId = user.Id,
                ContractAddressId = contractAddress.Id,
                TVChannelPackageId = tvChannelPackage.Id
            };
            Create(tvChannelPackageContract);
        }
        /// <summary>
        /// Create new TVChannelPackageContract, cancel all TVChannelContracts constains in that package and change user balance
        /// </summary>
        public override void Create(TVChannelPackageContract item)
        {
            TVChannelPackage tvChannelPackage = tvChannelPackageRepository.Get(item.TVChannelPackageId);

            double monthPrice = CalculatePrice(tvChannelPackage.Price);
            double price = CalculatePrice(monthPrice);
            User user = (userRepository as UserRepository).GetById(item.SubscriberId);

            if (user.Balance < price)
            {
                throw new Exception();
            }

            ContractAddress contractAddress = contractAddressRepository.Get(item.ContractAddressId);
            IEnumerable<TVChannel> tvChannelsInPackage = tvChannelPackageRepository.Get(item.TVChannelPackageId).Channels.ToArray();
            IEnumerable<TVChannelContract> tvChannelContracts = contractAddress.TVChannelContracts.Where(tvChannel => !tvChannel.IsCanceled).ToArray();
            foreach (TVChannelContract tvChannelContract in tvChannelContracts)
            {
                if (tvChannelsInPackage.Count(tvChannel => tvChannel.Id == tvChannelContract.TVChannelId) != 0)
                {
                    TVChannelContract tvChannelContractToCancel = tvChannelContractRepository.Get(tvChannelContract.Id);
                    tvChannelContractToCancel.IsCanceled = true;
                    tvChannelContractRepository.Edit(tvChannelContractToCancel);
                }
            }

            base.Create(item);

            user.Balance -= price;
            userRepository.Edit(user);
        }

        public override void Cancel(Guid id)
        {
            TVChannelPackageContract item = Get(id);
            Cancel(item);
        }
        public override void Cancel(TVChannelPackageContract item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Renew(TVChannelPackageContract item)
        {
            throw new NotImplementedException();
        }

        public override TVChannelPackageContract GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<TVChannelPackageContract> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<TVChannelPackageContract> GetAllNotCanceledOrderBy(Expression<Func<TVChannelPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<TVChannelPackageContract> GetAllNotCanceledOrderByDescending(Expression<Func<TVChannelPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<TVChannelPackageContract> Sort(IEnumerable<TVChannelPackageContract> items, string sortBy, bool orderByDescending)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check can user subscribe to this tariff plan
        /// </summary>
        public bool CanSubscribe(Guid contractAddressId, Guid tvChannelPackageId)
        {
            ContractAddress contractAddress = contractAddressRepository.Get(contractAddressId);

            if (contractAddress.TVChannelPackageContracts.Count(item => item.TVChannelPackageId == tvChannelPackageId && !item.IsCanceled) != 0)
            {
                return false;
            }

            User user = (userRepository as UserRepository).GetById(contractAddress.SubscriberId);
            TVChannelPackage tvChannelPackage = tvChannelPackageRepository.Get(tvChannelPackageId);
            double price = CalculatePrice(tvChannelPackage.Price);

            return user.Balance >= price;
        }
        /// <summary>
        /// Calculate tariff plan price to current time moment
        /// </summary>
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