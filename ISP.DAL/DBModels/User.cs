using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.DBModels
{
    public class User : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Номер телефона")]
        public string MobileTelephone { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Баланс")]
        public double Balance { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime DoB { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Операции с балансом")]
        public virtual ICollection<UserBalanceLog> BalanceLog { get; set; }

        [Display(Name = "Адреса обслуживания")]
        public virtual ICollection<ContractAddress> ContractAddresses { get; set; }

        [Display(Name = "Контракты на каналы")]
        public virtual ICollection<TVChannelContract> TVChannelContracts { get; set; }

        [Display(Name = "Контракты на пакеты каналов")]
        public virtual ICollection<TVChannelPackageContract> TVChannelPackageContracts { get; set; }

        [Display(Name = "Контракты на пакеты интернет услуг")]
        public virtual ICollection<InternetPackageContract> InternetPackageContracts { get; set; }
    }
}