#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using ThatConference.Common.Exceptions;
using ThatConference.Northwind.Common;
using ThatConference.Northwind.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Services.Helpers
{
    /// <summary>
    /// This class helps to create/get/manage user Scope objects.
    /// </summary>
    internal static class ScopeHelper
    {

        #region << Constants >>

        private const int SCOPE_EXPIRE_MINUTES = 35;

        #endregion

        #region << Variables >>

        private readonly static Dictionary<INorthwindScopeKey, INorthwindScope> _scopes =
            new Dictionary<INorthwindScopeKey, INorthwindScope>();
        private static DateTime _lastHouseKeepingRun = DateTime.UtcNow;

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Expires any scopes that have not been accessed in the past 35 minutes
        /// </summary>
        public static void RunHouseKeeping()
        {
            if (!((DateTime.UtcNow - _lastHouseKeepingRun).TotalSeconds > 60))
            {
                return;
            }
            lock (_scopes)
            {
                var expiredSessions = _scopes
                    .Where(a => (DateTime.UtcNow - a.Value.LastAccessed).TotalMinutes > SCOPE_EXPIRE_MINUTES)
                    .Select(a => a.Key).ToList();

                expiredSessions.ForEach(session =>
                {
                    if (_scopes.ContainsKey(session))
                    {
                        _scopes.Remove(session);
                    }
                });
            }
            _lastHouseKeepingRun = DateTime.UtcNow;
        }
        
        /// <summary>
        /// Creates a new scope and returns the key to it.
        /// </summary>
        /// <param name="userName">The user name of the logged in user.</param>
        /// <returns>They key to retrieve this scope.</returns>
        public static INorthwindScopeKey CreateScope(string userName) //TODO: Pass in all user information here
        {
            var key = new NorthwindScopeKey(Guid.NewGuid());

            INorthwindScope scope = new NorthwindScope
            {
                UserName = userName
            };
            scope.KeepAlive();

            lock (_scopes)
            {
                _scopes[key] = scope;
            }

            return key;
        }

        /// <summary>
        /// Returns the scope for the given key.
        /// </summary>
        /// <param name="key">The key to lookup the scope object.</param>
        /// <returns>Instance of INorthwindScope.</returns>
        public static INorthwindScope GetScope(INorthwindScopeKey key)
        {
            RunHouseKeeping();
            if (key == null || key.Key == Guid.Empty)
            {
                return null;
            }

            INorthwindScope identity;
            lock (_scopes)
            {
                identity = _scopes.ContainsKey(key) ? _scopes[key] : null;
            }
            if (identity == null)
            {
                throw new UserFriendlyException("Cannot find Identity with the given key. Please refresh page.");
            }

            identity.KeepAlive();
            return identity;
        }

        #endregion

    }
}
