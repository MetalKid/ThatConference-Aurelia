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
    /// This class stores data relating to the [dbo].[Employees] table and all foreign table references.
    /// </summary>
    [Table("Employees", Schema = "dbo")]
    public class Employees : EntityBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the data from the [EmployeeID] column.
        /// </summary>
        [Column("EmployeeID", Order = 1, TypeName = "int")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [LastName] column.
        /// </summary>
        [Column("LastName", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [FirstName] column.
        /// </summary>
        [Column("FirstName", Order = 3, TypeName = "nvarchar")]
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Title] column.
        /// </summary>
        [Column("Title", Order = 4, TypeName = "nvarchar")]
        [MaxLength(30)]
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [TitleOfCourtesy] column.
        /// </summary>
        [Column("TitleOfCourtesy", Order = 5, TypeName = "nvarchar")]
        [MaxLength(25)]
        public string TitleOfCourtesy { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [BirthDate] column.
        /// </summary>
        [Column("BirthDate", Order = 6, TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [HireDate] column.
        /// </summary>
        [Column("HireDate", Order = 7, TypeName = "datetime")]
        public DateTime? HireDate { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Address] column.
        /// </summary>
        [Column("Address", Order = 8, TypeName = "nvarchar")]
        [MaxLength(60)]
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [City] column.
        /// </summary>
        [Column("City", Order = 9, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Region] column.
        /// </summary>
        [Column("Region", Order = 10, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string Region { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [PostalCode] column.
        /// </summary>
        [Column("PostalCode", Order = 11, TypeName = "nvarchar")]
        [MaxLength(10)]
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Country] column.
        /// </summary>
        [Column("Country", Order = 12, TypeName = "nvarchar")]
        [MaxLength(15)]
        public string Country { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [HomePhone] column.
        /// </summary>
        [Column("HomePhone", Order = 13, TypeName = "nvarchar")]
        [MaxLength(24)]
        public string HomePhone { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Extension] column.
        /// </summary>
        [Column("Extension", Order = 14, TypeName = "nvarchar")]
        [MaxLength(4)]
        public string Extension { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Photo] column.
        /// </summary>
        [Column("Photo", Order = 15, TypeName = "image")]
        [MaxLength(2147483647)]
        public byte[] Photo { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [Notes] column.
        /// </summary>
        [Column("Notes", Order = 16, TypeName = "ntext")]
        [MaxLength(1073741823)]
        public string Notes { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [ReportsTo] column.
        /// </summary>
        [Column("ReportsTo", Order = 17, TypeName = "int")]
        public int? ReportsTo { get; set; }
        
        /// <summary>
        /// Gets or sets the data from the [PhotoPath] column.
        /// </summary>
        [Column("PhotoPath", Order = 18, TypeName = "nvarchar")]
        [MaxLength(255)]
        public string PhotoPath { get; set; }

        #endregion

        #region << Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [ReportsTo].
        /// </summary>
        [ForeignKey("ReportsTo")]
        public Employees ReportToEmployee { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [ReportsTo].
        /// </summary>
        [ForeignKey("ReportsTo")]
        public virtual ICollection<Employees> EmployeeEmployeeses { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[EmployeeTerritories] table via column(s) [EmployeeID].
        /// </summary>
        [ForeignKey("EmployeeID")]
        public virtual ICollection<EmployeeTerritories> EmployeeTerritorieEmployeeTerritorieses { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Orders] table via column(s) [EmployeeID].
        /// </summary>
        [ForeignKey("EmployeeID")]
        public virtual ICollection<Orders> OrderOrderses { get; set; }

        #endregion

    }
}