#region << Usings >>

using System;
using System.Collections.Generic;
using ClassyMapper.Attributes;
using ThatConference.Common;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Common.DataTransferObjects.Northwind
{
    /// <summary>
    /// This class represents data coming from the [dbo].[Orders] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class OrdersDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [OrderID] column.
        /// </summary>
        public int OrderID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [CustomerID] column.
        /// </summary>
        public string CustomerID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [EmployeeID] column.
        /// </summary>
        public int? EmployeeID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [OrderDate] column.
        /// </summary>
        public DateTime? OrderDate { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [RequiredDate] column.
        /// </summary>
        public DateTime? RequiredDate { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShippedDate] column.
        /// </summary>
        public DateTime? ShippedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShipVia] column.
        /// </summary>
        public int? ShipVia { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Freight] column.
        /// </summary>
        public decimal? Freight { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShipName] column.
        /// </summary>
        public string ShipName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShipAddress] column.
        /// </summary>
        public string ShipAddress { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShipCity] column.
        /// </summary>
        public string ShipCity { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShipRegion] column.
        /// </summary>
        public string ShipRegion { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShipPostalCode] column.
        /// </summary>
        public string ShipPostalCode { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ShipCountry] column.
        /// </summary>
        public string ShipCountry { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Customers] table via column(s) [CustomerID].
        /// </summary>
        public CustomersDto Customers { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [EmployeeID].
        /// </summary>
        public EmployeesDto Employees { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Shippers] table via column(s) [ShipVia].
        /// </summary>
        public ShippersDto Shippers { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Order Details] table via column(s) [OrderID].
        /// </summary>
        [MapListItem("OrderID", "ProductID")]
        public ICollection<OrderDetailsDto> OrderDetailOrderDetailses { get; set; }

        #endregion

    }
}