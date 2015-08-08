#region << Usings >>

using System.Collections.Generic;
using System.Threading.Tasks;
using ThatConference.Common.Filters;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Services.Common.Interfaces
{
    /// <summary>
    /// This interface defines the methods on a service to get data.
    /// </summary>
    /// <typeparam name="TDto">The type of data returned.</typeparam>
    /// <typeparam name="TFilter">The type of filter to fetch the data</typeparam>
    public interface ISelectableService<TDto, in TFilter>
        where TDto : IDataTransferObject
        where TFilter: FilterBase
    {

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        IDataResult<ICollection<TDto>> Get(TFilter filter = null);

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        Task<IDataResult<ICollection<TDto>>> GetAsync(TFilter filter = null);

    }
}