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
    /// This repository is responsible for getting, validating, and saving data for the [dbo].[EmployeeTerritories] table.
    /// </summary>
    internal class EmployeeTerritoriesRepository : 
        RepositoryBase<INorthwindContext, EmployeeTerritories, EmployeeTerritoriesFilter, INorthwindScope>, 
        IEmployeeTerritoriesRepository
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scope">The current user's information.</param>
        /// <param name="contextFactory">The factory to create new connections to the database, if needed.</param>
        public EmployeeTerritoriesRepository(
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
        protected override IQueryable<EmployeeTerritories> ApplyGetIncludes(
            INorthwindContext ctx,
            IQueryable<EmployeeTerritories> query, 
            EmployeeTerritoriesFilter filter)
        {
            if (filter == null)
            {
                return query;
            }

            if (filter.IncludeEmployees)
            {
                query = query.Include(a => a.Employees);
            }
            if (filter.IncludeTerritories)
            {
                query = query.Include(a => a.Territories);
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
        protected override IQueryable<EmployeeTerritories> ApplyGetOrderBy(
            INorthwindContext ctx,
            IQueryable<EmployeeTerritories> query, 
            EmployeeTerritoriesFilter filter)
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
			EmployeeTerritories entity)
        {
            var pk = new Lazy<string>(() => entity.EmployeeID.ToString(CultureInfo.InvariantCulture) + (entity.TerritoryID ?? ""));

            brokenRules.Required(entity.TerritoryID == null, "TerritoryID", "TerritoryID", pk);

            brokenRules.MaxLength(20, entity.TerritoryID, "TerritoryID", "TerritoryID", pk);
        }

        #endregion

    }
}