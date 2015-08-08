#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[CustomerDemographics] table.
    /// </summary>
    public class CustomerDemographicsFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CustomerTypeID] column.
        /// </summary>
        [MapToProperty("CustomerTypeID")]
        public FilterString CustomerTypeID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CustomerDesc] column.
        /// </summary>
        [MapToProperty("CustomerDesc")]
        public FilterString CustomerDesc { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[CustomerCustomerDemo] table via the [CustomerTypeID] column.
        /// </summary>
        public Boolean IncludeCustomerCustomerDemos { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public CustomerDemographicsFilter()
        {
            CustomerTypeID = new FilterString();
            CustomerDesc = new FilterString();
        }

        #endregion

    }
}