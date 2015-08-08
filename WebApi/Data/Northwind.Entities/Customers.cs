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
    /// This class stores data relating to the [dbo].[Customers] table and all foreign table references.
    /// </summary>
    [Table("Customers", Schema = "dbo")]
    public class Customers : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [CustomerID] column.
        /// </summary>
        [Column("CustomerID", Order = 1, TypeName = "nchar")]
        [Key]
        [Required]
        [MaxLength(5)]
        public string CustomerID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [CompanyName] column.
        /// </summary>
        [Column("CompanyName", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ContactName] column.
        /// </summary>
        [Column("ContactName", Order = 3, TypeName = "nvarchar")]
        [MaxLength(30)]
        public string ContactName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ContactTitle] column.
        /// </summary>
        [Column("ContactTitle", Order = 4, TypeName = "nvarchar")]
        [MaxLength(30)]
        public string ContactTitle { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Address] column.
        /// </summary>
        [Column("Address", Order = 5, TypeName = "nvarchar")]
        [MaxLength(60)]
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [City] column.
        /// </summary>
        [Column("City", Order = 6, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Region] column.
        /// </summary>
        [Column("Region", Order = 7, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string Region { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [PostalCode] column.
        /// </summary>
        [Column("PostalCode", Order = 8, TypeName = "nvarchar")]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Country] column.
        /// </summary>
        [Column("Country", Order = 9, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string Country { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Phone] column.
        /// </summary>
        [Column("Phone", Order = 10, TypeName = "nvarchar")]
        [MaxLength(24)]
        public string Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Fax] column.
        /// </summary>
        [Column("Fax", Order = 11, TypeName = "nvarchar")]
        [MaxLength(24)]
        public string Fax { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[CustomerCustomerDemo] table via column(s) [CustomerID].
        /// </summary>
        [ForeignKey("CustomerID")]
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Orders] table via column(s) [CustomerID].
        /// </summary>
        [ForeignKey("CustomerID")]
        public virtual ICollection<Orders> OrderOrderses { get; set; }

        #endregion

    }
}