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
    /// This interface defines methods to Insert a massive amount of data for a service.
    /// </summary>
    /// <typeparam name="TFilter">The type of filter to set the primary key values on.</typeparam>
    /// <typeparam name="TDtoIn">The type of dto that sends the data to update.</typeparam>
    /// <typeparam name="TDtoOut">The type of dto that is returned and mapped from the entities.</typeparam>
    /// <remarks>Only SQL Server can bulk insert data.</remarks>
    public interface ISaveableWithBulkInsertService<in TFilter, TDtoIn, TDtoOut> : ISaveableService<TFilter, TDtoIn, TDtoOut>
        where TFilter : FilterBase
        where TDtoIn : SaveableDataTransferObjectBase
        where TDtoOut : IDataTransferObject
    {

        /// <summary>
        /// Creates the given dtos to the datastore, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The result of the transaction.</returns>
        IResult BulkInsert(ICollection<TDtoIn> dtos);

        /// <summary>
        /// Creates the given dtos to the datastore, returning the number of affected rows.
        /// </summary>
        /// <param name="dtos">The data to save.</param>
        /// <returns>The result of the transaction.</returns>
        Task<IResult> BulkInsertAsync(ICollection<TDtoIn> dtos);

    }
}
