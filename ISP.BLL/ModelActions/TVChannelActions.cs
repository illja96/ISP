﻿using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ISP.DAL.Repositories;
using ISP.DAL;

namespace ISP.BLL.ModelActions
{
    public class TVChannelActions : ModelActionBase<TVChannel>
    {
        public TVChannelActions()
        {
            context = new ISPContext();
            repository = new TVChannelRepository(context);
        }

        /// <summary>
        /// Create new TVChannel. Set IsCanceled if IsIPTV and IsTV is false
        /// </summary>
        public override void Create(TVChannel item)
        {
            if(!item.IsIPTV && !item.IsTV)
                item.IsCanceled = true;
            base.Create(item);
        }
        /// <summary>
        /// Edit TVChannel. Set IsCanceled if IsIPTV and IsTV is false
        /// </summary>
        public override void Edit(TVChannel item)
        {
            if (!item.IsIPTV && !item.IsTV)
                item.IsCanceled = true;
            base.Edit(item);
        }

        public override void Cancel(Guid id)
        {
            TVChannel item = Get(id);
            Cancel(item);
        }
        public override void Cancel(TVChannel item)
        {
            item.IsCanceled = true;
            Edit(item);
        }
        /// <summary>
        /// Renew TVChannel. If IsIPTV and IsTV is false set its to true
        /// </summary>
        public override void Renew(Guid id)
        {
            TVChannel item = Get(id);
            Renew(item);
        }
        /// <summary>
        /// Renew TVChannel. If IsIPTV and IsTV is false set its to true
        /// </summary>
        public override void Renew(TVChannel item)
        {
            if (!item.IsIPTV && !item.IsTV)
            {
                item.IsIPTV = true;
                item.IsTV = true;
            }
            item.IsCanceled = false;
            Edit(item);
        }

        public override TVChannel GetNotCanceled(Guid id)
        {
            return repository.Get(item => item.Id == id && !item.IsCanceled);
        }
        public override IEnumerable<TVChannel> GetAllNotCanceled()
        {
            return repository.GetAll(item => !item.IsCanceled);
        }
        public override IEnumerable<TVChannel> GetAllNotCanceledOrderBy(Expression<Func<TVChannel, object>> keySelector)
        {
            return repository.GetAllOrderBy(item => !item.IsCanceled, keySelector);
        }
        public override IEnumerable<TVChannel> GetAllNotCanceledOrderByDescending(Expression<Func<TVChannel, object>> keySelector)
        {
            return repository.GetAllOrderByDescending(item => !item.IsCanceled, keySelector);
        }

        public override void GetAvailableSortList(out Dictionary<string, string> sortBy, out Dictionary<string, bool> orderByDescending)
        {
            sortBy = new Dictionary<string, string>()
            {
                { "По умолчанию", "Id" },
                { "По названию", "Name" },
                { "По наличию TV", "IsTV" },
                { "По наличию IPTV", "IsIPTV" },
                { "По цене", "Price" },
                { "По состоянию", "IsCanceled" }
            };

            orderByDescending = new Dictionary<string, bool>()
            {
                { "По возрастанию", false },
                { "По убыванию", true }
            };
        }
        public override IEnumerable<TVChannel> Sort(IEnumerable<TVChannel> items, string sortBy, bool orderByDescending)
        {
            switch (sortBy)
            {
                case "Name":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.Name);
                    else
                        return items.OrderBy(item => item.Name);

                case "Price":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.Price);
                    else
                        return items.OrderBy(item => item.Price);

                case "IsTV":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.IsTV);
                    else
                        return items.OrderBy(item => item.IsTV);

                case "IsIPTV":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.IsIPTV);
                    else
                        return items.OrderBy(item => item.IsIPTV);

                case "IsCanceled":
                    if (orderByDescending)
                        return items.OrderByDescending(item => item.IsCanceled);
                    else
                        return items.OrderBy(item => item.IsCanceled);

                default:
                    return items;
            }
        }
    }
}