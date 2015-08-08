#region << Usings >>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ClassyMapper;
using ThatConference.Common;
using ThatConference.Common.Enums;
using ThatConference.Common.Filters;
using ThatConference.Common.Helpers;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Validation;
using ThatConference.Data.Common;
using ThatConference.Data.Common.Helpers;
using ThatConference.Services.Common.Helpers;
using ThatConference.Services.Common.Interfaces;

#endregion

namespace ThatConference.Services.Common
{
    /// <summary>
    /// This class is used by all services that run common code.
    /// </summary>
    /// <typeparam name="TScope">
    /// The type of Scope that stores information about the currently logged in user.
    /// </typeparam>
    /// <typeparam name="TScopeKey">A value that allows looking up the Scope or is the Scope value itself.</typeparam>
    public abstract class ServiceBase<TScope, TScopeKey> : IIoC 
        where TScope : class, IScope
        where TScopeKey : class, IScopeKey
    {

        #region << Variables >>

        private readonly TScopeKey _scopeKey;
        private TScope _scope;

        #endregion

        #region << Properties >>

        /// <summary>
        /// Gets the manager of the instances that were IoC injected into this service.
        /// </summary>
        public InstanceManager Instances { get; private set; }

        /// <summary>
        /// Gets the user's scope for the current call.
        /// </summary>
        public TScope Scope
        {
            get { return _scope ?? (_scope = GetScope(_scopeKey)); }
        }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scopeKey">The key to get the user's scope.</param>
        /// <param name="instances">The instances to load.</param>
        protected ServiceBase(TScopeKey scopeKey, params object[] instances)
        {
            _scopeKey = scopeKey;
            List<object> parms = new List<object> { this };
            if (instances != null)
            {
                parms.AddRange(instances);
            }
            Instances = InstanceManager.Create(parms.ToArray());
        }

        #endregion

        #region << Code Runner Methods >>

        /// <summary>
        /// Runs code with common exception/replay log handling and forces IResult to be returned by 
        /// all Service Layer methods.
        /// </summary>
        /// <typeparam name="TResult">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="parameters">The parameters used to call this method.</param>
        /// <returns>The result.</returns>
        protected TResult Run<TResult>(Action<TResult> code, params object[] parameters)
            where TResult : IResult, new()
        {
            TResult result = new TResult();

            Lazy<string> methodName = GetMethodName();

            try
            {
                code(result);
            }
            catch (Exception ex)
            {
                result.HandleException(ex);
                HandleRunException(result, ex, methodName.Value, parameters);
            }

            return result;
        }

        /// <summary>
        /// Runs code asynchronously with common exception/replay log handling and forces IResult to be returned by 
        /// all Service Layer methods.
        /// </summary>
        /// <typeparam name="TResult">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="parameters">The parameters used to call this method.</param>
        /// <returns>The result.</returns>
        protected async Task<TResult> RunAsync<TResult>(Func<TResult, Task> code, params object[] parameters) 
            where TResult : IResult, new()
        {
            TResult result = new TResult();

            Lazy<string> methodName = GetMethodName();

            try
            {
                await code(result);
            }
            catch (Exception ex)
            {
                result.HandleException(ex);
                HandleRunException(result, ex, methodName.Value, parameters);
            }

            return result;
        }

        #endregion

        #region << Exception Methods >>

        /// <summary>
        /// This method is called when a method surrounded by a Run() call throws an exception.
        /// </summary>
        /// <param name="result">Result of the currently running process.</param>
        /// <param name="ex">The exception occurred.</param>
        /// <param name="methodName">The name of the method that was called.</param>
        /// <param name="parameters">The parameters used to call this method.</param>
        protected virtual void HandleRunException(
            IResult result,
            Exception ex,
            string methodName,
            params object[] parameters)
        {
            // Do nothing
        }

        /// <summary>
        /// Returns a lazy reference to get the method name should an exception occur.
        /// </summary>
        /// <returns></returns>
        private Lazy<string> GetMethodName()
        {
            return new Lazy<string>(
                () =>
                {
                    var stackTrace = new StackTrace();
                    var stackFrames = stackTrace.GetFrames();
                    if (stackFrames != null)
                    {
                        int i = 0;
                        while (!stackFrames[i].GetMethod().Name.StartsWith("Run"))
                        {
                            i++;
                        }
                        while (stackFrames[i].GetMethod().Name.StartsWith("Run"))
                        {
                            i++;
                        }
                        var method = stackFrames[i].GetMethod();
                        return method.Name;
                    }
                    return null;
                });
        }

        #endregion

        #region << Scope Methods >>

        /// <summary>
        /// Returns the scope to use for this call given the key value.
        /// </summary>
        /// <param name="key">The key to get back the Scope object.</param>
        /// <returns>IScope.</returns>
        public abstract TScope GetScope(TScopeKey key);

        #endregion

        #region << Get Methods >>

        /// <summary>
        /// Returns entities that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to return.</typeparam>
        /// <typeparam name="TFilter">The filter type to take.</typeparam>
        /// <param name="filter">The data used to filter the results.</param>
        /// <returns>List of entities that match the given filter criteria.</returns>
        protected ICollection<TEntity> GetEntities<TEntity, TFilter>(TFilter filter)
            where TEntity : EntityBase
            where TFilter : FilterBase
        {
            var response = Run<DataResult<ICollection<TEntity>>>(
                result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = manager.Get<TEntity, TFilter>(filter);
                });

            if (response.HandledExceptions.Count > 0)
            {
                throw response.HandledExceptions[0];
            }

            return response.Data;
        }

