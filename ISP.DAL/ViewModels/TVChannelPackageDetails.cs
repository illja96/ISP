using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DAL.ViewModels
{
    public class TVChannelPackageDetails
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

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

        [Display(Name = "Количество каналов")]
        public int ChannelsCount
        {
            get { return Channels.Count(); }
        }

        [Display(Name = "Каналы")]
        public IEnumerable<TVChannelDetails> Channels { get; set; }

        public TVChannelPackageDetails(TVChannelPackage items)
        {
            Id = items.Id;
            Name = items.Name;
            Price = items.Price;
            IsCanceled = items.IsCanceled;

            Contracts = items.Contracts.Count(tvChannelPackageContract => tvChannelPackageContract.IsCanceled == false);
            ContractsCanceled = items.Contracts.Count(tvChannelPackageContract => tvChannelPackageContract.IsCanceled == true);
            ContractsTotal = Contracts + ContractsCanceled;

            Channels = items.Channels.Select(tvChannel => new TVChannelDetails(tvChannel)).ToArray();
        }
    }
}