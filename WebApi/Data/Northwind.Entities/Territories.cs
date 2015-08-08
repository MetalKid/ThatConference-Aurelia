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
    /// This class stores data relating to the [dbo].[Territories] table and all foreign table references.
    /// </summary>
    [Table("Territories", Schema = "dbo")]
    public class Territories : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [TerritoryID] column.
        /// </summary>
        [Column("TerritoryID", Order = 1, TypeName = "nvarchar")]
        [Key]
        [Required]
        [MaxLength(20)]
        public string TerritoryID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [TerritoryDescription] column.
        /// </summary>
        [Column("TerritoryDescription", Order = 2, TypeName = "nchar")]
        [Required]
        [MaxLength(50)]
        public string TerritoryDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [RegionID] column.
        /// </summary>
        [Column("RegionID", Order = 3, TypeName = "int")]
        [Required]
        public int RegionID { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Region] table via column(s) [RegionID].
        /// </summary>
        [ForeignKey("RegionID")]
        public Region Region { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[EmployeeTerritories] table via column(s) [TerritoryID].
        /// </summary>
        [ForeignKey("TerritoryID")]
        public virtual ICollection<EmployeeTerritories> EmployeeTerritorieEmployeeTerritorieses { get; set; }

        #endregion

    }
}