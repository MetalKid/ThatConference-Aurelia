#region << Usings >>

using System;
using System.Linq;
using System.Threading.Tasks;
using ClassyMapper;
using ThatConference.Common.Enums;
using ThatConference.Common.Filters;
using ThatConference.Common.Helpers;
using ThatConference.Common.Interfaces;
using ThatConference.Data.Common.Helpers;

#endregion

namespace ThatConference.Services.Common.Helpers
{
    /// <summary>
    /// This class aids in copying data from Dtos to Entities.
    /// </summary>
    public static class DtoToEntityHelper
    {

        #region << Map And Save Methods >>

        /// <summary>
        /// Gets or Creates the entity and maps the dto to the entity.
        /// </summary>
        /// <typeparam name="TDto">The type of DataTransferObject.</typeparam>
        /// <typeparam name="TEntity">The type of Entity.</typeparam>
        /// <typeparam name="TFilter">The type of Filter.</typeparam>
        /// <typeparam name="TScope">The type of user Scope data.</typeparam>
        /// <param name="instances">The instances of repositories.</param>
        /// <param name="dto">The data that needs to be saved to the datastore.</param>
        /// <param name="state">The type of action being performed (only New, Modified do anything).</param>
        /// <param name="scope">The configuration information about the currently logged in user.</param>
        /// <param name="getFilter">
        /// Anonymous function that returns a Filter with the Primary Key assigned to get 
        /// the correct entity (null if New Entity).
        /// </param>
        /// <param name="customMap">Anonymous function called for any custom mapping a service may need to do.</param>
        /// <param name="ignoreLists">True if sub lists should not be mapped from dto to entity; false otherwise.</param>
        /// <returns>The number of rows affected.</returns>
        public static TEntity GetAndMap<TDto, TEntity, TFilter, TScope>(
            InstanceManager instances,
            TDto dto, 
            DataStateEnum state, 
            TScope scope, 
            Func<TDto, TFilter> getFilter, 
            Action<TEntity, TDto> customMap = null,
            bool ignoreLists = false)
            where TDto : class, IDataTransferObject, new()
            where TEntity : class, IEntity, new()
            where TFilter : FilterBase
            where TScope : class, IScope
        {
            if (dto == null) throw new ArgumentNullException("dto");

            TEntity entity = GetEntity<TDto, TEntity, TFilter, TScope>(instances, dto, state, scope, getFilter);
            Map(dto, state, entity, scope, customMap, ignoreLists);
            return entity;
        }

        /// <summary>
        /// Gets or Creates the entity and maps the dto to the entity.
        /// </summary>
        /// <typeparam name="TDto">The type of DataTransferObject.</typeparam>
        /// <typeparam name="TEntity">The type of Entity.</typeparam>
        /// <typeparam name="TFilter">The type of Filter.</typeparam>
        /// <typeparam name="TScope">The type of user Scope data.</typeparam>
        /// <param name="instances">The instances of repositories.</param>
        /// <param name="dto">The data that needs to be saved to the datastore.</param>
        /// <param name="state">The type of action being performed (only New, Modified do anything).</param>
        /// <param name="scope">The configuration information about the currently logged in user.</param>
        /// <param name="getFilter">
        /// Anonymous function that returns a Filter with the Primary Key assigned to get 
        /// the correct entity (null if New Entity).
        /// </param>
        /// <param name="customMap">Anonymous function called for any custom mapping a service may need to do.</param>
        /// <param name="ignoreLists">True if sub lists should not be mapped from dto to entity; false otherwise.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<TEntity> GetAndMapAsync<TDto, TEntity, TFilter, TScope>(
            InstanceManager instances,
            TDto dto, 
            DataStateEnum state,
            TScope scope,
            Func<TDto, TFilter> getFilter,
            Action<TEntity, TDto> customMap = null,
            bool ignoreLists = false)
            where TDto : class, IDataTransferObject, new()
            where TEntity : class, IEntity, new()
            where TFilter : FilterBase
            where TScope : class, IScope
        {
            if (dto == null) throw new ArgumentNullException("dto");

            TEntity entity = await GetEntityAsync<TDto, TEntity, TFilter, TScope>(instances, dto, state, scope, getFilter);
            Map(dto, state, entity, scope, customMap, ignoreLists);
            return entity;
        }

        #endregion

