using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ISP.DAL.Repositories
{
    public class InternetPackageRepo : IRepository<InternetPackage>
    {
        private ISPContext context;

        public InternetPackageRepo() { context = new ISPContext(); }
        public InternetPackageRepo(ISPContext context) { this.context = context; }

        public void Create(InternetPackage item)
        {
            item.Id = Guid.NewGuid();
            context.InternetPackages.Add(item);
            context.SaveChanges();
        }
        public void Edit(InternetPackage item)
        {
            context.Entry<InternetPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Cancel(Guid id)
        {
            InternetPackage item = Get(id);
            item.IsCanceled = true;
            context.Entry<InternetPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Cancel(InternetPackage item)
        {
            item.IsCanceled = true;
            context.Entry<InternetPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Renew(Guid id)
        {
            InternetPackage item = Get(id);
            item.IsCanceled = false;
            context.Entry<InternetPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void Renew(InternetPackage item)
        {
            item.IsCanceled = false;
            context.Entry<InternetPackage>(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public InternetPackage GetNotCanceled(Guid id)
        {
            return context.InternetPackages.First(internetPackage => internetPackage.Id == id && internetPackage.IsCanceled == false);
        }
        public IEnumerable<InternetPackage> GetAllNotCanceled()
        {
            return context.InternetPackages.Where(internetPackage => internetPackage.IsCanceled == false);
        }
        public IEnumerable<InternetPackage> GetAllNotCanceledOrderBy(Expression<Func<InternetPackage, object>> keySelector)
        {
            return context.InternetPackages.Where(internetPackage => internetPackage.IsCanceled == false).OrderBy(keySelector);
        }
        public IEnumerable<InternetPackage> GetAllNotCanceledOrderByDescending(Expression<Func<InternetPackage, object>> keySelector)
        {
            return context.InternetPackages.Where(internetPackage => internetPackage.IsCanceled == false).OrderByDescending(keySelector);
        }

        public InternetPackage Get(Guid id)
        {
            return context.InternetPackages.First(internetPackage => internetPackage.Id == id);
        }
        public IEnumerable<InternetPackage> GetAll()
        {
            return context.InternetPackages;
        }
        public IEnumerable<InternetPackage> GetAllOrderBy(Expression<Func<InternetPackage, object>> keySelector)
        {
            return context.InternetPackages.OrderBy(keySelector);
        }
        public IEnumerable<InternetPackage> GetAllOrderByDescending(Expression<Func<InternetPackage, object>> keySelector)
        {
            return context.InternetPackages.OrderByDescending(keySelector);
        }

        public IEnumerable<InternetPackage> Sort(IEnumerable<InternetPackage> items, string sortBy, bool orderByDescending)
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
        public void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
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

        public void Dispose()
        {
            context.Dispose();
        }
    }
}