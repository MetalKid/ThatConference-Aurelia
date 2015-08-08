#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Territories] table.
    /// </summary>
    public class TerritoriesFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [TerritoryID] column.
        /// </summary>
        [MapToProperty("TerritoryID")]
        public FilterString TerritoryID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [TerritoryDescription] column.
        /// </summary>
        [MapToProperty("TerritoryDescription")]
        public FilterString TerritoryDescription { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [RegionID] column.
        /// </summary>
        [MapToProperty("RegionID")]
        public FilterRange<int> RegionID { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Region] table via the [RegionID] column.
        /// </summary>
        public Boolean IncludeRegion { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[EmployeeTerritories] table via the [TerritoryID] column.
        /// </summary>
        public Boolean IncludeEmployeeTerritorieEmployeeTerritorieses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public TerritoriesFilter()
        {
            TerritoryID = new FilterString();
            TerritoryDescription = new FilterString();
            RegionID = new FilterRange<int>();
        }

        #endregion

    }
}