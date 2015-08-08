#region << Usings >>

using System.Collections.Generic;
using System.Threading.Tasks;
using ThatConference.Common.Filters;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Validation;

#endregion

namespace ThatConference.Data.Common.Interfaces
{
    /// <summary>
    /// This interface defines all Get/Save methods allowed on Repository Manager classes.
    /// </summary>
    public interface IRepositoryManager
    {

        /// <summary>
        /// Returns whether this manager contains a repository of the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to check</typeparam>
        /// <param name="entity">The entity holding the data, if available.</param>
        /// <returns>True if this manager contains a repository for the given entity type; false otherwise.</returns>
        bool ContainsEntityType<TEntity>(TEntity entity = null) 
            where TEntity : class, IEntity;

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
        /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        ICollection<TEntity> Get<TEntity, TFilter>(TFilter filter) 
            where TEntity : class, IEntity
            where TFilter : FilterBase;

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
        /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        Task<ICollection<TEntity>> GetAsync<TEntity, TFilter>(TFilter filter) 
            where TEntity : class, IEntity
            where TFilter : FilterBase;

        /// <summary>
        /// Saves the given entities to the data store via a bulk insert, returning the number of affected rows.
        /// Only SQL Server supported.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        int BulkInsert<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : EntityBase;

        /// <summary>
        /// Saves the given entities to the data store via a bulk insert, returning the number of affected rows.
        /// Only SQL Server supported.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> BulkInsertAsync<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : EntityBase;

        /// <summary>
        /// Saves the given entities to the data store, returning the number of affected rows.
        /// </summary>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        int Save<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : class, IEntity;

        /// <summary>
        /// Saves the given entities to the data store, returning the number of affected rows.
        /// </summary>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        Task<int> SaveAsync<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : class, IEntity;

    }
}