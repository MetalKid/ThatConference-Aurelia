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
    /// This class represents data coming from the [dbo].[Order Details] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class OrderDetailsDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [OrderID] column.
        /// </summary>
        public int OrderID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ProductID] column.
        /// </summary>
        public int ProductID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [UnitPrice] column.
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Quantity] column.
        /// </summary>
        public short Quantity { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Discount] column.
        /// </summary>
        public Single Discount { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Orders] table via column(s) [OrderID].
        /// </summary>
        public OrdersDto Orders { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Products] table via column(s) [ProductID].
        /// </summary>
        public ProductsDto Products { get; set; }

        #endregion

    }
}