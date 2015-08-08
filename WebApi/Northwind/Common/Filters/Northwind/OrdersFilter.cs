#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Orders] table.
    /// </summary>
    public class OrdersFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [OrderID] column.
        /// </summary>
        [MapToProperty("OrderID")]
        public FilterRange<int> OrderID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CustomerID] column.
        /// </summary>
        [MapToProperty("CustomerID")]
        public FilterString CustomerID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [EmployeeID] column.
        /// </summary>
        [MapToProperty("EmployeeID")]
        public FilterRange<int> EmployeeID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [OrderDate] column.
        /// </summary>
        [MapToProperty("OrderDate")]
        public FilterRange<DateTime> OrderDate { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [RequiredDate] column.
        /// </summary>
        [MapToProperty("RequiredDate")]
        public FilterRange<DateTime> RequiredDate { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShippedDate] column.
        /// </summary>
        [MapToProperty("ShippedDate")]
        public FilterRange<DateTime> ShippedDate { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipVia] column.
        /// </summary>
        [MapToProperty("ShipVia")]
        public FilterRange<int> ShipVia { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Freight] column.
        /// </summary>
        [MapToProperty("Freight")]
        public FilterRange<decimal> Freight { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipName] column.
        /// </summary>
        [MapToProperty("ShipName")]
        public FilterString ShipName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipAddress] column.
        /// </summary>
        [MapToProperty("ShipAddress")]
        public FilterString ShipAddress { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipCity] column.
        /// </summary>
        [MapToProperty("ShipCity")]
        public FilterString ShipCity { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipRegion] column.
        /// </summary>
        [MapToProperty("ShipRegion")]
        public FilterString ShipRegion { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipPostalCode] column.
        /// </summary>
        [MapToProperty("ShipPostalCode")]
        public FilterString ShipPostalCode { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipCountry] column.
        /// </summary>
        [MapToProperty("ShipCountry")]
        public FilterString ShipCountry { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Customers] table via the [CustomerID] column.
        /// </summary>
        public Boolean IncludeCustomers { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Employees] table via the [EmployeeID] column.
        /// </summary>
        public Boolean IncludeEmployees { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Shippers] table via the [ShipVia] column.
        /// </summary>
        public Boolean IncludeShippers { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Order Details] table via the [OrderID] column.
        /// </summary>
        public Boolean IncludeOrderDetailOrderDetailses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public OrdersFilter()
        {
            OrderID = new FilterRange<int>();
            CustomerID = new FilterString();
            EmployeeID = new FilterRange<int>();
            OrderDate = new FilterRange<DateTime>();
            RequiredDate = new FilterRange<DateTime>();
            ShippedDate = new FilterRange<DateTime>();
            ShipVia = new FilterRange<int>();
            Freight = new FilterRange<decimal>();
            ShipName = new FilterString();
            ShipAddress = new FilterString();
            ShipCity = new FilterString();
            ShipRegion = new FilterString();
            ShipPostalCode = new FilterString();
            ShipCountry = new FilterString();
        }

        #endregion

    }
}