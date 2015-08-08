#region << Usings >>

using ThatConference.Common;
using ThatConference.Common.Helpers;
using ThatConference.Common.Interfaces;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Services.Common;
using ThatConference.Northwind.Services.Helpers;
using ThatConference.Northwind.Services.Interfaces;

#endregion

namespace ThatConference.Northwind.Services
{
    /// <summary>
    /// This class works with the identity of the current user.
    /// </summary>
    public class ScopeService : NorthwindServiceBase, IScopeServiceInternal
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that loads the given instances by their interfaces.
        /// </summary>
        /// <param name="scopeKey">The key to get the user's scope.</param>
        public ScopeService(INorthwindScopeKey scopeKey)
            : base(scopeKey, null)
        {
        }

        #endregion

        #region << IScopeService Methods >>

        /// <summary>
        /// Creates a new scope for the user and returns the key to retrieve it later.
        /// </summary>
        /// <param name="userName">The user name of the logged in user.</param>
        /// <returns>They key to retrieve this scope.</returns>
        public IDataResult<INorthwindScopeKey> CreateScope(string userName) //TODO: Pass in all user information here
        {
            return Run<DataResult<INorthwindScopeKey>>(
                result =>
                {
                    Guard.IsNotNullOrEmpty(userName, "userName");
                    result.Data = ScopeHelper.CreateScope(userName);
                }, 
                userName);
        }

        /// <summary>
        /// Returns information on the scope that the caller is allowed to see.
        /// </summary>
        /// <returns>INorthwindScopePublic.</returns>
        public IDataResult<INorthwindScopePublic> GetPublicScope()
        {
            return Run<DataResult<INorthwindScopePublic>>(
                result =>
                {
                    result.Data = Scope as INorthwindScopePublic;
                });
        }

        #endregion

    }
}