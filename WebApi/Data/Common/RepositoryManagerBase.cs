#region << Usings >>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ThatConference.Common.Enums;
using ThatConference.Common.Exceptions;
using ThatConference.Common.Filters;
using ThatConference.Common.Helpers;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Validation;
using ThatConference.Data.Common.Interfaces;

#endregion

namespace ThatConference.Data.Common
{
    /// <summary>
    /// This class allows the management of all repositories for a sepcific DbContext and Scope.
    /// </summary>
    /// <typeparam name="TContext">The type of context used to connect to the database.</typeparam>
    /// <typeparam name="TScope">The type of IScope that holds data about the user.</typeparam>
    public abstract class RepositoryManagerBase<TContext, TScope> : IIoCGroup, IIoC
        where TContext : class, IDbContext
        where TScope : class, IScope
    {

        #region << Variables >>

        private InstanceManager _instanceManager;
        private readonly object[] _instances;

        #endregion

        #region << Properties >>

        /// <summary>
        /// Gets the information about the current user trying to query information from the database.
        /// </summary>
        protected TScope Scope { get; private set; }

        /// <summary>
        /// Gets the DbContext (Database) this repository interacts with.
        /// </summary>
        protected IDbContextFactory<TContext> ContextFactory { get; private set; }

        /// <summary>
        /// Gets the Instance Manager for the current class.
        /// </summary>
        /// <remarks>We must delay creating this because of property injection.</remarks>
        protected InstanceManager Instances
        {
            get
            {
                if (_instanceManager != null)
                {
                    return _instanceManager;
                }
                List<object> parms = new List<object> { this };
                if (_instances != null)
                {
                    parms.AddRange(_instances);
                }
                return _instanceManager ?? (_instanceManager = InstanceManager.Create(parms.ToArray()));
            }
        }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scope">The current user's information.</param>
        /// <param name="contextFactory">The current connection to the database.</param>
        /// <param name="instances">The instances to load.</param>
        protected RepositoryManagerBase(
            TScope scope,
            IDbContextFactory<TContext> contextFactory,
            params object[] instances)
        {
            Scope = scope;
            ContextFactory = contextFactory;
            _instances = instances;
        }

        #endregion

        #region << Check Methods >>

        /// <summary>
        /// Returns whether this manager contains a repository of the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to check</typeparam>
        /// <returns>True if this manager contains a repository for the given entity type; false otherwise.</returns>
        public bool ContainsEntityType<TEntity>(TEntity entity = null)
            where TEntity : class, IEntity
        {
            var type = entity != null ? entity.GetType() : typeof(TEntity);
            return Instances.GetInstance(typeof(IValidatableRepositoryInternal<,>), true, typeof(TContext), type) != null;
        }

        #endregion

        #region << Get Methods >>

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
        /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        public ICollection<TEntity> Get<TEntity, TFilter>(TFilter filter)
            where TEntity : class, IEntity
            where TFilter : FilterBase
        {
            using (var ctx = ContextFactory.Create())
            {
                var repo = GetISelectableRepositoryInternal<TEntity, TFilter>();
                return repo.Get(ctx, filter);
            }
        }

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
        /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        public async Task<ICollection<TEntity>> GetAsync<TEntity, TFilter>(TFilter filter)
            where TEntity : class, IEntity
            where TFilter : FilterBase
        {
            using (var ctx = ContextFactory.Create())
            {
                var repo = GetISelectableRepositoryInternal<TEntity, TFilter>();
                return await repo.GetAsync(ctx, filter);
            }
        }

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
        /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        public int GetCount<TEntity, TFilter>(TFilter filter)
            where TEntity : class, IEntity
            where TFilter : FilterBase
        {
            using (var ctx = ContextFactory.Create())
            {
                var repo = GetISelectableRepositoryInternal<TEntity, TFilter>();
                return repo.GetCount(ctx, filter);
            }
        }

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
        /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        public async Task<int> GetCountAsync<TEntity, TFilter>(TFilter filter)
            where TEntity : class, IEntity
            where TFilter : FilterBase
        {
            using (var ctx = ContextFactory.Create())
            {
                var repo = GetISelectableRepositoryInternal<TEntity, TFilter>();
                return await repo.GetCountAsync(ctx, filter);
            }
        }

        #endregion

        #region << Validation Methods >>

        /// <summary>
        /// Ensures that the given entities are valid and have not broken any database related rules.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entities">The list of entities to be validated.</param>
        protected void ValidateEntitiesNoChildren<TEntity>(
            IList<BrokenRule> brokenRules,
            TContext ctx,
            IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            ValidateEntitiesNoChildren(brokenRules, ctx, new List<IEntity>(), entities);
            if (brokenRules != null && brokenRules.Count > 0)
            {
                throw new BrokenRuleException(brokenRules);
            }
        }

        /// <summary>
        /// Ensures that the given entities are valid and have not broken any database related rules.
        /// Does not check any child/parent relationships.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="validatedEntities">The entities that have already been validated.</param>
        /// <param name="entities">The list of entities to be validated.</param>
        /// <remarks>This is mainly for Bulk Inserts.</remarks>
        private void ValidateEntitiesNoChildren<TEntity>(
            IList<BrokenRule> brokenRules,
            TContext ctx,
            ICollection<IEntity> validatedEntities,
            IEnumerable<TEntity> entities)
            where TEntity : EntityBase
        {
            Guard.IsNotNull(entities, "entities");
            var repo = GetValidatableRepositoryInternal<TEntity>();
            foreach (var entity in entities)
            {
                if (validatedEntities.Contains(entity))
                {
                    continue;
                }

                if (entity.DataState != DataStateEnum.Unchanged)
                {
                    repo.ValidateEntity(brokenRules, ctx, entity);
                }
                validatedEntities.Add(entity);
            }
        }

        #endregion

        #region << Save Methods >>

        /// <summary>
        /// Saves the given entities to the data store via a bulk insert, returning the number of affected rows.
        /// Only SQL Server supported.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        public int BulkInsert<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : EntityBase
        {
            using (var ctx = ContextFactory.Create())
            {
                ValidateEntitiesNoChildren(brokenRules, ctx, entities);
                dynamic repo = GetISaveableRepositoryInternal<TEntity>();
                return repo.BulkInsert(ctx, entities);
            }
        }

        /// <summary>
        /// Saves the given entities to the data store via a bulk insert, returning the number of affected rows.
        /// Only SQL Server supported.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<int> BulkInsertAsync<TEntity>(
            IList<BrokenRule> brokenRules,
            ICollection<TEntity> entities)
            where TEntity : EntityBase
        {
            using (var ctx = ContextFactory.Create())
            {
                ValidateEntitiesNoChildren(brokenRules, ctx, entities);
                dynamic repo = GetISaveableRepositoryInternal<TEntity>();
                return await repo.BulkInsertAsync(ctx, entities);
            }
        }

