#region << Usings >>

using ThatConference.Common.Interfaces;
using ThatConference.Data.Common.Interfaces;

#endregion

namespace ThatConference.Data.Common.Interfaces
{
    /// <summary>
    /// This interface is used for getting new connections to a database.
    /// </summary>
    /// <typeparam name="T">The type of database context to create.</typeparam>
    public interface IDbContextFactory<out T> : IIoC
        where T : IDbContext
    {

        /// <summary>
        /// Returns a new connection to the database.
        /// </summary>
        /// <returns>An instance of a IDbContext.</returns>
        T Create();

    }
}
