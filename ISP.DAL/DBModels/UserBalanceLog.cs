using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.DBModels
{
    public class UserBalanceLog
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Subscriber")]
        public string SubscriberId { get; set; }

        [Required]
        [Display(Name = "Сумма")]
        public double MoneyAmount { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Абонент")]
        public virtual User Subscriber { get; set; }

        public UserBalanceLog() { this.Id = Guid.NewGuid(); }
    }
}