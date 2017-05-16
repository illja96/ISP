using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.ViewModels
{
    public class UserAdminEdit
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression("[\\+]\\d{12}", ErrorMessage = "Требуется поле Телефон в формате: +380000000000")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DoB { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        public UserAdminEdit()
        {

        }

        public UserAdminEdit(User user, string role)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            DoB = user.DoB.Date;
            Role = role;
        }

        public void Split(ref User user, out string role)
        {
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.MiddleName = this.MiddleName;
            user.Email = this.Email;
            user.PhoneNumber = this.PhoneNumber;
            user.DoB = this.DoB;
            role = Role;
        }
    }
}