        #region << Get Entity Methods >>

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <typeparam name="TDto">The type of DataTransferObject.</typeparam>
        /// <typeparam name="TEntity">The type of Entity.</typeparam>
        /// <typeparam name="TFilter">The type of Filter.</typeparam>
        /// <typeparam name="TScope">The type of user Scope data.</typeparam>
        /// <param name="instances">The instances of repositories.</param>
        /// <param name="dto">The data that needs to be saved to the datastore.</param>
        /// <param name="state">The type of action being performed (only New, Modified do anything).</param>
        /// <param name="scope">The configuration information about the currently logged in user.</param>
        /// <param name="getFilter">
        /// Anonymous function that returns a Filter with the Primary Key assigned to get 
        /// the correct entity (null if New Entity).
        /// </param>
        /// <returns>The entity.</returns>
        public static TEntity GetEntity<TDto, TEntity, TFilter, TScope>(
            InstanceManager instances,
            TDto dto, 
            DataStateEnum state, 
            TScope scope, 
            Func<TDto, TFilter> getFilter)
            where TDto : class, IDataTransferObject
            where TEntity : class, IEntity, new()
            where TFilter : FilterBase
            where TScope : class, IScope
        {
            TEntity entity = default(TEntity);
            switch (state)
            {
                case DataStateEnum.New:
                    entity = new TEntity
                    {
                        DataState = DataStateEnum.New
                    };
                    break;
                case DataStateEnum.Modified:
                {
                    Guard.IsNotNull(getFilter, "getFilter");

                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(instances);
                    entity = manager.Get<TEntity, TFilter>(getFilter(dto)).SingleOrDefault();
                    if (entity != null)
                    {
                        entity.DataState = DataStateEnum.Modified;
                    }
                    break;
                }
                case DataStateEnum.Deleted:
                {
                    Guard.IsNotNull(getFilter, "getFilter");

                    var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(instances);
                    entity = manager.Get<TEntity, TFilter>(getFilter(dto)).SingleOrDefault();
                    if (entity != null)
                    {
                        entity.DataState = DataStateEnum.Deleted;
                    }
                    break;
                }
            }

            return entity;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <typeparam name="TDto">The type of DataTransferObject.</typeparam>
        /// <typeparam name="TEntity">The type of Entity.</typeparam>
        /// <typeparam name="TFilter">The type of Filter.</typeparam>
        /// <typeparam name="TScope">The type of user Scope data.</typeparam>
        /// <param name="instances">The instances of repositories.</param>
        /// <param name="dto">The data that needs to be saved to the datastore.</param>
        /// <param name="state">The type of action being performed (only New, Modified do anything).</param>
        /// <param name="scope">The configuration information about the currently logged in user.</param>
        /// <param name="getFilter">
        /// Anonymous function that returns a Filter with the Primary Key assigned to get 
        /// the correct entity (null if New Entity).
        /// </param>
        /// <returns>The entity.</returns>
        public static async Task<TEntity> GetEntityAsync<TDto, TEntity, TFilter, TScope>(
            InstanceManager instances,
            TDto dto,
            DataStateEnum state,
            TScope scope,
            Func<TDto, TFilter> getFilter)
            where TDto : class, IDataTransferObject
            where TEntity : class, IEntity, new()
            where TFilter : FilterBase
            where TScope : class, IScope
        {
            TEntity entity = default(TEntity);
            switch (state)
            {
                case DataStateEnum.New:
                    entity = new TEntity
                    {
                        DataState = DataStateEnum.New
                    };
                    break;
                case DataStateEnum.Modified:
                    {
                        Guard.IsNotNull(getFilter, "getFilter");

                        var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(instances);
                        entity = (await manager.GetAsync<TEntity, TFilter>(getFilter(dto))).SingleOrDefault();
                        if (entity != null)
                        {
                            entity.DataState = DataStateEnum.Modified;
                        }
                        break;
                    }
                case DataStateEnum.Deleted:
                    {
                        Guard.IsNotNull(getFilter, "getFilter");

                        var manager = RepositoryManagerHelper.GetRepositoryManager<TEntity>(instances);
                        entity = (await manager.GetAsync<TEntity, TFilter>(getFilter(dto))).SingleOrDefault();
                        if (entity != null)
                        {
                            entity.DataState = DataStateEnum.Deleted;
                        }
                        break;
                    }
            }

            return entity;
        }

        #endregion

        #region << Map Methods >>

        /// <summary>
        /// Gets or Creates the entity and maps the dto to the entity, ignoring sub-lists.
        /// </summary>
        /// <typeparam name="TDto">The type of DataTransferObject.</typeparam>
        /// <typeparam name="TEntity">The type of Entity.</typeparam>
        /// <typeparam name="TScope">The type of user Scope data.</typeparam>
        /// <param name="dto">The data that needs to be saved to the datastore.</param>
        /// <param name="state">The type of action being performed</param>
        /// <param name="entity">The entity to be mapped to.</param>
        /// <param name="scope">The configuration information about the currently logged in user.</param>
        /// <param name="customMap">Anonymous function called for any custom mapping a service may need to do.</param>
        /// <param name="ignoreLists">True if sub lists should not be mapped from dto to entity; false otherwise.</param>
        /// <returns>The entity to be saved.</returns>
        public static void Map<TDto, TEntity, TScope>(
            TDto dto,
            DataStateEnum state,
            TEntity entity, 
            TScope scope, 
            Action<TEntity, TDto> customMap = null,
            bool ignoreLists = false)
            where TDto : class, IDataTransferObject, new()
            where TEntity : class, IEntity, new()
            where TScope : IScope
        {
            Guard.IsNotNull(dto, "dto");

            if (entity == null)
            {
                return;
            }

            if (state == DataStateEnum.Deleted)
            {
                return;
            }

            ClassyMap.New().Map(entity, dto);
            if (customMap != null)
            {
                customMap(entity, dto);
            }
        }

        #endregion

    }
}