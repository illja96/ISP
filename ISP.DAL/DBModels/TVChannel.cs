using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.DBModels
{
    public class TVChannel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Наличие IP-TV")]
        public bool IsIPTV { get; set; }

        [Required]
        [Display(Name = "Наличие TV")]
        public bool IsTV { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Стоимость в розницу")]
        public double Price { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Аннулирован?")]
        public bool IsCanceled { get; set; }

        [Display(Name = "Пакеты каналов")]
        public virtual ICollection<TVChannelPackage> Packages { get; set; }

        [Display(Name = "Контракты")]
        public virtual ICollection<TVChannelContract> Contracts { get; set; }
    }
}