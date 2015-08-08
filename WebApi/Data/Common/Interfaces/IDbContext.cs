#region << Usings >>

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace ThatConference.Data.Common.Interfaces
{
/// <summary>
    /// This interface defines all methods for a DbContext to implement
    /// </summary>
    public interface IDbContext : IDisposable
    {

        /// <summary>
        /// Gets whether the context is displosed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Gets whether the database is case sensitive.
        /// </summary>
        bool IsDatabaseCaseSensitive { get; }

        /// <summary>
        /// Gets the access to configuration options for the context.
        /// </summary>
        DbContextConfiguration Configuration { get; }

        /// <summary>
        /// Gets a Database instance for this context that allows for creation, deletion, and existence
        ///  checks for the underlying database.
        /// </summary>
        Database Database { get; }
       
        /// <summary>
        /// Returns the DbSet of the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <returns>DbSet of the given entity type.</returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves changes to the database and returns the number of affected rows.
        /// </summary>
        /// <returns>Number of affected rows.</returns>
        int SaveChanges();

        /// <summary>
        /// Saves changes to the database asynchronously and returns the number of affected rows.
        /// </summary>
        /// <param name="cancellationToken">
        /// A System.Threading.CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns>Number of affected rows.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sets the state of the given entity on the current context.
        /// </summary>
        /// <typeparam name="T">The type of Entity</typeparam>
        /// <param name="entity">The entity that the state will be set for.</param>
        /// <param name="state">The state to set the entity to. (i.e. New, Modified, Deleted, etc)</param>
        void SetState<T>(T entity, EntityState state) where T : class;

    }
}