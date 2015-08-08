#region << Usings >>

using System;

#endregion

namespace ThatConference.Common.Exceptions
{
    /// <summary>
    /// This exception is thrown with the intention that the message is displayed directly to the user.
    /// </summary>
    [Serializable]
    public class UserFriendlyException : Exception
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that takes a message and inner exception.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public UserFriendlyException(string message = null, Exception innerException = null) 
            : base(message, innerException)
        {
        }

        #endregion

    }
}