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
    /// This class stores data relating to the [dbo].[EmployeeTerritories] table and all foreign table references.
    /// </summary>
    [Table("EmployeeTerritories", Schema = "dbo")]
    public class EmployeeTerritories : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [EmployeeID] column.
        /// </summary>
        [Column("EmployeeID", Order = 1, TypeName = "int")]
        [Key]
        [Required]
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [TerritoryID] column.
        /// </summary>
        [Column("TerritoryID", Order = 2, TypeName = "nvarchar")]
        [Key]
        [Required]
        [MaxLength(20)]
        public string TerritoryID { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [EmployeeID].
        /// </summary>
        [ForeignKey("EmployeeID")]
        public Employees Employees { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Territories] table via column(s) [TerritoryID].
        /// </summary>
        [ForeignKey("TerritoryID")]
        public Territories Territories { get; set; }

        #endregion

    }
}