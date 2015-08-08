#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Region] table.
    /// </summary>
    public class RegionFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [RegionID] column.
        /// </summary>
        [MapToProperty("RegionID")]
        public FilterRange<int> RegionID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [RegionDescription] column.
        /// </summary>
        [MapToProperty("RegionDescription")]
        public FilterString RegionDescription { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Territories] table via the [RegionID] column.
        /// </summary>
        public Boolean IncludeTerritorieTerritorieses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public RegionFilter()
        {
            RegionID = new FilterRange<int>();
            RegionDescription = new FilterString();
        }

        #endregion

    }
}