#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[sysdiagrams] table.
    /// </summary>
    public class sysdiagramsFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [name] column.
        /// </summary>
        [MapToProperty("name")]
        public FilterString name { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [principal_id] column.
        /// </summary>
        [MapToProperty("principalid")]
        public FilterRange<int> principalid { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [diagram_id] column.
        /// </summary>
        [MapToProperty("diagramid")]
        public FilterRange<int> diagramid { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [version] column.
        /// </summary>
        [MapToProperty("version")]
        public FilterRange<int> version { get; set; }

        #endregion

        #region << Include Properties >>

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public sysdiagramsFilter()
        {
            name = new FilterString();
            principalid = new FilterRange<int>();
            diagramid = new FilterRange<int>();
            version = new FilterRange<int>();
        }

        #endregion

    }
}