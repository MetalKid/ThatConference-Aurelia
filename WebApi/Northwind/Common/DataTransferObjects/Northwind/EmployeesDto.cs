#region << Usings >>

using System;
using System.Collections.Generic;
using ClassyMapper.Attributes;
using ThatConference.Common;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Common.DataTransferObjects.Northwind
{
    /// <summary>
    /// This class represents data coming from the [dbo].[Employees] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class EmployeesDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [EmployeeID] column.
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [LastName] column.
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [FirstName] column.
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Title] column.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [TitleOfCourtesy] column.
        /// </summary>
        public string TitleOfCourtesy { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [BirthDate] column.
        /// </summary>
        public DateTime? BirthDate { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [HireDate] column.
        /// </summary>
        public DateTime? HireDate { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Address] column.
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [City] column.
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Region] column.
        /// </summary>
        public string Region { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [PostalCode] column.
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Country] column.
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [HomePhone] column.
        /// </summary>
        public string HomePhone { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Extension] column.
        /// </summary>
        public string Extension { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Photo] column.
        /// </summary>
        public byte[] Photo { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Notes] column.
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ReportsTo] column.
        /// </summary>
        public int? ReportsTo { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [PhotoPath] column.
        /// </summary>
        public string PhotoPath { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [ReportsTo].
        /// </summary>
        public EmployeesDto ReportToEmployee { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [ReportsTo].
        /// </summary>
        [MapListItem("EmployeeID")]
        public ICollection<EmployeesDto> EmployeeEmployeeses { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[EmployeeTerritories] table via column(s) [EmployeeID].
        /// </summary>
        [MapListItem("EmployeeID", "TerritoryID")]
        public ICollection<EmployeeTerritoriesDto> EmployeeTerritorieEmployeeTerritorieses { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Orders] table via column(s) [EmployeeID].
        /// </summary>
        [MapListItem("OrderID")]
        public ICollection<OrdersDto> OrderOrderses { get; set; }

        #endregion

    }
}