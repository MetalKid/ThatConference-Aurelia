#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Shippers] table.
    /// </summary>
    public class ShippersFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ShipperID] column.
        /// </summary>
        [MapToProperty("ShipperID")]
        public FilterRange<int> ShipperID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CompanyName] column.
        /// </summary>
        [MapToProperty("CompanyName")]
        public FilterString CompanyName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Phone] column.
        /// </summary>
        [MapToProperty("Phone")]
        public FilterString Phone { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Orders] table via the [ShipVia] column.
        /// </summary>
        public Boolean IncludeOrderOrderses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public ShippersFilter()
        {
            ShipperID = new FilterRange<int>();
            CompanyName = new FilterString();
            Phone = new FilterString();
        }

        #endregion

    }
}