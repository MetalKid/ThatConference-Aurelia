#region << Usings >>

using System;
using ThatConference.Northwind.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Common
{
    /// <summary>
    /// This interface is used to pull out the NorthwindScopeScope in the service layer.
    /// </summary>
    public class NorthwindScopeKey : INorthwindScopeKey
    {

        #region << Properties >>

        /// <summary>
        /// Gets the key to find the corresponding IScope object.
        /// </summary>
        public Guid Key { get; private set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that takes in the lookup key.
        /// </summary>
        /// <param name="key">The lookup key.</param>
        public NorthwindScopeKey(Guid key)
        {
            Key = key;
        }

        #endregion

    }
}
