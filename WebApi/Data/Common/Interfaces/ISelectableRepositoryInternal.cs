#region << Usings >>

using System.Collections.Generic;
using System.Threading.Tasks;
using ThatConference.Common.Filters;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Data.Common.Interfaces
{
    /// <summary>
    /// This interface defines methods to get data from the database.
    /// </summary>
    /// <typeparam name="TContext">The connection to the database.</typeparam>
    /// <typeparam name="TEntity">The type of entity being returned.</typeparam>
    /// <typeparam name="TFilter">The type of filter used to filter the data.</typeparam>
    internal interface ISelectableRepositoryInternal<in TContext, TEntity, in TFilter>
        where TContext : IDbContext
        where TEntity : class, IEntity
        where TFilter : FilterBase
    {

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        ICollection<TEntity> Get(TContext ctx, TFilter filter);

        /// <summary>
        /// Returns the list of records that match the given filter criteria.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        Task<ICollection<TEntity>> GetAsync(TContext ctx, TFilter filter);

        /// <summary>
        /// Returns the total records that come back based upon the given filter.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        int GetCount(TContext ctx, TFilter filter);

        /// <summary>
        /// Returns the total records that come back based upon the given filter.
        /// </summary>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="filter">Data used to reduce the number of records returned.</param>
        /// <returns>Records that match the given filter criteria, if any.</returns>
        Task<int> GetCountAsync(TContext ctx, TFilter filter);
        
    }
}