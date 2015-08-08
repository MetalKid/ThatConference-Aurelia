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
    /// This class stores data relating to the [dbo].[CustomerCustomerDemo] table and all foreign table references.
    /// </summary>
    [Table("CustomerCustomerDemo", Schema = "dbo")]
    public class CustomerCustomerDemo : EntityBase
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
        /// Gets or sets the data from the [CustomerTypeID] column.
        /// </summary>
        [Column("CustomerTypeID", Order = 2, TypeName = "nchar")]
        [Key]
        [Required]
        [MaxLength(10)]
        public string CustomerTypeID { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[CustomerDemographics] table via column(s) [CustomerTypeID].
        /// </summary>
        [ForeignKey("CustomerTypeID")]
        public CustomerDemographics CustomerDemographics { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Customers] table via column(s) [CustomerID].
        /// </summary>
        [ForeignKey("CustomerID")]
        public Customers Customers { get; set; }

        #endregion

    }
}