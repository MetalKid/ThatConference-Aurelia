#region << Usings >>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using ThatConference.Common.Validation;
using ThatConference.Data.Common;
using ThatConference.Data.Northwind.Entities;
using ThatConference.Data.Northwind.Interfaces;
using ThatConference.Northwind.Common.Filters.Northwind;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Interfaces.Repositories.Northwind;

#endregion

namespace ThatConference.Northwind.Data.Repositories.Northwind
{
    /// <summary>
    /// This repository is responsible for getting, validating, and saving data for the [dbo].[Suppliers] table.
    /// </summary>
    internal class SuppliersRepository : 
        RepositoryBase<INorthwindContext, Suppliers, SuppliersFilter, INorthwindScope>, 
        ISuppliersRepository
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scope">The current user's information.</param>
        /// <param name="contextFactory">The factory to create new connections to the database, if needed.</param>
        public SuppliersRepository(
            INorthwindScope scope,
            INorthwindContextFactory contextFactory)
            : base(scope, contextFactory)
        {
        }

        #endregion

        #region << Get Methods >>

        /// <summary>
        /// Returns the query with all include related data.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="query">The current query to run on the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <returns>The query object.</returns>
        protected override IQueryable<Suppliers> ApplyGetIncludes(
            INorthwindContext ctx,
            IQueryable<Suppliers> query, 
            SuppliersFilter filter)
        {
            if (filter == null)
            {
                return query;
            }

            if (filter.IncludeProductProductses)
            {
                StoreData(query.SelectMany(a => a.ProductProductses));
            }

            return query;
        }

        /// <summary>
        /// Returns the query with the Order By clause assigned.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="query">The current query to run on the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <returns>The query object.</returns>
        protected override IQueryable<Suppliers> ApplyGetOrderBy(
            INorthwindContext ctx,
            IQueryable<Suppliers> query, 
            SuppliersFilter filter)
        {
            return query;
        }

        #endregion

        #region << Validation/Save Methods >>
        
        /// <summary>
        /// Validates the given entity against database specific rules before being saved.
        /// </summary>
        /// <param name="brokenRules">The list that contains any broken rules that may have occurred.</param>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entity">The current record being inspected.</param>
        /// <returns>The list of Broken Rules, if any.</returns>
        public override void ValidateEntity(
			IList<BrokenRule> brokenRules, 
			INorthwindContext ctx,
			Suppliers entity)
        {
            var pk = new Lazy<string>(() => entity.SupplierID.ToString(CultureInfo.InvariantCulture));

            brokenRules.Required(entity.CompanyName == null, "CompanyName", "CompanyName", pk);

            brokenRules.MaxLength(40, entity.CompanyName, "CompanyName", "CompanyName", pk);
            brokenRules.MaxLength(30, entity.ContactName, "ContactName", "ContactName", pk);
            brokenRules.MaxLength(30, entity.ContactTitle, "ContactTitle", "ContactTitle", pk);
            brokenRules.MaxLength(60, entity.Address, "Address", "Address", pk);
            brokenRules.MaxLength(15, entity.City, "City", "City", pk);
            brokenRules.MaxLength(15, entity.Region, "Region", "Region", pk);
            brokenRules.MaxLength(10, entity.PostalCode, "PostalCode", "PostalCode", pk);
            brokenRules.MaxLength(15, entity.Country, "Country", "Country", pk);
            brokenRules.MaxLength(24, entity.Phone, "Phone", "Phone", pk);
            brokenRules.MaxLength(24, entity.Fax, "Fax", "Fax", pk);
            brokenRules.MaxLength(1073741823, entity.HomePage, "HomePage", "HomePage", pk);
        }

        #endregion

    }
}