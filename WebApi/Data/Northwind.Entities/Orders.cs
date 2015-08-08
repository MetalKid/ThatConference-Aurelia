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
    /// This class stores data relating to the [dbo].[Orders] table and all foreign table references.
    /// </summary>
    [Table("Orders", Schema = "dbo")]
    public class Orders : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [OrderID] column.
        /// </summary>
        [Column("OrderID", Order = 1, TypeName = "int")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [CustomerID] column.
        /// </summary>
        [Column("CustomerID", Order = 2, TypeName = "nchar")]
        [MaxLength(5)]
        public string CustomerID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [EmployeeID] column.
        /// </summary>
        [Column("EmployeeID", Order = 3, TypeName = "int")]
        public int? EmployeeID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [OrderDate] column.
        /// </summary>
        [Column("OrderDate", Order = 4, TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [RequiredDate] column.
        /// </summary>
        [Column("RequiredDate", Order = 5, TypeName = "datetime")]
        public DateTime? RequiredDate { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShippedDate] column.
        /// </summary>
        [Column("ShippedDate", Order = 6, TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShipVia] column.
        /// </summary>
        [Column("ShipVia", Order = 7, TypeName = "int")]
        public int? ShipVia { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Freight] column.
        /// </summary>
        [Column("Freight", Order = 8, TypeName = "money")]
        public decimal? Freight { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShipName] column.
        /// </summary>
        [Column("ShipName", Order = 9, TypeName = "nvarchar")]
        [MaxLength(40)]
        public string ShipName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShipAddress] column.
        /// </summary>
        [Column("ShipAddress", Order = 10, TypeName = "nvarchar")]
        [MaxLength(60)]
        public string ShipAddress { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShipCity] column.
        /// </summary>
        [Column("ShipCity", Order = 11, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string ShipCity { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShipRegion] column.
        /// </summary>
        [Column("ShipRegion", Order = 12, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string ShipRegion { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShipPostalCode] column.
        /// </summary>
        [Column("ShipPostalCode", Order = 13, TypeName = "nvarchar")]
        [MaxLength(10)]
        public string ShipPostalCode { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ShipCountry] column.
        /// </summary>
        [Column("ShipCountry", Order = 14, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string ShipCountry { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Customers] table via column(s) [CustomerID].
        /// </summary>
        [ForeignKey("CustomerID")]
        public Customers Customers { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [EmployeeID].
        /// </summary>
        [ForeignKey("EmployeeID")]
        public Employees Employees { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Shippers] table via column(s) [ShipVia].
        /// </summary>
        [ForeignKey("ShipVia")]
        public Shippers Shippers { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Order Details] table via column(s) [OrderID].
        /// </summary>
        [ForeignKey("OrderID")]
        public virtual ICollection<OrderDetails> OrderDetailOrderDetailses { get; set; }

        #endregion

    }
}