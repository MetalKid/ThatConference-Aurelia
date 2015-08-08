#region << Usings >>

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace ThatConference.Data.Common.Interfaces
{
   /// <summary>
    /// This interface defines methods for saving entities to the database between repositories.
    /// </summary>
    /// <typeparam name="TContext">The connection to the database.</typeparam>
    /// <typeparam name="TEntity">The type of entity to save (table).</typeparam>
    internal interface ISaveableRepositoryInternal<in TContext, TEntity>
        where TEntity : EntityBase
    {

        /// <summary>
        /// Allows a repository to assign primary keys, if necessary.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entity">The new record about to be saved to the datastore.</param>
        void AssignPrimaryKey(TContext ctx, TEntity entity);

        /// <summary>
        /// This method can be overridden to make changes before an entity is attached to the context.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entity">The record about to be saved to the datastore.</param>
        void OnBeforeSaveChanges(TContext ctx, TEntity entity);

        /// <summary>
        /// Validates and saves the given entities via a bulk insert and returns the number of affected rows.
        /// Only support SQL Server.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entities">The records to save to the datastore.</param>
        /// <returns>The number of rows inserted.</returns>
        int BulkInsert(TContext ctx, ICollection<TEntity> entities);

        /// <summary>
        /// Validates and saves the given entities via a bulk insert and returns the number of affected rows.
        /// Only support SQL Server.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entities">The records to save to the datastore.</param>
        /// <returns>The number of rows inserted.</returns>
        Task<int> BulkInsertAsync(TContext ctx, ICollection<TEntity> entities);

    }
}