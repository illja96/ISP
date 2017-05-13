using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.ViewModels
{
    public class InternetPackageDetails
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Скорость приёма (МБ/с)")]
        public double DownloadSpeed { get; set; }

        [Display(Name = "Скорость отдачи (МБ/с)")]
        public double UploadSpeed { get; set; }

        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Аннулирован?")]
        public bool IsCanceled { get; set; }

        [Display(Name = "Количество контрактов")]
        public int Contracts { get; set; }

        [Display(Name = "Количество аннулированных контрактов")]
        public int ContractsCanceled { get; set; }

        [Display(Name = "Общее количество контрактов")]
        public int ContractsTotal { get; set; }

        public InternetPackageDetails(InternetPackage internetPackage)
        {
            Id = internetPackage.Id;
            Name = internetPackage.Name;
            DownloadSpeed = internetPackage.DownloadSpeed;
            UploadSpeed = internetPackage.UploadSpeed;
            Price = internetPackage.Price;
            IsCanceled = internetPackage.IsCanceled;

            Contracts = internetPackage.Contracts.Where(internetPackageContract => internetPackageContract.IsCanceled == false).Count();
            ContractsCanceled = internetPackage.Contracts.Where(internetPackageContract => internetPackageContract.IsCanceled == true).Count();
            ContractsTotal = Contracts + ContractsTotal;
        }
    }
}