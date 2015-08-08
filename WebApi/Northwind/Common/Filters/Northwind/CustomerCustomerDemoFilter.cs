#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[CustomerCustomerDemo] table.
    /// </summary>
    public class CustomerCustomerDemoFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CustomerID] column.
        /// </summary>
        [MapToProperty("CustomerID")]
        public FilterString CustomerID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CustomerTypeID] column.
        /// </summary>
        [MapToProperty("CustomerTypeID")]
        public FilterString CustomerTypeID { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[CustomerDemographics] table via the [CustomerTypeID] column.
        /// </summary>
        public Boolean IncludeCustomerDemographics { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Customers] table via the [CustomerID] column.
        /// </summary>
        public Boolean IncludeCustomers { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public CustomerCustomerDemoFilter()
        {
            CustomerID = new FilterString();
            CustomerTypeID = new FilterString();
        }

        #endregion

    }
}