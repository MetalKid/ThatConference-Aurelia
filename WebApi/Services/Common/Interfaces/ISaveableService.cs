#region << Usings >>

using System.Collections.Generic;
using System.Threading.Tasks;
using ThatConference.Common;
using ThatConference.Common.Filters;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Services.Common.Interfaces
{
    /// <summary>
    /// This interface defines methods to Save data for a service based upon the DataState value of each TDtoIn.
    /// </summary>
    /// <typeparam name="TFilter">The type of filter to set the primary key values to.</typeparam>
    /// <typeparam name="TDtoIn">The type of dto to map the data to be saved from.</typeparam>
    /// <typeparam name="TDtoOut">The type of dto that is returned and mapped from the entities.</typeparam>
    public interface ISaveableService<in TFilter, TDtoIn, TDtoOut> : IAssignFilterDataForSave<TFilter, TDtoIn>
        where TFilter : FilterBase
        where TDtoIn : SaveableDataTransferObjectBase
        where TDtoOut : IDataTransferObject
    {

        /// <summary>
        /// Saves the entities in the datastore that match the given dto data, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The result of the transaction.</returns>
        IDataResult<ICollection<TDtoOut>> Save(ICollection<TDtoIn> dtos);

        /// <summary>
        /// Saves the entities in the datastore that match the given dto data, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The result of the transaction.</returns>
        Task<IDataResult<ICollection<TDtoOut>>> SaveAsync(ICollection<TDtoIn> dtos);

    }
}
