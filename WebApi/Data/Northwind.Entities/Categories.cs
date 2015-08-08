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
    /// This class stores data relating to the [dbo].[Categories] table and all foreign table references.
    /// </summary>
    [Table("Categories", Schema = "dbo")]
    public class Categories : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [CategoryID] column.
        /// </summary>
        [Column("CategoryID", Order = 1, TypeName = "int")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CategoryID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [CategoryName] column.
        /// </summary>
        [Column("CategoryName", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(15)]
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Description] column.
        /// </summary>
        [Column("Description", Order = 3, TypeName = "ntext")]
        [MaxLength(1073741823)]
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Picture] column.
        /// </summary>
        [Column("Picture", Order = 4, TypeName = "image")]
        [MaxLength(2147483647)]
        public byte[] Picture { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Products] table via column(s) [CategoryID].
        /// </summary>
        [ForeignKey("CategoryID")]
        public virtual ICollection<Products> ProductProductses { get; set; }

        #endregion

    }
}