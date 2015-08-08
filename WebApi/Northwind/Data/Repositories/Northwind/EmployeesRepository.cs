#region << Usings >>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using ThatConference.Common.Enums;
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
    /// This repository is responsible for getting, validating, and saving data for the [dbo].[Employees] table.
    /// </summary>
    internal class EmployeesRepository : 
        RepositoryBase<INorthwindContext, Employees, EmployeesFilter, INorthwindScope>, 
        IEmployeesRepository
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scope">The current user's information.</param>
        /// <param name="contextFactory">The factory to create new connections to the database, if needed.</param>
        public EmployeesRepository(
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
        protected override IQueryable<Employees> ApplyGetIncludes(
            INorthwindContext ctx,
            IQueryable<Employees> query, 
            EmployeesFilter filter)
        {
            if (filter == null)
            {
                return query;
            }

            if (filter.IncludeEmployees)
            {
                query = query.Include(a => a.ReportToEmployee);
            }
            if (filter.IncludeEmployeeEmployeeses)
            {
                StoreData(query.SelectMany(a => a.EmployeeEmployeeses));
            }
            if (filter.IncludeEmployeeTerritorieEmployeeTerritorieses)
            {
                StoreData(query.SelectMany(a => a.EmployeeTerritorieEmployeeTerritorieses));
            }
            if (filter.IncludeOrderOrderses)
            {
                StoreData(query.SelectMany(a => a.OrderOrderses));
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
        protected override IQueryable<Employees> ApplyGetOrderBy(
            INorthwindContext ctx,
            IQueryable<Employees> query, 
            EmployeesFilter filter)
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
			Employees entity)
        {
            var pk = new Lazy<string>(() => entity.EmployeeID.ToString(CultureInfo.InvariantCulture));

            brokenRules.Required(string.IsNullOrEmpty(entity.LastName), "LastName", "LastName", pk);
            brokenRules.Required(string.IsNullOrEmpty(entity.FirstName), "FirstName", "FirstName", pk);
            brokenRules.Required(!entity.HireDate.HasValue, "HireDate", "HireDate", pk);

            brokenRules.MaxLength(20, entity.LastName, "LastName", "LastName", pk);
            brokenRules.MaxLength(10, entity.FirstName, "FirstName", "FirstName", pk);
            brokenRules.MaxLength(30, entity.Title, "Title", "Title", pk);
            brokenRules.MaxLength(25, entity.TitleOfCourtesy, "TitleOfCourtesy", "TitleOfCourtesy", pk);
            brokenRules.MaxLength(60, entity.Address, "Address", "Address", pk);
            brokenRules.MaxLength(15, entity.City, "City", "City", pk);
            brokenRules.MaxLength(15, entity.Region, "Region", "Region", pk);
            brokenRules.MaxLength(10, entity.PostalCode, "PostalCode", "PostalCode", pk);
            brokenRules.MaxLength(15, entity.Country, "Country", "Country", pk);
            brokenRules.MaxLength(24, entity.HomePhone, "HomePhone", "HomePhone", pk);
            brokenRules.MaxLength(4, entity.Extension, "Extension", "Extension", pk);
            brokenRules.MaxLength(1073741823, entity.Notes, "Notes", "Notes", pk);
            brokenRules.MaxLength(255, entity.PhotoPath, "PhotoPath", "PhotoPath", pk);

            if (entity.DataState == DataStateEnum.Deleted)
            {
                if (ctx.OrderOrderses.Any(a => a.EmployeeID == entity.EmployeeID))
                {
                    brokenRules.Add(new BrokenRule("An Order already exists under this employee, so this record cannot be deleted."));
                }
                else if (ctx.EmployeeTerritorieEmployeeTerritorieses.Any(a => a.EmployeeID == entity.EmployeeID))
                {
                    brokenRules.Add(new BrokenRule("An Employee Territory already exists under this employee, sothis record cannot be deleted."));
                }
                else if (ctx.EmployeeEmployeeses.Any(a => a.ReportsTo == entity.EmployeeID))
                {
                    brokenRules.Add(new BrokenRule("Another employee reports to this employee already, so this record cannot be deleted."));
                }
            }
        }

        #endregion

    }
}