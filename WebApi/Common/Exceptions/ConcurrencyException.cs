#region << Usings >>

using System;

#endregion

namespace ThatConference.Common.Exceptions
{
    /// <summary>
    /// This exception is thrown when a Timestamp is defined on the filter and 
    /// the record is not found after finding a record without filtering on the Timestamp.
    /// </summary>
    [Serializable]
    public class ConcurrencyException : Exception
    {

        #region << Constructors >>

        /// <summary>
        /// Constructor that takes a message and inner exception.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public ConcurrencyException(string message = null, Exception innerException = null) 
            : base(message, innerException)
        {
        }

        #endregion

    }
}