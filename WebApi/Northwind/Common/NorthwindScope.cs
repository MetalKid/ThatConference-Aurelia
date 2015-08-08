#region << Usings >>

using System;
using ThatConference.Northwind.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Common
{
    /// <summary>
    /// This class holds information about the currently logged in user.
    /// </summary>
    public class NorthwindScope : INorthwindScope, INorthwindScopePublic
    {

        //TODO: Fill this class with information about the currently logged in user
        //NOTE: Feel free to entirely change the contents of this class

        #region << Properties >>

        /// <summary>
        /// Gets or sets the primary key of the currently logged in user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name of the currently logged in user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the last time this scope was used.
        /// </summary>
        public DateTime LastAccessed { get; private set; }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Updates the LastAccessed to UtcNow.
        /// </summary>
        public void KeepAlive()
        {
            LastAccessed = DateTime.UtcNow;
        }

        #endregion

    }
}