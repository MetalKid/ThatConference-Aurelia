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
    /// This class stores data relating to the [dbo].[Shippers] table and all foreign table references.
    /// </summary>
    [Table("Shippers", Schema = "dbo")]
    public class Shippers : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [ShipperID] column.
        /// </summary>
        [Column("ShipperID", Order = 1, TypeName = "int")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ShipperID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [CompanyName] column.
        /// </summary>
        [Column("CompanyName", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Phone] column.
        /// </summary>
        [Column("Phone", Order = 3, TypeName = "nvarchar")]
        [MaxLength(24)]
        public string Phone { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Orders] table via column(s) [ShipVia].
        /// </summary>
        [ForeignKey("ShipVia")]
        public virtual ICollection<Orders> OrderOrderses { get; set; }

        #endregion

    }
}