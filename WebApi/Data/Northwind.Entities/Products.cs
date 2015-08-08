#region << Usings >>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThatConference.Data.Common;
using ThatConference.Data.Common.Attributes;

#endregion

namespace ThatConference.Data.Northwind.Entities
{
    /// <summary>
    /// This class stores data relating to the [dbo].[Products] table and all foreign table references.
    /// </summary>
    [Table("Products", Schema = "dbo")]
    public class Products : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [ProductID] column.
        /// </summary>
        [Column("ProductID", Order = 1, TypeName = "int")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ProductID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ProductName] column.
        /// </summary>
        [Column("ProductName", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [SupplierID] column.
        /// </summary>
        [Column("SupplierID", Order = 3, TypeName = "int")]
        public int? SupplierID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [CategoryID] column.
        /// </summary>
        [Column("CategoryID", Order = 4, TypeName = "int")]
        public int? CategoryID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [QuantityPerUnit] column.
        /// </summary>
        [Column("QuantityPerUnit", Order = 5, TypeName = "nvarchar")]
        [MaxLength(20)]
        public string QuantityPerUnit { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [UnitPrice] column.
        /// </summary>
        [Column("UnitPrice", Order = 6, TypeName = "money")]
        public decimal? UnitPrice { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [UnitsInStock] column.
        /// </summary>
        [Column("UnitsInStock", Order = 7, TypeName = "smallint")]
        public short? UnitsInStock { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [UnitsOnOrder] column.
        /// </summary>
        [Column("UnitsOnOrder", Order = 8, TypeName = "smallint")]
        public short? UnitsOnOrder { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ReorderLevel] column.
        /// </summary>
        [Column("ReorderLevel", Order = 9, TypeName = "smallint")]
        public short? ReorderLevel { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Discontinued] column.
        /// </summary>
        [Column("Discontinued", Order = 10, TypeName = "bit")]
        [Required]
        public bool Discontinued { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Categories] table via column(s) [CategoryID].
        /// </summary>
        [ForeignKey("CategoryID")]
        public Categories Categories { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Suppliers] table via column(s) [SupplierID].
        /// </summary>
        [ForeignKey("SupplierID")]
        public Suppliers Suppliers { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Order Details] table via column(s) [ProductID].
        /// </summary>
        [ForeignKey("ProductID")]
        public virtual ICollection<OrderDetails> OrderDetailOrderDetailses { get; set; }

        #endregion

    }
}