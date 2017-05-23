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
    public class InternetPackageContractActions : ModelActionBase<InternetPackageContract>
    {
        private RepositoryBase<User> userRepository;
        private RepositoryBase<ContractAddress> contractAddressRepository;
        private RepositoryBase<InternetPackage> internetPackageRepository;

        public InternetPackageContractActions()
        {
            context = new ISPContext();
            repository = new InternetPackageContractRepository(context);
            userRepository = new UserRepository(context);
            contractAddressRepository = new ContractAddressRepository(context);
            internetPackageRepository = new InternetPackageRepository(context);
        }

        /// <summary>
        /// Create new InternetPackageContract. Cancel all others InternetPackageContract
        /// </summary>
        public void Create(string userId, Guid contractAddressId, Guid internetPackageId)
        {
            User user = (userRepository as UserRepository).GetById(userId);
            ContractAddress contractAddress = contractAddressRepository.Get(contractAddressId);
            InternetPackage internetPackage = internetPackageRepository.Get(internetPackageId);

            InternetPackageContract internetPackageContract = new InternetPackageContract()
            {
                Number = repository.Count().ToString(),
                DoS = DateTime.UtcNow,
                SubscriberId = user.Id,
                ContractAddressId = contractAddress.Id,
                InternetPackageId = internetPackage.Id,
            };
            Create(internetPackageContract);
        }
        /// <summary>
        /// Create new InternetPackageContract. Cancel all others InternetPackageContract
        /// </summary>
        public override void Create(InternetPackageContract item)
        {
            InternetPackage intrenetPackage = internetPackageRepository.Get(item.InternetPackageId);

            double monthPrice = CalculatePrice(intrenetPackage.Price);
            double price = CalculatePrice(monthPrice);
            User user = (userRepository as UserRepository).GetById(item.SubscriberId);

            if (user.Balance < price)
            {
                throw new Exception();
            }

            ContractAddress contractAddress = contractAddressRepository.Get(item.ContractAddressId);
            IEnumerable<InternetPackageContract> intenetPackageContracts = contractAddress.InternetPackageContracts.Where(internetContract => !internetContract.IsCanceled);
            foreach (InternetPackageContract internetPackageContract in intenetPackageContracts)
            {
                Cancel(internetPackageContract.Id);
            }
            base.Create(item);

            user.Balance -= price;
            userRepository.Edit(user);
        }

        public override void Cancel(Guid id)
        {
            InternetPackageContract item = Get(id);
            Cancel(item);
        }
        public override void Cancel(InternetPackageContract item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            throw new NotImplementedException();
        }
        public override void Renew(InternetPackageContract item)
        {
            throw new NotImplementedException();
        }

        public override InternetPackageContract GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<InternetPackageContract> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<InternetPackageContract> GetAllNotCanceledOrderBy(Expression<Func<InternetPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<InternetPackageContract> GetAllNotCanceledOrderByDescending(Expression<Func<InternetPackageContract, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<InternetPackageContract> Sort(IEnumerable<InternetPackageContract> items, string sortBy, bool orderByDescending)
        {
            throw new NotImplementedException();
        }

        public bool CanSubscribe(Guid contractAddressId, Guid internetPackageId)
        {
            ContractAddress contractAddress = contractAddressRepository.Get(contractAddressId);

            if(contractAddress.InternetPackageContracts.Count(item => item.InternetPackageId == internetPackageId && !item.IsCanceled) != 0)
            {
                return false;
            }

            User user = (userRepository as UserRepository).GetById(contractAddress.SubscriberId);
            InternetPackage internetPackage = internetPackageRepository.Get(internetPackageId);
            double price = CalculatePrice(internetPackage.Price);
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