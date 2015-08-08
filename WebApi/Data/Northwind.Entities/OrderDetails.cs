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
    /// This class stores data relating to the [dbo].[Order Details] table and all foreign table references.
    /// </summary>
    [Table("Order Details", Schema = "dbo")]
    public class OrderDetails : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [OrderID] column.
        /// </summary>
        [Column("OrderID", Order = 1, TypeName = "int")]
        [Key]
        [Required]
        public int OrderID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ProductID] column.
        /// </summary>
        [Column("ProductID", Order = 2, TypeName = "int")]
        [Key]
        [Required]
        public int ProductID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [UnitPrice] column.
        /// </summary>
        [Column("UnitPrice", Order = 3, TypeName = "money")]
        [Required]
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Quantity] column.
        /// </summary>
        [Column("Quantity", Order = 4, TypeName = "smallint")]
        [Required]
        public short Quantity { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Discount] column.
        /// </summary>
        [Column("Discount", Order = 5, TypeName = "real")]
        [Required]
        public Single Discount { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Orders] table via column(s) [OrderID].
        /// </summary>
        [ForeignKey("OrderID")]
        public Orders Orders { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Products] table via column(s) [ProductID].
        /// </summary>
        [ForeignKey("ProductID")]
        public Products Products { get; set; }

        #endregion

    }
}