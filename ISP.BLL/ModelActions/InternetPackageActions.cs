using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ISP.DAL.Repositories;

namespace ISP.BLL.ModelActions
{
    public class InternetPackageActions : ModelActionBase<InternetPackage>
    {
        public InternetPackageActions()
        {
            repository = new InternetPackageRepository();
        }

        public override void Cancel(Guid id)
        {
            InternetPackage item = Get(id);
            Cancel(item);
        }
        public override void Cancel(InternetPackage item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        public override void Renew(Guid id)
        {
            InternetPackage item = Get(id);
            Renew(item);
        }
        public override void Renew(InternetPackage item)
        {
            item.IsCanceled = false;
            Edit(item);
        }

        public override InternetPackage GetNotCanceled(Guid id)
        {
            return repository.context.Set<InternetPackage>().First(item => !item.IsCanceled);
        }
        public override IEnumerable<InternetPackage> GetAllNotCanceled()
        {
            return repository.context.Set<InternetPackage>().Where(item => !item.IsCanceled);
        }
        public override IEnumerable<InternetPackage> GetAllNotCanceledOrderBy(Expression<Func<InternetPackage, object>> keySelector)
        {
            return repository.context.Set<InternetPackage>().Where(item => !item.IsCanceled).OrderBy(keySelector);
        }
        public override IEnumerable<InternetPackage> GetAllNotCanceledOrderByDescending(Expression<Func<InternetPackage, object>> keySelector)
        {
            return repository.context.Set<InternetPackage>().Where(item => !item.IsCanceled).OrderByDescending(keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            sortBy = new Dictionary<string, string>()
            {
                { "По умолчанию", "Id" },
                { "По названию", "Name" },
                { "По скорости приёма", "DownloadSpeed" },
                { "По скорости отдачи", "UploadSpeed" },
                { "По цене", "Price" },
                { "По состоянию", "IsCanceled" }
            };

            orderByDescending = new Dictionary<string, bool>()
            {
                { "По возрастанию", false },
                { "По убыванию", true }
            };
        }
        public override IEnumerable<InternetPackage> Sort(IEnumerable<InternetPackage> items, string sortBy, bool orderByDescending)
        {
            switch (sortBy)
            {
                case "Name":
                    if (orderByDescending)
                        return items.OrderByDescending(internetPackage => internetPackage.Name);
                    else
                        return items.OrderBy(internetPackage => internetPackage.Name);

                case "DownloadSpeed":
                    if (orderByDescending)
                        return items.OrderByDescending(internetPackage => internetPackage.DownloadSpeed);
                    else
                        return items.OrderBy(internetPackage => internetPackage.DownloadSpeed);

                case "UploadSpeed":
                    if (orderByDescending)
                        return items.OrderByDescending(internetPackage => internetPackage.UploadSpeed);
                    else
                        return items.OrderBy(internetPackage => internetPackage.UploadSpeed);

                case "Price":
                    if (orderByDescending)
                        return items.OrderByDescending(internetPackage => internetPackage.Price);
                    else
                        return items.OrderBy(internetPackage => internetPackage.Price);

                case "IsCanceled":
                    if (orderByDescending)
                        return items.OrderByDescending(internetPackage => internetPackage.IsCanceled);
                    else
                        return items.OrderBy(internetPackage => internetPackage.IsCanceled);

                default:
                    return items;
            }
        }
    }
}