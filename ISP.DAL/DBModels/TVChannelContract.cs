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
    public class TVChannelContract
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

        [Required]
        [ForeignKey("TVChannel")]
        public Guid ChannelId { get; set; }

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

        [Display(Name = "Канал")]
        public virtual TVChannel TVChannel { get; set; }

        public TVChannelContract() { this.Id = Guid.NewGuid(); }
    }
}