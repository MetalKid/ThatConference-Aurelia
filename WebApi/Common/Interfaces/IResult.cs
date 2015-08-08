#region << Usings >>

using System;
using System.Collections.Generic;
using ThatConference.Common.Enums;
using ThatConference.Common.Validation;

#endregion

namespace ThatConference.Common.Interfaces
{
/// <summary>
    /// This interfaces stores basic result data of the process.
    /// </summary>
    public interface IResult
    {

        /// <summary>
        /// Gets or sets the title of the message to display
        /// </summary>
        string MessageTitle { get; set;  }

        /// <summary>
        /// Gets or sets the text of the message to display.
        /// </summary>
        string MessageText { get; set; }

        /// <summary>
        /// Gets or sets the type of message being returned.
        /// </summary>
        MessageIconTypeEnum MessageIconType { get; set; }

        /// <summary>
        /// Gets the number of rows inserted, updated, or deleted.
        /// </summary>
        int RowsAffected { get; }

        /// <summary>
        /// Gets whether the process was successful. (i.e. No exception was thrown and the min number of rows were met)
        /// </summary>
        bool IsSuccessful { get; }

        /// <summary>
        /// Gets or sets the URL the user should be redirected to.
        /// </summary>
        string RedirectUrl { get; }

        /// <summary>
        /// Gets or sets the list of Timestamps for an updated/created record, if needed.
        /// </summary>
        IList<string> Timestamps { get; }

        /// <summary>
        /// Gets or sets the exceptions that were handled by this result.
        /// </summary>
        IList<Exception> HandledExceptions { get; }

        /// <summary>
        /// Gets or sets the exceptions that were logged up to this point.
        /// </summary>
        IList<Exception> LoggedExceptions { get; }

        /// <summary>
        /// Gets or sets the broken rules that occurred, if any.
        /// </summary>
        IList<BrokenRule> BrokenRules { get; }

        /// <summary>
        /// Gets or sets a list of warning messages to display to the user.
        /// </summary>
        IList<string> WarningMessages { get; }

        /// <summary>
        /// Gets or sets the key of the record being saved.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets or sets the list of keys of the records being saved.
        /// </summary>
        IList<string> Keys { get; }

        /// <summary>
        /// Merges another result into this one, usually from a sub-process being run.
        /// </summary>
        /// <param name="result">The result to merge in.</param>
        void Merge<T>(T result) where T : IResult;

        /// <summary>
        /// Verifies that the result has no broken rules.  If so, a BrokenRuleException is thrown.
        /// </summary>
        void Verify();

        /// <summary>
        /// Verifies that the result has no broken rules.  If so, a BrokenRuleException is thrown.
        /// </summary>
        void Verify(string concurrencyExceptionMessage);

        /// <summary>
        /// Converts the byte[] timestamp to a string base 64 and stores it.
        /// </summary>
        /// <param name="timestamp">The timestamp to store.</param>
        void SetByteArrayTimestamp(byte[] timestamp);

        /// <summary>
        /// Handles an exception by marking the result as not successful and adding broken rules if applicable.
        /// The exception will also be logged.
        /// </summary>
        /// <param name="ex">The exception to handle.</param>
        void HandleException(Exception ex);

        /// <summary>
        /// Converts this result to base Result, regardless of what it was.
        /// </summary>
        /// <returns>An anonymous class with data the client utilizes.</returns>
        object ToJson();

    }
}