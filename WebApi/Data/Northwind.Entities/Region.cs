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
    /// This class stores data relating to the [dbo].[Region] table and all foreign table references.
    /// </summary>
    [Table("Region", Schema = "dbo")]
    public class Region : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [RegionID] column.
        /// </summary>
        [Column("RegionID", Order = 1, TypeName = "int")]
        [Key]
        [Required]
        public int RegionID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [RegionDescription] column.
        /// </summary>
        [Column("RegionDescription", Order = 2, TypeName = "nchar")]
        [Required]
        [MaxLength(50)]
        public string RegionDescription { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Territories] table via column(s) [RegionID].
        /// </summary>
        [ForeignKey("RegionID")]
        public virtual ICollection<Territories> TerritorieTerritorieses { get; set; }

        #endregion

    }
}