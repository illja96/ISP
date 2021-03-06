﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.DBModels
{
    public class InternetPackageContract
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Номер")]
        public string Number { get; set; }

        [Required]
        [ForeignKey("Subscriber")]
        public string SubscriberId { get; set; }

        [Required]
        [ForeignKey("Address")]
        public Guid ContractAddressId { get; set; }

        [ForeignKey("InternetPackage")]
        public Guid InternetPackageId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата подписания")]
        public DateTime DoS { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Аннулирован?")]
        public bool IsCanceled { get; set; }

        [Display(Name = "Абонент")]
        public virtual User Subscriber { get; set; }

        [Display(Name = "Адрес")]
        public virtual ContractAddress Address { get; set; }

        [Display(Name = "Пакет интернет услуг")]
        public virtual InternetPackage InternetPackage { get; set; }

        public InternetPackageContract() { this.Id = Guid.NewGuid(); }
    }
}