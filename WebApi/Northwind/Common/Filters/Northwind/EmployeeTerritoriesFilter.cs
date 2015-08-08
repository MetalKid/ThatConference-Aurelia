#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[EmployeeTerritories] table.
    /// </summary>
    public class EmployeeTerritoriesFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [EmployeeID] column.
        /// </summary>
        [MapToProperty("EmployeeID")]
        public FilterRange<int> EmployeeID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [TerritoryID] column.
        /// </summary>
        [MapToProperty("TerritoryID")]
        public FilterString TerritoryID { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Employees] table via the [EmployeeID] column.
        /// </summary>
        public Boolean IncludeEmployees { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Territories] table via the [TerritoryID] column.
        /// </summary>
        public Boolean IncludeTerritories { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public EmployeeTerritoriesFilter()
        {
            EmployeeID = new FilterRange<int>();
            TerritoryID = new FilterString();
        }

        #endregion

    }
}