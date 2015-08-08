#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Employees] table.
    /// </summary>
    public class EmployeesFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [EmployeeID] column.
        /// </summary>
        [MapToProperty("EmployeeID")]
        public FilterRange<int> EmployeeID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [LastName] column.
        /// </summary>
        [MapToProperty("LastName")]
        public FilterString LastName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [FirstName] column.
        /// </summary>
        [MapToProperty("FirstName")]
        public FilterString FirstName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Title] column.
        /// </summary>
        [MapToProperty("Title")]
        public FilterString Title { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [TitleOfCourtesy] column.
        /// </summary>
        [MapToProperty("TitleOfCourtesy")]
        public FilterString TitleOfCourtesy { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [BirthDate] column.
        /// </summary>
        [MapToProperty("BirthDate")]
        public FilterRange<DateTime> BirthDate { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [HireDate] column.
        /// </summary>
        [MapToProperty("HireDate")]
        public FilterRange<DateTime> HireDate { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Address] column.
        /// </summary>
        [MapToProperty("Address")]
        public FilterString Address { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [City] column.
        /// </summary>
        [MapToProperty("City")]
        public FilterString City { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Region] column.
        /// </summary>
        [MapToProperty("Region")]
        public FilterString Region { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [PostalCode] column.
        /// </summary>
        [MapToProperty("PostalCode")]
        public FilterString PostalCode { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Country] column.
        /// </summary>
        [MapToProperty("Country")]
        public FilterString Country { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [HomePhone] column.
        /// </summary>
        [MapToProperty("HomePhone")]
        public FilterString HomePhone { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Extension] column.
        /// </summary>
        [MapToProperty("Extension")]
        public FilterString Extension { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Notes] column.
        /// </summary>
        [MapToProperty("Notes")]
        public FilterString Notes { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ReportsTo] column.
        /// </summary>
        [MapToProperty("ReportsTo")]
        public FilterRange<int> ReportsTo { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [PhotoPath] column.
        /// </summary>
        [MapToProperty("PhotoPath")]
        public FilterString PhotoPath { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Employees] table via the [ReportsTo] column.
        /// </summary>
        public Boolean IncludeEmployees { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Employees] table via the [ReportsTo] column.
        /// </summary>
        public Boolean IncludeEmployeeEmployeeses { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[EmployeeTerritories] table via the [EmployeeID] column.
        /// </summary>
        public Boolean IncludeEmployeeTerritorieEmployeeTerritorieses { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Orders] table via the [EmployeeID] column.
        /// </summary>
        public Boolean IncludeOrderOrderses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public EmployeesFilter()
        {
            EmployeeID = new FilterRange<int>();
            LastName = new FilterString();
            FirstName = new FilterString();
            Title = new FilterString();
            TitleOfCourtesy = new FilterString();
            BirthDate = new FilterRange<DateTime>();
            HireDate = new FilterRange<DateTime>();
            Address = new FilterString();
            City = new FilterString();
            Region = new FilterString();
            PostalCode = new FilterString();
            Country = new FilterString();
            HomePhone = new FilterString();
            Extension = new FilterString();
            Notes = new FilterString();
            ReportsTo = new FilterRange<int>();
            PhotoPath = new FilterString();
        }

        #endregion

    }
}