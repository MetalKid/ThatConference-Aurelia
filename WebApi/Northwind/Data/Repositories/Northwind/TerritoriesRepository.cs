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
    /// This repository is responsible for getting, validating, and saving data for the [dbo].[Territories] table.
    /// </summary>
    internal class TerritoriesRepository : 
        RepositoryBase<INorthwindContext, Territories, TerritoriesFilter, INorthwindScope>, 
        ITerritoriesRepository
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scope">The current user's information.</param>
        /// <param name="contextFactory">The factory to create new connections to the database, if needed.</param>
        public TerritoriesRepository(
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
        protected override IQueryable<Territories> ApplyGetIncludes(
            INorthwindContext ctx,
            IQueryable<Territories> query, 
            TerritoriesFilter filter)
        {
            if (filter == null)
            {
                return query;
            }

            if (filter.IncludeRegion)
            {
                query = query.Include(a => a.Region);
            }
            if (filter.IncludeEmployeeTerritorieEmployeeTerritorieses)
            {
                StoreData(query.SelectMany(a => a.EmployeeTerritorieEmployeeTerritorieses));
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
        protected override IQueryable<Territories> ApplyGetOrderBy(
            INorthwindContext ctx,
            IQueryable<Territories> query, 
            TerritoriesFilter filter)
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
			Territories entity)
        {
            var pk = new Lazy<string>(() => entity.TerritoryID);

            brokenRules.Required(entity.TerritoryID == null, "TerritoryID", "TerritoryID", pk);
            brokenRules.Required(entity.TerritoryDescription == null, "TerritoryDescription", "TerritoryDescription", pk);

            brokenRules.MaxLength(20, entity.TerritoryID, "TerritoryID", "TerritoryID", pk);
            brokenRules.MaxLength(50, entity.TerritoryDescription, "TerritoryDescription", "TerritoryDescription", pk);
        }

        #endregion

    }
}