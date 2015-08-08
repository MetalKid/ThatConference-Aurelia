#region << Usings >>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ThatConference.Common;
using ThatConference.Common.Exceptions;
using ThatConference.Common.Filters;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Validation;
using ThatConference.Data.Common.Interfaces;
using QueryFilter;
using QueryFilter.Interfaces;

#endregion

namespace ThatConference.Data.Common
{
   /// <summary>
    /// This class defines the most common methods for all repositories.
    /// </summary>
    /// <typeparam name="TContext">The type of context used to connect to the database.</typeparam>
    /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
    /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
    /// <typeparam name="TScope">The type of IScope that holds data about the user.</typeparam>
    public abstract class RepositoryBase<TContext, TEntity, TFilter, TScope> : IIoC,
        ISelectableRepositoryInternal<TContext, TEntity, TFilter>, IValidatableRepositoryInternal<TContext, TEntity>,
        ISaveableRepositoryInternal<TContext, TEntity> where TContext : class, IDbContext
        where TEntity : EntityBase
        where TFilter : FilterBase
        where TScope : class, IScope
    {

        #region << Properites >>

        /// <summary>
        /// Gets the information about the current user trying to query information from the database.
        /// </summary>
        protected TScope Scope { get; private set; }

        /// <summary>
        /// Gets the information about the current user trying to query information from the database.
        /// </summary>
        protected IDbContextFactory<TContext> ContextFactory { get; private set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scope">The current user's information.</param>
        /// <param name="contextFactory">The context factory to get a new database connection, if needed.</param>
        protected RepositoryBase(TScope scope, IDbContextFactory<TContext> contextFactory)
        {
            Scope = scope;
            ContextFactory = contextFactory;
        }

        #endregion

        #region << Get Methods >>

        /// <summary>
        /// Returns all the data that are within scope and match the given filter criteria.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">The data to filter the query on, if any.</param>
        /// <returns>List of Country entities.</returns>
        public ICollection<TEntity> Get(TContext ctx, TFilter filter)
        {
            IQueryable<TEntity> query = GetFullQuery(ctx, filter);
            ICollection<TEntity> result = query.ToList();

            CheckForConcurrencyIssues(ctx, result, filter);

            result = GetPostQuery(ctx, result, filter);
            return result;
        }

        /// <summary>
        /// Returns all the data that are within scope and match the given filter criteria.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">The data to filter the query on, if any.</param>
        /// <returns>List of Country entities.</returns>
        public async Task<ICollection<TEntity>> GetAsync(TContext ctx, TFilter filter)
        {
            IQueryable<TEntity> query = GetFullQuery(ctx, filter);
            ICollection<TEntity> result = await query.ToListAsync();

            CheckForConcurrencyIssues(ctx, result, filter);

            result = GetPostQuery(ctx, result, filter);
            return result;
        }

        /// <summary>
        /// Returns the total records that come back based upon the given filter.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">The data to filter the query on, if any.</param>
        /// <returns>List of Country entities.</returns>
        public int GetCount(TContext ctx, TFilter filter)
        {
            IQueryable<TEntity> query = GetFullQuery(ctx, filter, true);
            return query.Count();
        }

        /// <summary>
        /// Returns the total records that come back based upon the given filter.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">The data to filter the query on, if any.</param>
        /// <returns>List of Country entities.</returns>
        public async Task<int> GetCountAsync(TContext ctx, TFilter filter)
        {
            IQueryable<TEntity> query = GetFullQuery(ctx, filter, true);
            return await query.CountAsync();
        }

        /// <summary>
        /// Returns the basic Get query without making a call to the database.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <param name="isForGetCount">True if this is being used in GetCount call; false otherwise.</param>
        /// <returns>The current query to run on the database.</returns>
        protected IQueryable<TEntity> GetFullQuery(TContext ctx, TFilter filter, bool isForGetCount = false)
        {
            IQueryable<TEntity> query = ctx.Set<TEntity>();

            query = ApplyQueryFilters(ctx, query, filter);
            query = ApplyGetFilters(ctx, query, filter);
            query = ApplyGetIncludes(ctx, query, filter);
            query = ApplyGetOrderBy(ctx, query, filter);
            query = ApplyBaseFilter(query, filter, isForGetCount);

            return query;
        }

        /// <summary>
        /// Allows custom query filter mappings to be added before applying all QueryFilter filters.
        /// </summary>
        /// <param name="queryFilter">The object to add the custom mappings to</param>
        protected virtual void LoadCustomQueryFilterMaps(IQueryFilterBuilder<TEntity, TFilter> queryFilter)
        {
        }

        /// <summary>
        /// Returns the query with all Query Filter related clauses applied.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="query">The current query to run on the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <returns>The query object.</returns>
        protected IQueryable<TEntity> ApplyQueryFilters(TContext ctx, IQueryable<TEntity> query, TFilter filter)
        {
            var queryFilter = new QueryFilterBuilder<TEntity, TFilter>(ctx.IsDatabaseCaseSensitive);
            LoadCustomQueryFilterMaps(queryFilter);
            return queryFilter.Build(query, filter);
        }

        /// <summary>
        /// Returns the query with all filter related clauses applied.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="query">The current query to run on the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <returns>The query object.</returns>
        protected virtual IQueryable<TEntity> ApplyGetFilters(TContext ctx, IQueryable<TEntity> query, TFilter filter)
        {
            return query;
        }

        /// <summary>
        /// Returns the query with all include related clauses applied.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="query">The current query to run on the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <returns>The query object.</returns>
        protected virtual IQueryable<TEntity> ApplyGetIncludes(TContext ctx, IQueryable<TEntity> query, TFilter filter)
        {
            return query;
        }

        /// <summary>
        /// Returns the query with the Order By clause assigned.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="query">The current query to run on the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <returns>The query object.</returns>
        protected virtual IQueryable<TEntity> ApplyGetOrderBy(TContext ctx, IQueryable<TEntity> query, TFilter filter)
        {
            return query;
        }

        /// <summary>
        /// Use this method to apply the filter criteria on FilterBase.
        /// </summary>
        /// <param name="query">The current query to run on the database.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <param name="isForGetCount">True if this is being used in GetCount call; false otherwise.</param>
        /// <returns>The query object.</returns>
        protected IQueryable<TEntity> ApplyBaseFilter(
            IQueryable<TEntity> query,
            TFilter filter,
            bool isForGetCount = false)
        {
            if (filter == null)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(filter.Timestamp))
            {
                query = query.Take(2); // Should never get back more than one
            }
            if (!isForGetCount  && filter.MaxRecordCount.HasValue)
            {
                query = query.Take(filter.MaxRecordCount.Value);
            }
            if (!isForGetCount && filter.PageNumber.HasValue && filter.PageSize.HasValue)
            {
                var skip = filter.PageNumber.Value*filter.PageSize.Value;
                query = query.Skip(skip).Take(filter.PageSize.Value);
            }
            return query;
        }

        /// <summary>
        /// Returns the items that should be returned to the caller.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="list">The list of items the query returned.</param>
        /// <param name="filter">The data to filter the request on.</param>
        /// <returns>The l ist of items to return to the caller.</returns>
        protected virtual ICollection<TEntity> GetPostQuery(TContext ctx, ICollection<TEntity> list, TFilter filter)
        {
            return list;
        }

        /// <summary>
        /// Ensures there are no concurrency issues with the data.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="list">The list of items the query returned.</param>
        /// <param name="filter">The data to filter the request on.</param>
        private void CheckForConcurrencyIssues(TContext ctx, ICollection<TEntity> list, TFilter filter)
        {
            if (filter == null || string.IsNullOrEmpty(filter.Timestamp))
            {
                return;
            }

            if (list.Count > 1)
            {
                //NOTE: This should be impossible
                throw new DataException(
                    "Filter criteria did not bring back one record when also providing a timestamp.");
            }

            if (list.Count != 1)
            {
                return;
            }

            byte[] timestamp = Convert.FromBase64String(filter.Timestamp);
            if (list.Count(a => a.Timestamp.SequenceEqual(timestamp)) == 0)
            {
                throw new ConcurrencyException();
            }
        }

        /// <summary>
        /// Invokes ToList of IQueryable to store the data on the containing DbContext object.
        /// </summary>
        /// <param name="query">The query to run.</param>
        protected void StoreData<T>(IQueryable<T> query) where T : EntityBase
        {
            query.ToList(); // Calling ToList() will store the data on the DbContext automatically.
        }

        /// <summary>
        /// Invokes ToList of IQueryable to store the data on the containing DbContext object.
        /// </summary>
        /// <param name="query">The query to run.</param>
        protected async Task StoreDataAsync<T>(IQueryable<T> query) where T : EntityBase
        {
            await query.ToListAsync(); // Calling ToList() will store the data on the DbContext automatically.
        }

        #endregion

        #region << Validation Methods >>

        /// <summary>
        /// Validates the given parent and all of its children before being saved.
        /// </summary>
        /// <param name="brokenRules">The list that contains any broken rules that may have occurred.</param>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entity">The current record being inspected.</param>
        /// <returns>The list of Broken Rules, if any.</returns>
        public virtual void ValidateEntity(IList<BrokenRule> brokenRules, TContext ctx, TEntity entity)
        {
        }

        #endregion

        #region << Bulk Insert Methods >>

        /// <summary>
        /// Validates and saves the given entities via a bulk insert and returns the number of affected rows.
        /// Only support SQL Server.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entities">The records to save to the datastore.</param>
        /// <returns>The number of rows inserted.</returns>
        public int BulkInsert(TContext ctx, ICollection<TEntity> entities)
        {
            OnBeforeBulkInsert(entities);

            Type entityType = typeof (TEntity);
            var propInfos = entityType.GetProperties();
            var dataTable = ConvertToDataTable(entities, propInfos);

            var connection = GetConnection(ctx.Database.Connection.ConnectionString);

            using (var transaction = connection.BeginTransaction())
            {
                using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    SetupBulkCopy(sqlBulkCopy, entityType, propInfos);
                    sqlBulkCopy.WriteToServer(dataTable);
                    transaction.Commit();
                }
            }

            connection.Close();

            // If no exception thrown, all entities saved
            return entities.Count;
        }