        /// <summary>
        /// Returns entities that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to return.</typeparam>
        /// <typeparam name="TFilter">The filter type to take.</typeparam>
        /// <param name="filter">The data used to filter the results.</param>
        /// <returns>List of entities that match the given filter criteria.</returns>
        protected async Task<ICollection<TEntity>> GetEntitiesAsync<TEntity, TFilter>(TFilter filter)
            where TEntity : EntityBase
            where TFilter : FilterBase
        {
            var response = await RunAsync<DataResult<ICollection<TEntity>>>(
                async result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = await manager.GetAsync<TEntity, TFilter>(filter);
                });

            if (response.HandledExceptions.Count > 0)
            {
                throw response.HandledExceptions[0];
            }

            return response.Data;
        }

        /// <summary>
        /// Returns an entity that matches the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to return.</typeparam>
        /// <typeparam name="TFilter">The filter type to take.</typeparam>
        /// <param name="filter">The data used to filter the results.</param>
        /// <returns>An entity that matches the given filter criteria.</returns>
        protected TEntity GetEntity<TEntity, TFilter>(TFilter filter)
            where TEntity : EntityBase 
            where TFilter : FilterBase
        {
            var response = Run<DataResult<TEntity>>(
                result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = manager.Get<TEntity, TFilter>(filter).SingleOrDefault();
                });

            if (response.HandledExceptions.Count > 0)
            {
                throw response.HandledExceptions[0];
            }

            return response.Data;
        }


        /// <summary>
        /// Returns an entity that matches the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to return.</typeparam>
        /// <typeparam name="TFilter">The filter type to take.</typeparam>
        /// <param name="filter">The data used to filter the results.</param>
        /// <returns>An entity that matches the given filter criteria.</returns>
        protected async Task<TEntity> GetEntityAsync<TEntity, TFilter>(TFilter filter)
            where TEntity : EntityBase
            where TFilter : FilterBase
        {
            var response = await RunAsync<DataResult<TEntity>>(
                async result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = (await manager.GetAsync<TEntity, TFilter>(filter)).SingleOrDefault();
                });

            if (response.HandledExceptions.Count > 0)
            {
                throw response.HandledExceptions[0];
            }

            return response.Data;
        }

        /// <summary>
        /// Returns list of View Models that match the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to get.</typeparam>
        /// <typeparam name="TFilter">The filter type to take.</typeparam>
        /// <typeparam name="TDto">The Dto type to return.</typeparam>
        /// <param name="filter">The data used to filter the results.</param>
        /// <returns>A list of Dtos that match the given filter criteria.</returns>
        protected ICollection<TDto> GetDtos<TEntity, TFilter, TDto>(TFilter filter)
            where TEntity : EntityBase 
            where TFilter : FilterBase 
            where TDto : DataTransferObjectBase, new()
        {
            var response = Run<DataResult<ICollection<TDto>>>(
                result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = ClassyMap.New().MapToList<TDto, TEntity>(manager.Get<TEntity, TFilter>(filter));
                });

            if (response.HandledExceptions.Count > 0)
            {
                throw response.HandledExceptions[0];
            }

            return response.Data;
        }

        /// <summary>
        /// Returns a View Model that matches the given filter criteria.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to get.</typeparam>
        /// <typeparam name="TFilter">The filter type to take.</typeparam>
        /// <typeparam name="TDto">The Dto type to return.</typeparam>
        /// <param name="filter">The data used to filter the results.</param>
        /// <returns>A Dto that matches the given filter criteria.</returns>
        protected TDto GetDto<TEntity, TFilter, TDto>(TFilter filter)
            where TEntity : EntityBase where TFilter : FilterBase where TDto : DataTransferObjectBase, new()
        {
            var response = Run<DataResult<TDto>>(
                result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data =
                        ClassyMap.New()
                            .MapToList<TDto, TEntity>(manager.Get<TEntity, TFilter>(filter))
                            .SingleOrDefault();
                });

            if (response.HandledExceptions.Count > 0)
            {
                throw response.HandledExceptions[0];
            }

            return response.Data;
        }

        #endregion

        #region << Save Methods >>

        /// <summary>
        /// Saves all given entities under a single transaction.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="entities">The entities being saved.</param>
        /// <param name="contextSpecificValidation">
        /// Any business rules that need to be run specific to the caller.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        protected int SaveEnsureAll<TEntity>(
            ICollection<TEntity> entities,
            Action<IList<BrokenRule>, ICollection<TEntity>> contextSpecificValidation = null) 
            where TEntity : EntityBase
        {
            int result;
            using (TransactionScope ts = new TransactionScope())
            {
                result = SaveEntities(entities, contextSpecificValidation);
                if (result >= entities.Count)
                {
                    ts.Complete();
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// Saves all given entities under a single transaction.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="entities">The entities being saved.</param>
        /// <param name="contextSpecificValidation">
        /// Any business rules that need to be run specific to the caller.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        protected async Task<int> SaveEnsureAllAsync<TEntity>(
            ICollection<TEntity> entities,
            Action<IList<BrokenRule>, ICollection<TEntity>> contextSpecificValidation = null)
            where TEntity : EntityBase
        {
            int result;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await SaveEntitiesAsync(entities, contextSpecificValidation);
                if (result >= entities.Count)
                {
                    ts.Complete();
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// Saves the entities to the datastore.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="entities">The entities being saved.</param>
        /// <param name="contextSpecificValidation">
        /// Any business rules that need to be run specific to the caller.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        protected int SaveEntities<TEntity>(
            ICollection<TEntity> entities,
            Action<IList<BrokenRule>, ICollection<TEntity>> contextSpecificValidation = null) 
            where TEntity : EntityBase
        {
            var response = Run<DataResult<int>>(
                result =>
                {
                    Guard.IsNotNull(entities, "entities");

                    IList<BrokenRule> brokenRules = CheckForBrokenRules(entities, contextSpecificValidation);
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = manager.Save(brokenRules, entities);
                },
                null,
                entities);

            return HandleResponse(response);
        }

        /// <summary>
        /// Saves the entities to the datastore.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="entities">The entities being saved.</param>
        /// <param name="contextSpecificValidation">
        /// Any business rules that need to be run specific to the caller.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        protected async Task<int> SaveEntitiesAsync<TEntity>(
            ICollection<TEntity> entities,
            Action<IList<BrokenRule>, ICollection<TEntity>> contextSpecificValidation = null)
            where TEntity : EntityBase
        {
            var response = await RunAsync<DataResult<int>>(
                async result =>
                {
                    Guard.IsNotNull(entities, "entities");

                    IList<BrokenRule> brokenRules = CheckForBrokenRules(entities, contextSpecificValidation);
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = await manager.SaveAsync(brokenRules, entities);
                },
                null,
                entities);

            return HandleResponse(response);
        }

        /// <summary>
        /// Saves the entities to the datastore via a bulk insert request to speed things up. 
        /// Only works with SQL Server.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="entities">The entities being saved.</param>
        /// <param name="contextSpecificValidation">
        /// Any business rules that need to be run specific to the caller.
        /// </param>
        /// <returns>The number of rows inserted.</returns>
        protected int BulkInsertEntities<TEntity>(
            ICollection<TEntity> entities,
            Action<IList<BrokenRule>, ICollection<TEntity>> contextSpecificValidation = null) 
            where TEntity : EntityBase
        {
            var response = Run<DataResult<int>>(
                result =>
                {
                    Guard.IsNotNull(entities, "entities");

                    IList<BrokenRule> brokenRules = CheckForBrokenRules(entities, contextSpecificValidation);
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = manager.BulkInsert(brokenRules, entities);
                },
                null,
                entities);

            return HandleResponse(response);
        }


        /// <summary>
        /// Saves the entities to the datastore via a bulk insert request to speed things up. 
        /// Only works with SQL Server.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="entities">The entities being saved.</param>
        /// <param name="contextSpecificValidation">
        /// Any business rules that need to be run specific to the caller.
        /// </param>
        /// <returns>The number of rows inserted.</returns>
        protected async Task<int> BulkInsertEntitiesAsync<TEntity>(
            ICollection<TEntity> entities,
            Action<IList<BrokenRule>, ICollection<TEntity>> contextSpecificValidation = null)
            where TEntity : EntityBase
        {
            var response = await RunAsync<DataResult<int>>(
                async result =>
                {
                    Guard.IsNotNull(entities, "entities");

                    IList<BrokenRule> brokenRules = CheckForBrokenRules(entities, contextSpecificValidation);
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);
                    result.Data = await manager.BulkInsertAsync(brokenRules, entities);
                },
                null,
                entities);

            return HandleResponse(response);
        }

        /// <summary>
        /// Throws any exceptions that happened or returns the number of rows affected.
        /// </summary>
        /// <param name="response">The response object to insepect.</param>
        /// <returns>Number of rows affected.</returns>
        private int HandleResponse(IDataResult<int> response)
        {
            if (response.HandledExceptions.Count > 0)
            {
                // We want an exception to be thrown so the errors get saved
                throw response.HandledExceptions[0];
            }

            return response.Data;
        }

        /// <summary>
        /// Returns the business rule validation instance of the Entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to return.</typeparam>
        /// <returns>Repository instance.</returns>
        protected IList<IBusinessRuleValidation<TEntity, TScope>> GetBusinessRuleValidations<TEntity>()
            where TEntity : EntityBase
        {
            return Instances.GetInstances<IBusinessRuleValidation<TEntity, TScope>>();
        }

        #endregion

        #region << Vaidation Methods >>

        /// <summary>
        /// Applies validation and returns any broken rules.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities being saved.</typeparam>
        /// <param name="entities">The entities being saved.</param>
        /// <param name="contextSpecificValidation">
        /// Any business rules that need to be run specific to the caller.
        /// </param>
        /// <returns>List of BrokenRules, if any.</returns>
        private IList<BrokenRule> CheckForBrokenRules<TEntity>(
            ICollection<TEntity> entities,
            Action<IList<BrokenRule>, ICollection<TEntity>> contextSpecificValidation)
            where TEntity : EntityBase
        {
            IList<BrokenRule> brokenRules = new List<BrokenRule>();
            if (contextSpecificValidation != null)
            {
                contextSpecificValidation(brokenRules, entities);
            }
            ApplyRules(brokenRules, entities);
            return brokenRules;
        }

        /// <summary>
        /// Ensures that the given entities are valid and have not broken any business related rules.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="entities">The list of entities to be validated.</param>
        public void ApplyRules<TEntity>(IList<BrokenRule> brokenRules, ICollection<TEntity> entities)
            where TEntity : EntityBase
        {
            ApplyRules(brokenRules, new List<IEntity>(), entities);
        }

        /// <summary>
        /// Ensures that the given entities are valid and have not broken any business related rules.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="validatedEntities">The list of entities that were already validated up to this point.</param>
        /// <param name="entities">The list of entities to be validated.</param>
        public void ApplyRules<TEntity>(
            IList<BrokenRule> brokenRules,
            ICollection<IEntity> validatedEntities,
            ICollection<TEntity> entities) 
            where TEntity : EntityBase
        {
            Guard.IsNotNull(entities, "entities");

            bool needsChangeType = typeof (TEntity) == typeof (EntityBase);
            foreach (var entity in entities)
            {
                if (validatedEntities.Contains(entity))
                {
                    continue;
                }

                if (entity.DataState != DataStateEnum.Unchanged)
                {
                    var businessRules = GetBusinessRuleValidations(entity.GetType());

                    if (businessRules != null)
                    {
                        foreach (dynamic businessRule in businessRules)
                        {
                            if (needsChangeType)
                            {
                                dynamic entityOfType = Convert.ChangeType(entity, entity.GetType());
                                businessRule.ApplyRules(brokenRules, Scope, entityOfType);
                            }
                            else
                            {
                                businessRule.ApplyRules(brokenRules, Scope, entity);
                            }
                        }
                    }
                }
                validatedEntities.Add(entity);
                ApplyRules(brokenRules, validatedEntities, entity.GetChildEntities());
            }
        }

        /// <summary>
        /// Returns all Business Rule Validation classes to run for the given entity type, if any.
        /// </summary>
        /// <param name="entity">The type of entity to validate.</param>
        /// <returns>All Business Rule Validation classes to run for the given entity type, if any.</returns>
        private IEnumerable<dynamic> GetBusinessRuleValidations(Type entity)
        {
            return Instances.GetInstances(typeof (IBusinessRuleValidation<,>), false, entity, typeof (TScope));
        }

        #endregion

    }

    /// <summary>
    /// This class is used by a Service that is dedicated to one specific Entity. 
    /// </summary>
    /// <typeparam name="TDto">The type of Dto to return for Gets.</typeparam>
    /// <typeparam name="TEntity">The type of Entity being returned from the Repository.</typeparam>
    /// <typeparam name="TFilter">The type of Filter used by the Repository for Gets</typeparam>
    /// <typeparam name="TScope">The type of Scope that stores information about the currently logged in user.</typeparam>
    /// <typeparam name="TScopeKey">A value that allows looking up the Scope or is the Scope value itself.</typeparam>
    public abstract class ServiceBase<TDto, TEntity, TFilter, TScope, TScopeKey> : ServiceBase<TScope, TScopeKey>
        where TDto : SaveableDataTransferObjectBase, new()
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
        where TScope : class, IScope
        where TScopeKey : class, IScopeKey
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scopeKey">The key to get the user's scope.</param>
        /// <param name="instances">The instances to load.</param>
        protected ServiceBase(TScopeKey scopeKey, params object[] instances) : base(scopeKey, instances)
        {
        }

        #endregion

        #region << Select Methods >>

        /// <summary>
        /// Gives the ability to intercept the filter being used by the Get call and make changes.
        /// </summary>
        /// <param name="filter">The current filter data.</param>
        /// <returns>The filter data the Get should use.</returns>
        protected virtual TFilter OnPreGet(TFilter filter)
        {
            return filter;
        }

        /// <summary>
        /// Gives the abilitiy to perform code on the entities that are returned by the repository.
        /// </summary>
        /// <param name="entities">The entities returned from the Get call.</param>
        /// <returns>The entities to be mapped to Dtos.</returns>
        protected virtual ICollection<TEntity> OnPostGet(ICollection<TEntity> entities)
        {
            return entities;
        }

        /// <summary>
        /// Gives the ability to perform code on the Dtos that were mapped from the entities.
        /// </summary>
        /// <param name="dtos">The Dtos that were mapped from the entities.</param>
        /// <returns>The Dtos being returned to the caller.</returns>
        protected virtual ICollection<TDto> OnPostGetMap(ICollection<TDto> dtos)
        {
            return dtos;
        }

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        public IDataResult<ICollection<TDto>> Get(TFilter filter = null)
        {
            return Run<DataResult<ICollection<TDto>>>(
                result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);

                    filter = OnPreGet(filter);
                    var entities = manager.Get<TEntity, TFilter>(filter);
                    entities = OnPostGet(entities);

                    ICollection<TDto> dtos = ClassyMap.New().MapToList<TDto, TEntity>(entities);
                    dtos = OnPostGetMap(dtos);
                    result.Data = dtos;
                },
                filter);
        }

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        public async Task<IDataResult<ICollection<TDto>>> GetAsync(TFilter filter = null)
        {
            return await RunAsync<DataResult<ICollection<TDto>>>(
                async result =>
                {
                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(Instances);

                    filter = OnPreGet(filter);
                    var entities = await manager.GetAsync<TEntity, TFilter>(filter);
                    entities = OnPostGet(entities);

                    ICollection<TDto> dtos = ClassyMap.New().MapToList<TDto, TEntity>(entities);
                    dtos = OnPostGetMap(dtos);
                    result.Data = dtos;
                },
                filter);
        }

        #endregion

        #region << Filter Methods >>

        /// <summary>
        /// Returns a new filter with primary key values assigned and possibly a timestamp.
        /// </summary>
        /// <typeparam name="TFromDto">The type of dto the primary key values are coming from.</typeparam>
        /// <param name="dto">The dto to get the primary key values from.</param>
        /// <returns>The new filter.</returns>
        private TFilter GetNewFilter<TFromDto>(TFromDto dto) 
            where TFromDto : class, IDataTransferObject, new()
        {
            var filter = new TFilter();
            var timestampDto = dto as ITimestamp;
            if (timestampDto != null)
            {
                filter.Timestamp = timestampDto.Timestamp;
            }

            var primaryKeyFilter = Instances.GetInstance(
                typeof (IAssignFilterDataForSave<,>),
                true,
                typeof (TFilter),
                typeof (TFromDto));

            primaryKeyFilter.AssignFilterDataForSave(filter, dto);

            return filter;
        }

        #endregion

        #region << Generic Entity Save Methods >>

        /// <summary>
        /// Saves the given dtos in the datastore, returning the number of affected rows.
        /// </summary>
        /// <typeparam name="TFromDto">The type of Dto being mapped from.</typeparam>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows and the entities that were created from those Dtos.</returns>
        public IDataResult<ICollection<TDto>> Save<TFromDto>(ICollection<TFromDto> dtos)
            where TFromDto : SaveableDataTransferObjectBase, new()
        {
            return Run<DataResult<ICollection<TDto>>>(
                result =>
                {
                    var entities = GetAndMapEntities(dtos);
                    result.RowsAffected += SaveEntities(entities);
                    AssignResults(result, entities);
                },
                null,
                dtos);
        }

        /// <summary>
        /// Saves the given dtos in the datastore, returning the number of affected rows.
        /// </summary>
        /// <typeparam name="TFromDto">The type of Dto being mapped from.</typeparam>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows and the entities that were created from those Dtos.</returns>
        public async Task<IDataResult<ICollection<TDto>>> SaveAsync<TFromDto>(ICollection<TFromDto> dtos)
            where TFromDto : SaveableDataTransferObjectBase, new()
        {
            return await RunAsync<DataResult<ICollection<TDto>>>(
                async result =>
                {
                    var entities = await GetAndMapEntitiesAsync(dtos);
                    result.RowsAffected += await SaveEntitiesAsync(entities);
                    AssignResults(result, entities);
                },
                null,
                dtos);
        }

        /// <summary>
        /// Creates the given dtos in the datastore via a Bulk Insert approach (bypasses database saftey checks).
        /// Only SQL Server is supported.
        /// </summary>
        /// <typeparam name="TFromDto">The type of Dto being mapped from.</typeparam>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows and the entities that were created from those Dtos.</returns>
        protected IResult BulkInsert<TFromDto>(ICollection<TFromDto> dtos)
            where TFromDto : SaveableDataTransferObjectBase, new()
        {
            return Run<Result>(
                result =>
                {
                    var entities = GetAndMapEntities(dtos);
                    result.RowsAffected += BulkInsertEntities(entities);
                },
                null,
                dtos);
        }

        /// <summary>
        /// Creates the given dtos in the datastore via a Bulk Insert approach (bypasses database saftey checks).
        /// Only SQL Server is supported.
        /// </summary>
        /// <typeparam name="TFromDto">The type of Dto being mapped from.</typeparam>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows and the entities that were created from those Dtos.</returns>
        protected async Task<IResult> BulkInsertAsync<TFromDto>(ICollection<TFromDto> dtos)
            where TFromDto : SaveableDataTransferObjectBase, new()
        {
            return await RunAsync<Result>(
                async result =>
                {
                    var entities = await GetAndMapEntitiesAsync(dtos);
                    result.RowsAffected += await BulkInsertEntitiesAsync(entities);
                },
                null,
                dtos);
        }

        /// <summary>
        /// Maps the entities to Dtos and assigns those Dtos to the result along with any Timestamps.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="entities"></param>
        private void AssignResults<TFromDto>(DataResult<ICollection<TFromDto>> result, ICollection<TEntity> entities)
            where TFromDto : class, IDataTransferObject, new()
        {
            result.Data = ClassyMap.New().MapToList<TFromDto, TEntity>(entities);
            result.Timestamps =
                entities.Select(
                    a => a.Timestamp == null ? string.Empty : Convert.ToBase64String(a.Timestamp)).ToList();
        }

        /// <summary>
        /// Converts the given Dtos to Entities.
        /// </summary>
        /// <typeparam name="TFromDto">The type of Dto being mapped from.</typeparam>
        /// <param name="dtos">The data to save.</param>
        /// <returns>List of entities.</returns>
        private IList<TEntity> GetAndMapEntities<TFromDto>(IEnumerable<TFromDto> dtos)
            where TFromDto : SaveableDataTransferObjectBase, new()
        {
            Guard.IsNotNull(dtos, "dtos");

            Action<TEntity, TFromDto> map = GetCustomMap<TFromDto>();

            var result = new List<TEntity>();
            foreach (var dto in dtos)
            {
                TFromDto localDto = dto;
                var filter = dto.DataState == DataStateEnum.New ? null : GetNewFilter(localDto);
                TEntity entity = DtoToEntityHelper.GetAndMap(
                    Instances,
                    localDto,
                    dto.DataState,
                    Scope,
                    a => filter,
                    map,
                    true);
                if (entity != null)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the given Dtos to Entities.
        /// </summary>
        /// <typeparam name="TFromDto">The type of Dto being mapped from.</typeparam>
        /// <param name="dtos">The data to save.</param>
        /// <returns>List of entities.</returns>
        private async Task<IList<TEntity>> GetAndMapEntitiesAsync<TFromDto>(IEnumerable<TFromDto> dtos)
            where TFromDto : SaveableDataTransferObjectBase, new()
        {
            Guard.IsNotNull(dtos, "dtos");

            Action<TEntity, TFromDto> map = GetCustomMap<TFromDto>();

            var result = new List<TEntity>();
            foreach (var dto in dtos)
            {
                TFromDto localDto = dto;
                var filter = dto.DataState == DataStateEnum.New ? null : GetNewFilter(localDto);
                TEntity entity = await DtoToEntityHelper.GetAndMapAsync(
                    Instances,
                    localDto,
                    dto.DataState,
                    Scope,
                    a => filter,
                    map,
                    true);
                if (entity != null)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the custom map anonymous method, if one exists.
        /// </summary>
        /// <typeparam name="TFromDto">The type of Dto being mapped from.</typeparam>
        /// <returns>An anonymous method to do custom mapping, if one exists.</returns>
        private Action<TEntity, TFromDto> GetCustomMap<TFromDto>()
            where TFromDto : SaveableDataTransferObjectBase, new()
        {
            dynamic customMap = Instances.GetInstance(
             typeof(ICustomEntityMap<,>),
             false,
             typeof(TEntity),
             typeof(TFromDto));

            Action<TEntity, TFromDto> map = null;

            if (customMap != null)
            {
                map = (to, from) => customMap.CustomMap(to, from);
            }
            return map;
        }

        #endregion

        #region << Dto Specific Entity Save Methods >>

        /// <summary>
        /// Creates the given dtos in the datastore, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows.</returns>
        public IResult BulkInsert(ICollection<TDto> dtos)
        {
            return BulkInsert<TDto>(dtos);
        }

        /// <summary>
        /// Creates the given dtos in the datastore, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<IResult> BulkInsertAsync(ICollection<TDto> dtos)
        {
            return await BulkInsertAsync<TDto>(dtos);
        }
        /// <summary>
        /// Creates the given dtos in the datastore, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows.</returns>
        public IDataResult<ICollection<TDto>> Save(ICollection<TDto> dtos)
        {
            return Save<TDto>(dtos);
        }

        /// <summary>
        /// Creates the given dtos in the datastore, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The number of affected rows.</returns>
        public async Task<IDataResult<ICollection<TDto>>> SaveAsync(ICollection<TDto> dtos)
        {
            return await SaveAsync<TDto>(dtos);
        }

        #endregion

    }
}