        /// <summary>
        /// Saves the given entities to the data store, returning the number of affected rows.
        /// </summary>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        public int Save<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : class, IEntity
        {
            Guard.IsNotNull(brokenRules, "brokenRules");
            Guard.IsNotNull(entities, "entities");

            if (entities.Count == 0)
            {
                return 0;
            }
            using (var ctx = ContextFactory.Create())
            {
                ValidateAndAttachEntities(brokenRules, ctx, entities.Cast<EntityBase>());
                return ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Saves the given entities to the data store, returning the number of affected rows.
        /// </summary>
        /// <param name="brokenRules">The list of business broken rules.</param>
        /// <param name="entities">The records to save.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<int> SaveAsync<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : class, IEntity
        {
            Guard.IsNotNull(brokenRules, "brokenRules");
            Guard.IsNotNull(entities, "entities");

            if (entities.Count == 0)
            {
                return 0;
            }
            using (var ctx = ContextFactory.Create())
            {
                ValidateAndAttachEntities(brokenRules, ctx, entities.Cast<EntityBase>());
                return await ctx.SaveChangesAsync();
            }
        }

        #endregion

        #region << ValidateAndAttachEntities Methods >>

        /// <summary>
        /// Validates and attaches the entities to the context.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entities">The entities to attach.</param>
        /// <returns>The number of rows affected, if any.</returns>
        protected void ValidateAndAttachEntities(IList<BrokenRule> brokenRules, TContext ctx, IEnumerable<EntityBase> entities)
        {
            var attached = new List<IEntity>();
            ValidateAndAttachEntities(brokenRules, ctx, entities, attached);
            if (brokenRules.Count > 0)
            {
                throw new BrokenRuleException(brokenRules);
            }
            foreach (var entity in attached)
            {
                // After the entity is validated and attached, we can reset the DataState to Unchanged
                entity.DataState = DataStateEnum.Unchanged;
            }
        }

        /// <summary>
        /// Validtes and attaches the entities to the context.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entities">The entities to attach.</param>
        /// <param name="attachedEntities">The entities that have already been attacahed to the context.</param>
        /// <returns>The number of rows affected, if any.</returns>
        private void ValidateAndAttachEntities(
            IList<BrokenRule> brokenRules,
            TContext ctx,
            IEnumerable<EntityBase> entities,
            ICollection<IEntity> attachedEntities)
        {
            if (entities == null)
            {
                return;
            }

            foreach (var entity in entities.OrderBy(a => a.DataState))
            {
                if (attachedEntities.Contains(entity))
                {
                    continue;
                }

                dynamic entityOfType = Convert.ChangeType(entity, entity.GetType());
                if (entity.DataState != DataStateEnum.Unchanged)
                {
                    var validateRepo = GetValidatableRepositoryInternal(entity);
                    validateRepo.ValidateEntity(brokenRules, ctx, entityOfType);
                }

                var saveRepo = GetISaveableRepositoryInternal(entity);
                saveRepo.OnBeforeSaveChanges(ctx, entityOfType);

                attachedEntities.Add(entity);
                switch (entity.DataState)
                {
                    case DataStateEnum.Unchanged:
                        ValidateAndAttachEntities(brokenRules, ctx, entity.GetChildEntities(), attachedEntities);
                        ctx.SetState(entity, EntityState.Unchanged);
                        break;
                    case DataStateEnum.New:
                        saveRepo.AssignPrimaryKey(ctx, entityOfType);

                        ctx.SetState(entity, EntityState.Added);
                        ValidateAndAttachEntities(brokenRules, ctx, entity.GetChildEntities(), attachedEntities);
                        break;
                    case DataStateEnum.Modified:
                        ValidateAndAttachEntities(brokenRules, ctx, entity.GetChildEntities(), attachedEntities);
                        ctx.SetState(entity, EntityState.Modified);
                        break;
                    case DataStateEnum.Deleted:
                        ValidateAndAttachEntities(brokenRules, ctx, entity.GetChildEntities(), attachedEntities);
                        ctx.SetState(entity, EntityState.Deleted);
                        break;
                }
            }
        }

        #endregion

        #region << Helper Methods >>

        /// <summary>
        /// Returns the ISelectableRepositoryInternal instance based on the given types.
        /// </summary>
        /// <typeparam name="TEntity">The type of parent being returned.</typeparam>
        /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
        /// <returns>ISelectableRepository instance based on the given types.</returns>
        private dynamic GetISelectableRepositoryInternal<TEntity, TFilter>()
        {
            return Instances.GetInstance(
                typeof(ISelectableRepositoryInternal<,,>),
                true,
                typeof(TContext),
                typeof(TEntity),
                typeof(TFilter));
        }

        /// <summary>
        /// Returns the repository of the given entity type.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entity">The entity to get the repository for.</param>
        /// <returns>Instance of IValidatableRepositoryInternal.</returns>
        private dynamic GetValidatableRepositoryInternal<T>(T entity = null)
            where T : EntityBase
        {
            return Instances.GetInstance(
                typeof(IValidatableRepositoryInternal<,>),
                true,
                typeof(TContext),
                entity == null ? typeof(T) : entity.GetType());
        }

        /// <summary>
        /// Returns the repository of the given entity type.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entity">The entity to get the repository for.</param>
        /// <returns>Instance of ISaveableRepositoryInternal.</returns>
        private dynamic GetISaveableRepositoryInternal<T>(T entity = null)
            where T : EntityBase
        {
            return Instances.GetInstance(
                typeof(ISaveableRepositoryInternal<,>),
                true,
                typeof(TContext),
                entity == null ? typeof(T) : entity.GetType());
        }

        #endregion

    }
}
