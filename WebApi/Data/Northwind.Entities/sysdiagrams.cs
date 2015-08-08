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
    /// This class stores data relating to the [dbo].[sysdiagrams] table and all foreign table references.
    /// </summary>
    [Table("sysdiagrams", Schema = "dbo")]
    public class sysdiagrams : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [name] column.
        /// </summary>
        [Column("name", Order = 1, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        public string name { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [principal_id] column.
        /// </summary>
        [Column("principal_id", Order = 2, TypeName = "int")]
        [Required]
        public int principalid { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [diagram_id] column.
        /// </summary>
        [Column("diagram_id", Order = 3, TypeName = "int")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int diagramid { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [version] column.
        /// </summary>
        [Column("version", Order = 4, TypeName = "int")]
        public int? version { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [definition] column.
        /// </summary>
        [Column("definition", Order = 5, TypeName = "varbinary")]
        public byte[] definition { get; set; }

        #endregion

        #region << Navigation Properties >>

        #endregion

    }
}