#region << Usings >>

using ThatConference.Data.Northwind.Entities;
using ThatConference.Northwind.Common.DataTransferObjects.Northwind;
using ThatConference.Northwind.Common.Filters.Northwind;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Interfaces.Repositories.Northwind;
using ThatConference.Northwind.Services.Common;
using ThatConference.Northwind.Services.Interfaces.Northwind;
using ThatConference.Northwind.Services.Interfaces.Northwind.BusinessRules;

#endregion

namespace ThatConference.Northwind.Services.EntityServices.Northwind
{
    /// <summary>
    /// This service makes calls to the [dbo].[sysdiagrams] table.
    /// </summary>
    public class sysdiagramsService : 
        NorthwindServiceBase<sysdiagramsDto, sysdiagrams, sysdiagramsFilter>, 
        IsysdiagramsServiceInternal
    {
        
        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scopeKey">The key to get the user's scope.</param>
        /// <param name="northwindBusinessRuleManager">
        /// The instance that stores all possible business rules to use.
        /// </param>
        /// <param name="northwindRepositoryManager">
        /// The instance that stores all possible repositories to use.
        /// </param>
        public sysdiagramsService(
            INorthwindScopeKey scopeKey,
            INorthwindBusinessRuleManager northwindBusinessRuleManager,
            INorthwindRepositoryManager northwindRepositoryManager)
            : base(
            scopeKey,
            northwindBusinessRuleManager,
            northwindRepositoryManager)
        {
        }

        #endregion

        #region << sysdiagramsDto Save Methods >>

        /// <summary>
        /// Assigns the filter data needed to load an entity based upon the given dto for saving.
        /// </summary>
        /// <param name="filter">The filter to assign the primary key properties to.</param>
        /// <param name="dto">The dto to get the primary key values from.</param>
        public void AssignFilterDataForSave(sysdiagramsFilter filter, sysdiagramsDto dto)
        {
            filter.diagramid.EqualTo(dto.diagramid);


        }

        #endregion

    }
}