using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.ViewModels
{
    public class TVChannelDetails
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "TV")]
        public bool IsTV { get; set; }

        [Display(Name = "IPTV")]
        public bool IsIPTV { get; set; }

        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Аннулирован?")]
        public bool IsCanceled { get; set; }

        [Display(Name = "Количество контрактов в розницу")]
        public int Contracts { get; set; }

        [Display(Name = "Количество аннулированных контрактов в розницу")]
        public int ContractsCanceled { get; set; }

        [Display(Name = "Общее количество контрактов в розницу")]
        public int ContractsTotal { get; set; }

        [Display(Name = "Входит в пакеты")]
        public string[] Packages { get; set; }

        [Display(Name = "Количество контрактов в пакетах")]
        public int ContractsInPackage { get; set; }

        [Display(Name = "Количество аннулированных контрактов в пакетах")]
        public int ContractsCanceledInPackage { get; set; }

        [Display(Name = "Общее количество контрактов в пакетах")]
        public int ContractsTotalInPackage { get; set; }

        public TVChannelDetails(TVChannel tvChannel)
        {
            Id = tvChannel.Id;
            Name = tvChannel.Name;
            IsTV = tvChannel.IsTV;
            IsIPTV = tvChannel.IsIPTV;
            Price = tvChannel.Price;
            IsCanceled = tvChannel.IsCanceled;

            Contracts = tvChannel.Contracts.Where(tvChannelContract => tvChannelContract.IsCanceled == false).Count();
            ContractsCanceled = tvChannel.Contracts.Where(tvChannelContract => tvChannelContract.IsCanceled == true).Count();
            ContractsTotal = Contracts + ContractsTotal;

            Packages = tvChannel.Packages.Select(tvChannelPackage => tvChannelPackage.Name).ToArray();

            ContractsInPackage = 0;
            ContractsCanceledInPackage = 0;
            foreach (var tvChannelPackage in tvChannel.Packages)
            {
                ContractsInPackage += tvChannelPackage.Contracts.Count(tvChannelPackageContract => tvChannelPackageContract.IsCanceled == false);
                ContractsCanceledInPackage += tvChannelPackage.Contracts.Count(tvChannelPackageContract => tvChannelPackageContract.IsCanceled == true);
            }
            ContractsTotalInPackage = ContractsInPackage + ContractsCanceledInPackage;
        }
    }
}