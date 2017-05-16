using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace ISP.DAL.ViewModels
{
    public class UserDetails
    {
        public string Id { get; set; }

        [Display(Name = "Номер")]
        public string UserName { get; set; }

        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Баланс")]
        public double Balance { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DoB { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Количество контрактов")]
        public int Contracts { get; set; }

        [Display(Name = "Количество аннулированных контрактов")]
        public int ContractsCanceled { get; set; }

        [Display(Name = "Общее количество контрактов")]
        public int ContractsTotal { get; set; }

        [Display(Name = "Операции с балансом")]
        public ICollection<UserBalanceLog> BalanceLog { get; set; }

        [Display(Name = "Адреса обслуживания")]
        public ICollection<ContractAddress> ContractAddresses { get; set; }

        public UserDetails(User user, string role)
        {
            Id = user.Id;
            UserName = user.UserName;
            Role = role;
            FullName = string.Format("{0} {1} {2}", user.LastName, user.FirstName, user.MiddleName);
            Balance = user.Balance;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            DoB = user.DoB;
            RegistrationDate = user.RegistrationDate;

            int TVChannelContracts = user.TVChannelContracts.Count(item => !item.IsCanceled);
            int TVChannelContractsCanceled = user.TVChannelContracts.Count(item => item.IsCanceled);
            int TVChannelContractsTotal = TVChannelContracts + TVChannelContractsCanceled;

            int TVChannelPackageContracts = user.TVChannelPackageContracts.Count(item => !item.IsCanceled);
            int TVChannelPackageContractsCanceled = user.TVChannelPackageContracts.Count(item => item.IsCanceled);
            int TVChannelPackageContractsTotal = TVChannelPackageContracts + TVChannelPackageContractsCanceled;

            int InternetPackageContract = user.InternetPackageContracts.Count(item => !item.IsCanceled);
            int InternetPackageContractCanceled = user.InternetPackageContracts.Count(item => item.IsCanceled);
            int InternetPackageContractTotal = InternetPackageContract + InternetPackageContractCanceled;

            Contracts = TVChannelContracts + TVChannelPackageContracts + InternetPackageContract;
            ContractsCanceled = TVChannelContractsCanceled + TVChannelPackageContractsCanceled + InternetPackageContractCanceled;
            ContractsTotal = TVChannelContractsTotal + TVChannelPackageContractsTotal + InternetPackageContractTotal;
        }
    }
}