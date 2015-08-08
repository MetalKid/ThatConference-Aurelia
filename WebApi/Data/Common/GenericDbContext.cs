#region << Usings >>

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ThatConference.Data.Common.Interfaces;

#endregion

namespace ThatConference.Data.Common
{
   /// <summary>
    /// This class implements common DbContext methods common to all.
    /// </summary>
    public abstract class GenericDbContext : DbContext, IDbContext
    {

        #region << Constants >>

        private const string DEFAULT_PROVIDER_NAME = "System.Data.SqlClient";
        private const string DEFAULT_PROVIDER_VERSION = "2008";

        #endregion

        #region << Properties >>

        /// <summary>
        /// Gets whether this context is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets whether the database this repository interacts with is case sensitive.
        /// </summary>
        public virtual bool IsDatabaseCaseSensitive
        {
            get { return false; }
        }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Connection string/Cached model constructor.
        /// </summary>
        protected GenericDbContext(string connString = null, DbCompiledModel model = null)
            : base(connString, model)
        {
            // Nothing to do
        }

        #endregion

        #region << Dispose Methods >>

        /// <summary>
        /// Stores that this object was disposed before continuing the base call.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }

        #endregion

        #region << Cache >>

        /// <summary>
        /// Returns a compiled version of the entity tree and caches the result in order to greatly improve performance.
        /// </summary>
        /// <param name="modelCache">The cache of this DbCompiledModel, if it exists.</param>
        /// <param name="lockObj">The object used to ensure cross-thread safety.</param>
        /// <param name="buildModelMappingCallback"></param>
        /// <param name="providerName">The name of the database provider namespace. (i.e. System.Data.SqlClient)</param>
        /// <param name="version">The version of the provider. (i.e. 2008)</param>
        /// <returns>DbCompiledModel.</returns>
        public static DbCompiledModel GetCompiledModel(
            ref DbCompiledModel modelCache, 
            ref object lockObj, 
            Action<DbModelBuilder> buildModelMappingCallback,
            string providerName = DEFAULT_PROVIDER_NAME,
            string version = DEFAULT_PROVIDER_VERSION)
        {
            if (modelCache != null)
            {
                return modelCache;
            }

            lock (lockObj)
            {
                // Another thread may have created it while waiting
                if (modelCache != null)
                {
                    return modelCache;
                }

                DbModelBuilder builder = new DbModelBuilder(DbModelBuilderVersion.Latest);
                buildModelMappingCallback(builder);
                modelCache = builder.Build(new DbProviderInfo(providerName, version)).Compile();
            }

            return modelCache;
        }

        #endregion

        #region << Interface Methods >>

        /// <summary>
        /// Returns the DbSet of the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <returns>DbSet of the given entity type.</returns>
        /// <remarks>Need to shadow (new) this non-virtual method because it does not return an interface.</remarks>
        public new IDbSet<TEntity> Set<TEntity>() 
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Sets the state of the given entity on the current context.
        /// </summary>
        /// <typeparam name="T">The type of Entity</typeparam>
        /// <param name="entity">The entity that the state will be set for.</param>
        /// <param name="state">The state to set the entity to. (i.e. New, Modified, Deleted, etc)</param>
        public void SetState<T>(T entity, EntityState state) 
            where T : class
        {
            Entry(entity).State = state;
        }

        #endregion

    }
}