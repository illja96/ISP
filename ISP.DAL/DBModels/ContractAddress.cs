using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.DBModels
{
    public class ContractAddress
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Subscriber")]
        public string SubscriberId { get; set; }

        [Required]
        [Display(Name = "Область")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Населенный пункт")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Номер дома")]
        public string House { get; set; }

        [Display(Name = "Номер квартиры")]
        public string Apartment { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Почтовый индекс")]
        public int ZIP { get; set; }

        [Display(Name = "Абоненты")]
        public virtual User Subscriber { get; set; }

        [Display(Name = "Контракты на каналы")]
        public virtual ICollection<TVChannelContract> TVChannelContracts { get; set; }

        [Display(Name = "Контракты на пакеты каналов")]
        public virtual ICollection<TVChannelPackageContract> TVChannelPackageContracts { get; set; }

        [Display(Name = "Контракты на пакеты интернет услуг")]
        public virtual ICollection<InternetPackageContract> InternetPackageContracts { get; set; }
    }
}