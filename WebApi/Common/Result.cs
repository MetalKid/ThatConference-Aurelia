#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using ThatConference.Common.Enums;
using ThatConference.Common.Exceptions;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Validation;

#endregion

namespace ThatConference.Common
{
      /// <summary>
    /// This class stores basic result data of the process.
    /// </summary>
    [Serializable]
    public class Result : IResult
    {

        #region << Properites >>

        /// <summary>
        /// Gets or sets the title of the message to display
        /// </summary>
        /// <remarks>
        /// DO NOT put any properties above this one. 
        /// The client scripts expect MessageTitle to be the first property returned to determine if a Result
        /// type was passed back.  This allows the client to compare the resulting JSON as a string instead of
        /// having to parse the entire contents.
        /// </remarks>
        public string MessageTitle { get; set; }

        /// <summary>
        /// Gets or sets the text of the message to display.
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Gets or sets the type of message being returned.
        /// </summary>
        public MessageIconTypeEnum MessageIconType { get; set; }

        /// <summary>
        /// Gets or sets the URL the user should be redirected to.
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the number of rows inserted, updated, or deleted.
        /// </summary>
        public int RowsAffected { get; set; }

        /// <summary>
        /// Gets or sets whether the process was successful.
        /// (i.e. No exception was thrown and the min number of rows were met.)
        /// </summary>
        public bool IsSuccessful { get { return BrokenRules.Count == 0 && HandledExceptions.Count == 0; }}

        /// <summary>
        /// Gets or sets the list of Timestamps for an updated/created record, if needed.
        /// </summary>
        public IList<string> Timestamps { get; set; }

        /// <summary>
        /// Gets or sets the broken rules that occurred, if any.
        /// </summary>
        public IList<BrokenRule> BrokenRules { get; private set; }

        /// <summary>
        /// Gets or sets a list of warning messages to display to the user.
        /// </summary>
        public IList<string> WarningMessages { get; private set; }

        /// <summary>
        /// Gets or sets the exception that was handled by this result.
        /// </summary>
        public IList<Exception> HandledExceptions { get; private set; }

        /// <summary>
        /// Gets or sets the exceptions that were logged up to this point.
        /// </summary>
        public IList<Exception> LoggedExceptions { get; private set; }

        /// <summary>
        /// Gets or sets the key of the record being saved.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the list of keys of the records being saved.
        /// </summary>
        public IList<string> Keys { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Default cosntructor.
        /// </summary>
        public Result()
        {
            BrokenRules = new List<BrokenRule>();
            WarningMessages = new List<string>();
            HandledExceptions = new List<Exception>();
            LoggedExceptions = new List<Exception>();
            Timestamps = new List<string>();
        }

        #endregion

        #region << Timestamp Methods >>

        /// <summary>
        /// Converts the byte[] timestamp to a string base 64 and stores it.
        /// </summary>
        /// <param name="timestamp">The timestamp to store.</param>
        public void SetByteArrayTimestamp(byte[] timestamp)
        {
            if (timestamp == null)
            {
                return;
            }

            Timestamps = Timestamps ?? new List<string>();
            Timestamps.Add(Convert.ToBase64String(timestamp));
        }

        #endregion

        #region << Warning Methods >>

        /// <summary>
        /// Adds a new warning message to be displayed to the user.
        /// </summary>
        /// <param name="message"></param>
        public void AddWarningMessage(string message)
        {
            if (WarningMessages == null) WarningMessages = new List<string>();
            WarningMessages.Add(message);
        }

        #endregion

        #region << Broken Rule Methods >>

        /// <summary>
        /// Adds a new broken rule to the list.
        /// </summary>
        /// <param name="message">The broken rule message.</param>
        public void AddBrokenRule(string message)
        {
            AddBrokenRule(message, null);
        }

        /// <summary>
        /// Adds a new broken rule to the list
        /// </summary>
        /// <param name="message">The broken rule message.</param>
        /// <param name="relation">For the UI to use to put the error on the screen somewhow.</param>
        public void AddBrokenRule(string message, string relation)
        {
            AddBrokenRule(message, relation, null);
        }

        /// <summary>
        /// Adds a new broken rule to the list
        /// </summary>
        /// <param name="message">The broken rule message.</param>
        /// <param name="relation">For the UI to use to put the error on the screen somewhow.</param>
        /// <param name="id">Primary Key of the entity with the broken rule.</param>
        public void AddBrokenRule(string message, string relation, string id)
        {
            if (BrokenRules.All(a => a.RuleMessage != message || a.Relation != relation || a.Id != id))
            {
                BrokenRules.Add(new BrokenRule(message, relation, id));
            }
        }

        /// <summary>
        /// Verifies that the result has no broken rules.  If so, a BrokenRuleException is thrown.
        /// </summary>
        public void Verify()
        {
            Verify("Another user has made a change to this record. Please refresh the page and try again.");
        }

        /// <summary>
        /// Verifies that the result has no broken rules.  If so, a BrokenRuleException is thrown.
        /// </summary>
        /// <param name="concurrencyExceptionMessage">The message to show the user for a concurrency issue.</param>
        public void Verify(string concurrencyExceptionMessage)
        {
            if (BrokenRules.Count > 0)
            {
                throw new BrokenRuleException(BrokenRules);
            }

            if (HandledExceptions.Count == 1 &&
                (HandledExceptions[0] is UserFriendlyException || HandledExceptions[0] is RedirectException))
            {
                throw HandledExceptions[0];
            }
            
            if (HandledExceptions.Count(a => a is ConcurrencyException) > 0)
            {
                throw new UserFriendlyException(concurrencyExceptionMessage);
            }
        }

        #endregion

        #region << Merge Methods >>

        /// <summary>
        /// Merges another result into this one, usually from a sub-process being run.
        /// </summary>
        /// <param name="result">The result to merge in.</param>
        public virtual void Merge<T>(T result)
            where T : IResult
        {
            foreach (var item in result.BrokenRules)
            {
                if (BrokenRules.Count(a => a.RuleMessage == item.RuleMessage) == 0)
                {
                    BrokenRules.Add(item);
                }
            }
            foreach (var ex in result.HandledExceptions)
            {
                HandledExceptions.Add(ex);
            }
            foreach (var ex in result.LoggedExceptions)
            {
                LoggedExceptions.Add(ex);
            }
            foreach (var item in result.WarningMessages)
            {
                if (!WarningMessages.Contains(item))
                {
                    WarningMessages.Add(item);
                }
            }
            RowsAffected += result.RowsAffected;
        }

        #endregion

        #region << Exception Methods >>

        /// <summary>
        /// Handles an exception by marking the result as not successful and adding broken rules if applicable.
        /// The exception will also be logged.
        /// </summary>
        /// <param name="ex">The exception to handle.</param>
        public virtual void HandleException(Exception ex)
        {
            HandledExceptions.Add(ex);
            var brException = ex as BrokenRuleException;

            if (brException == null)
            {
                return;
            }

            foreach (var rule in brException.BrokenRules)
            {
                BrokenRules.Add(rule);
            }
            //Note: We do not log the exception here.  The UI will figure out how/where to log it.
        }

        #endregion

        #region << Json Methods >>

        /// <summary>
        /// Converts this result to base Result, regardless of what it was.
        /// </summary>
        public virtual object ToJson()
        {
            return
                new
                {
                    MessageTitle, // MessageTile MUST be first
                    BrokenRules,
                    IsSuccessful,
                    MessageIconType,
                    MessageText,
                    RedirectUrl,
                    RowsAffected,
                    WarningMessages,
                    Timestamps
                };
        }
        
        #endregion

    }
}