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
    /// This class stores data relating to the [dbo].[CustomerDemographics] table and all foreign table references.
    /// </summary>
    [Table("CustomerDemographics", Schema = "dbo")]
    public class CustomerDemographics : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [CustomerTypeID] column.
        /// </summary>
        [Column("CustomerTypeID", Order = 1, TypeName = "nchar")]
        [Key]
        [Required]
        [MaxLength(10)]
        public string CustomerTypeID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [CustomerDesc] column.
        /// </summary>
        [Column("CustomerDesc", Order = 2, TypeName = "ntext")]
        [MaxLength(1073741823)]
        public string CustomerDesc { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[CustomerCustomerDemo] table via column(s) [CustomerTypeID].
        /// </summary>
        [ForeignKey("CustomerTypeID")]
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }

        #endregion

    }
}