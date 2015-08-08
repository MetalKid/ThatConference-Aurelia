#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Products] table.
    /// </summary>
    public class ProductsFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ProductID] column.
        /// </summary>
        [MapToProperty("ProductID")]
        public FilterRange<int> ProductID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ProductName] column.
        /// </summary>
        [MapToProperty("ProductName")]
        public FilterString ProductName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [SupplierID] column.
        /// </summary>
        [MapToProperty("SupplierID")]
        public FilterRange<int> SupplierID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CategoryID] column.
        /// </summary>
        [MapToProperty("CategoryID")]
        public FilterRange<int> CategoryID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [QuantityPerUnit] column.
        /// </summary>
        [MapToProperty("QuantityPerUnit")]
        public FilterString QuantityPerUnit { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [UnitPrice] column.
        /// </summary>
        [MapToProperty("UnitPrice")]
        public FilterRange<decimal> UnitPrice { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [UnitsInStock] column.
        /// </summary>
        [MapToProperty("UnitsInStock")]
        public FilterRange<short> UnitsInStock { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [UnitsOnOrder] column.
        /// </summary>
        [MapToProperty("UnitsOnOrder")]
        public FilterRange<short> UnitsOnOrder { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ReorderLevel] column.
        /// </summary>
        [MapToProperty("ReorderLevel")]
        public FilterRange<short> ReorderLevel { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Discontinued] column.
        /// </summary>
        [MapToProperty("Discontinued")]
        public FilterEquatable<bool?> Discontinued { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Categories] table via the [CategoryID] column.
        /// </summary>
        public Boolean IncludeCategories { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Suppliers] table via the [SupplierID] column.
        /// </summary>
        public Boolean IncludeSuppliers { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Order Details] table via the [ProductID] column.
        /// </summary>
        public Boolean IncludeOrderDetailOrderDetailses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public ProductsFilter()
        {
            ProductID = new FilterRange<int>();
            ProductName = new FilterString();
            SupplierID = new FilterRange<int>();
            CategoryID = new FilterRange<int>();
            QuantityPerUnit = new FilterString();
            UnitPrice = new FilterRange<decimal>();
            UnitsInStock = new FilterRange<short>();
            UnitsOnOrder = new FilterRange<short>();
            ReorderLevel = new FilterRange<short>();
            Discontinued = new FilterEquatable<bool?>();
        }

        #endregion

    }
}