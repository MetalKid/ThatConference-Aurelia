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
    /// This class represents data coming from the [dbo].[Products] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class ProductsDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [ProductID] column.
        /// </summary>
        public int ProductID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ProductName] column.
        /// </summary>
        public string ProductName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [SupplierID] column.
        /// </summary>
        public int? SupplierID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [CategoryID] column.
        /// </summary>
        public int? CategoryID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [QuantityPerUnit] column.
        /// </summary>
        public string QuantityPerUnit { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [UnitPrice] column.
        /// </summary>
        public decimal? UnitPrice { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [UnitsInStock] column.
        /// </summary>
        public short? UnitsInStock { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [UnitsOnOrder] column.
        /// </summary>
        public short? UnitsOnOrder { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ReorderLevel] column.
        /// </summary>
        public short? ReorderLevel { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Discontinued] column.
        /// </summary>
        public bool Discontinued { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Categories] table via column(s) [CategoryID].
        /// </summary>
        public CategoriesDto Categories { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Suppliers] table via column(s) [SupplierID].
        /// </summary>
        public SuppliersDto Suppliers { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Order Details] table via column(s) [ProductID].
        /// </summary>
        [MapListItem("OrderID", "ProductID")]
        public ICollection<OrderDetailsDto> OrderDetailOrderDetailses { get; set; }

        #endregion

    }
}