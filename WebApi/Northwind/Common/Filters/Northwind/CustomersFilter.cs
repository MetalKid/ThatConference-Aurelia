#region << Usings >>

using System;
using ThatConference.Common.Filters;
using QueryFilter;
using QueryFilter.Attributes;

#endregion

namespace ThatConference.Northwind.Common.Filters.Northwind
{
    /// <summary>
    /// This class is used to filter data from the [dbo].[Customers] table.
    /// </summary>
    public class CustomersFilter : FilterBase
    {

        #region << Column Properties >>
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CustomerID] column.
        /// </summary>
        [MapToProperty("CustomerID")]
        public FilterString CustomerID { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [CompanyName] column.
        /// </summary>
        [MapToProperty("CompanyName")]
        public FilterString CompanyName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ContactName] column.
        /// </summary>
        [MapToProperty("ContactName")]
        public FilterString ContactName { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [ContactTitle] column.
        /// </summary>
        [MapToProperty("ContactTitle")]
        public FilterString ContactTitle { get; set; }
        
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
        /// Gets the QueryFilter object to filter on the [Phone] column.
        /// </summary>
        [MapToProperty("Phone")]
        public FilterString Phone { get; set; }
        
        /// <summary>
        /// Gets the QueryFilter object to filter on the [Fax] column.
        /// </summary>
        [MapToProperty("Fax")]
        public FilterString Fax { get; set; }

        #endregion

        #region << Include Properties >>
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[CustomerCustomerDemo] table via the [CustomerID] column.
        /// </summary>
        public Boolean IncludeCustomerCustomerDemos { get; set; }
        
        /// <summary>
        /// Gets or sets whether to include data from the [dbo].[Orders] table via the [CustomerID] column.
        /// </summary>
        public Boolean IncludeOrderOrderses { get; set; }

        #endregion

        #region << Constructor >>

        /// <summary>
        /// Constructor that initializes all QueryFilter properties.
        /// </summary>
        public CustomersFilter()
        {
            CustomerID = new FilterString();
            CompanyName = new FilterString();
            ContactName = new FilterString();
            ContactTitle = new FilterString();
            Address = new FilterString();
            City = new FilterString();
            Region = new FilterString();
            PostalCode = new FilterString();
            Country = new FilterString();
            Phone = new FilterString();
            Fax = new FilterString();
        }

        #endregion

    }
}