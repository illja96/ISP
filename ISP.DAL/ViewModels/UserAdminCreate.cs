using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.ViewModels
{
    public class UserAdminCreate
    {
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
        [Display(Name = "Новый пароль")]
        public string Password { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Почтовый индекс")]
        public int ZIP { get; set; }

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
        
        public void Split(out User user, out string password, out ContractAddress address)
        {
            user = new User()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                MiddleName = this.MiddleName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                DoB = this.DoB
            };

            password = Password;

            address = new ContractAddress()
            {
                ZIP = this.ZIP,
                Department = this.Department,
                City = this.City,
                Street = this.Street,
                House = this.House,
                Apartment = this.Apartment
            };
        }
    }
}