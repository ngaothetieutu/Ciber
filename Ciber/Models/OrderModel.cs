﻿
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ciber.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            List<SelectListItem> ProductSelectListItems = new List<SelectListItem>();
            List<SelectListItem> CustomerSelectListItems = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string OrderName { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        public string CustomerName { get; set; }
        public string CategoryName { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }
        public List<SelectListItem> CustomerSelectListItems { get; set; }

    }
    public class OrderSearchModel
    {
        ///<summary>
        /// Gets or sets Customers.
        ///</summary>
        public List<OrderModel> OrderModels { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }
    }
}