        /// <summary>
        /// Validates and saves the given entities via a bulk insert and returns the number of affected rows.
        /// Only support SQL Server.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entities">The records to save to the datastore.</param>
        /// <returns>The number of rows inserted.</returns>
        public async Task<int> BulkInsertAsync(TContext ctx, ICollection<TEntity> entities)
        {
            OnBeforeBulkInsert(entities);

            Type entityType = typeof (TEntity);
            var propInfos = entityType.GetProperties();
            var dataTable = ConvertToDataTable(entities, propInfos);

            var connection = GetConnection(ctx.Database.Connection.ConnectionString);

            using (var transaction = connection.BeginTransaction())
            {
                using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    SetupBulkCopy(sqlBulkCopy, entityType, propInfos);
                    await sqlBulkCopy.WriteToServerAsync(dataTable);
                    transaction.Commit();
                }
            }

            connection.Close();

            // If no exception thrown, all entities saved
            return entities.Count;
        }

        /// <summary>
        /// This method is called right before a bulk insert occurs.
        /// </summary>
        /// <param name="entities">The records to save to the datastore.</param>
        protected virtual void OnBeforeBulkInsert(ICollection<TEntity> entities)
        {
        }

        /// <summary>
        /// Returns an open SqlConnection.
        /// </summary>
        /// <returns>An open SqlConnection.</returns>
        /// <param name="connString">The connection string to the database.</param>
        private SqlConnection GetConnection(string connString)
        {
            var connection = new SqlConnection(connString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// Sets up the Bulk Copy.
        /// </summary>
        /// <param name="sqlBulkCopy">The object to setup.</param>
        /// <param name="entityType">The type of entity being inserted.</param>
        /// <param name="propInfos">The PropertyInfo classes of that entity type.</param>
        private void SetupBulkCopy(SqlBulkCopy sqlBulkCopy, Type entityType, IEnumerable<PropertyInfo> propInfos)
        {
            var table = entityType.GetCustomAttribute<TableAttribute>(false);
            if (table == null)
            {
                throw new NotImplementedException(
                    "Type '" + entityType.FullName + "' does not have the Table attribute assigned.");
            }
            sqlBulkCopy.DestinationTableName = string.Format(
                "[{0}].[{1}]",
                string.IsNullOrEmpty(table.Schema) ? "dbo" : table.Schema,
                table.Name);

            foreach (var prop in propInfos)
            {
                var column = prop.GetCustomAttribute<ColumnAttribute>(false);
                if (column == null)
                {
                    continue;
                }
                sqlBulkCopy.ColumnMappings.Add(prop.Name, column.Name);
            }
            if (sqlBulkCopy.ColumnMappings.Count == 0)
            {
                throw new NotImplementedException(
                    "Type '" + entityType.FullName + "' does not have any Column attributes assigned.");
            }
        }

        #endregion

        #region << DataTable Methods >>

        /// <summary>
        /// Converts the list of entities to a DataTable.
        /// </summary>
        /// <param name="list">The list to convert.</param>
        /// <param name="properties">The property infos of TEntity.</param>
        /// <returns>DataTable representing the list of entities.</returns>
        protected DataTable ConvertToDataTable(ICollection<TEntity> list, PropertyInfo[] properties)
        {
            DataTable table = CreateDataTable(properties);

            foreach (TEntity item in list)
            {
                FillData(properties, table, item);
            }
            return table;
        }

        /// <summary>
        /// Creates a new DataTable based upon the PropertyInfo objects.
        /// </summary>
        /// <param name="properties">The column information.</param>
        /// <returns>New, empty DataTable.</returns>
        private DataTable CreateDataTable(IEnumerable<PropertyInfo> properties)
        {
            DataTable table = new DataTable();
            foreach (PropertyInfo pi in properties)
            {
                DataColumn col = new DataColumn
                {
                    ColumnName = pi.Name,
                    DataType = Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType
                };
                table.Columns.Add(col);
            }
            return table;
        }

        /// <summary>
        /// Creates a new row in the DataTable based upon the given entity.
        /// </summary>
        /// <param name="properties">The column information.</param>
        /// <param name="table">The DataTable to put the data into.</param>
        /// <param name="item">The source object.</param>
        private void FillData(IEnumerable<PropertyInfo> properties, DataTable table, TEntity item)
        {
            DataRow row = table.NewRow();
            foreach (PropertyInfo pi in properties)
            {
                row[pi.Name] = pi.GetValue(item, null) ?? DBNull.Value;
            }
            table.Rows.Add(row);
        }

        #endregion

        #region << Save Methods >>

        /// <summary>
        /// Allows a repository to assign primary keys, if necessary.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entity">The new record about to be saved to the datastore.</param>
        public virtual void AssignPrimaryKey(TContext ctx, TEntity entity)
        {
        }

        /// <summary>
        /// This method can be overridden to make changes before an entity is attached to the context.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entity">The record about to be saved to the datastore.</param>
        public virtual void OnBeforeSaveChanges(TContext ctx, TEntity entity)
        {
        }

        #endregion

    }
}