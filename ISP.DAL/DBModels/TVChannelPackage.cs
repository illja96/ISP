using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.DBModels
{
    public class TVChannelPackage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Стоимость в месяц")]
        public double Price { get; set; }

        [Display(Name = "Каналы")]
        public virtual ICollection<TVChannel> Channels { get; set; }

        [Display(Name = "Контракты")]
        public virtual ICollection<TVChannelPackageContract> Contracts { get; set; }
    }
}