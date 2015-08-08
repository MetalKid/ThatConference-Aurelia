#region << Usings >>

using System;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Common.Interfaces
{
    /// <summary>
    /// This interface is used to find the INorthwindScope in the service layer.
    /// </summary>
    public interface INorthwindScopeKey : IScopeKey
    {

        /// <summary>
        /// Gets the key to find the corresponding IScope object.
        /// </summary>
        Guid Key { get; }

    }
}
