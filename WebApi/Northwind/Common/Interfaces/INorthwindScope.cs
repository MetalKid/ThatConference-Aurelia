#region << Usings >>

using System;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Common.Interfaces
{
    /// <summary>
    /// This interface defines all information about the user.
    /// </summary>
    public interface INorthwindScope : IScope
    {
    
        //TODO: Fill this class with information about the currently logged in user

        /// <summary>
        /// Gets the last time this scope was used.
        /// </summary>
        DateTime LastAccessed { get; }

        /// <summary>
        /// Updates the LastAccessed to UtcNow.
        /// </summary>
        void KeepAlive();

    }
}