#region << Usings >>

using System;

#endregion

namespace ThatConference.Common.Interfaces
{
    /// <summary>
    /// This interface is implemented by classes that are used to identity user information.
    /// </summary>
    public interface IScope
    {

        //TODO: Fill this class with information about the currently logged in user
        //NOTE: Feel free to entirely change the contents of this class

        /// <summary>
        /// Gets or sets the primary key of the currently logged in user.
        /// </summary>
        Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name of the currently logged in user.
        /// </summary>
        string UserName { get; }

    }
}