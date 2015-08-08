#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Categories] table.
    /// </summary>
    public class CategoriesFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CategoryID] column.
        /// </summary>
        [MapToProperty("CategoryID")]
        public FilterRange<int> CategoryID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CategoryName] column.
        /// </summary>
        [MapToProperty("CategoryName")]
        public FilterString CategoryName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Description] column.
        /// </summary>
        [MapToProperty("Description")]
        public FilterString Description { get; set; }
        
        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Products] table via the [CategoryID] column.
        /// </summary>
        public Boolean IncludeProductProductses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public CategoriesFilter()
        {
            CategoryID = new FilterRange<int>();
            CategoryName = new FilterString();
            Description = new FilterString();
        }

        #endregion

    }
}