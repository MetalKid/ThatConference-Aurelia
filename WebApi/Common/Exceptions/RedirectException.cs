#region << Usings >>

using System;

#endregion

namespace ThatConference.Common.Exceptions
{
    /// <summary>
    /// This exception is thrown when a process wants the 
    /// client to be redirected somewhere else instead of continuing.
    /// </summary>
    [Serializable]
    public class RedirectException : Exception
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets where the client should be redirected.
        /// </summary>
        public string Redirection { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>.
        /// Constructor that takes a message, inner exception, and url.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="redirection">The Url to redirect the client to.</param>
        public RedirectException(string message = null, Exception innerException = null, string redirection = null)
            : base(message, innerException)
        {
            Redirection = redirection;
        }

        #endregion

    }
}