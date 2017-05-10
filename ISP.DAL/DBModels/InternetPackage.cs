using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.DBModels
{
    public class InternetPackage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Скорость отдачи (МБ/с)")]
        public string UploadSpeed { get; set; }

        [Required]
        [Display(Name = "Скорость приёма (МБ/с)")]
        public string DownloadSpeed { get; set; }

        [Required]
        [DefaultValue(false)]
        [Display(Name = "Трафик лимитирован?")]
        public bool IsTrafficLimit { get; set; }

        [Required]
        [DefaultValue(0)]
        [Range(0, double.MaxValue)]
        [Display(Name = "Лимит трафика в МБ")]
        public double TrafficLimit { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Стоимость в месяц")]
        public double Price { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Аннулирован?")]
        public bool IsCanceled { get; set; }
    }
}