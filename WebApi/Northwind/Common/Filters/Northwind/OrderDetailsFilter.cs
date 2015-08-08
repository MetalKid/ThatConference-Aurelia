#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Order Details] table.
    /// </summary>
    public class OrderDetailsFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [OrderID] column.
        /// </summary>
        [MapToProperty("OrderID")]
        public FilterRange<int> OrderID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ProductID] column.
        /// </summary>
        [MapToProperty("ProductID")]
        public FilterRange<int> ProductID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [UnitPrice] column.
        /// </summary>
        [MapToProperty("UnitPrice")]
        public FilterRange<decimal> UnitPrice { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Quantity] column.
        /// </summary>
        [MapToProperty("Quantity")]
        public FilterRange<short> Quantity { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Discount] column.
        /// </summary>
        [MapToProperty("Discount")]
        public FilterRange<Single> Discount { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Orders] table via the [OrderID] column.
        /// </summary>
        public Boolean IncludeOrders { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Products] table via the [ProductID] column.
        /// </summary>
        public Boolean IncludeProducts { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public OrderDetailsFilter()
        {
            OrderID = new FilterRange<int>();
            ProductID = new FilterRange<int>();
            UnitPrice = new FilterRange<decimal>();
            Quantity = new FilterRange<short>();
            Discount = new FilterRange<Single>();
        }

        #endregion

    }
}