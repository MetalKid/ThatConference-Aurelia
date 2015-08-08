#region << Usings >>

using ThatConference.Common.Interfaces;
using ThatConference.Northwind.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Interfaces.Services
{
    /// <summary>
    /// This interface defines the methods to register/login the current user.
    /// </summary>
    public interface IScopeService
    {

        /// <summary>
        /// Creates a new scope for the user and returns the key to retrieve it later.
        /// </summary>
        /// <param name="userName">The user name of the logged in user.</param>
        /// <returns>They key to retrieve this scope.</returns>
        IDataResult<INorthwindScopeKey> CreateScope(string userName);

        /// <summary>
        /// Returns information on the scope that the caller is allowed to see.
        /// </summary>
        /// <returns>INorthwindScopePublic.</returns>
        IDataResult<INorthwindScopePublic> GetPublicScope();

    }
}
