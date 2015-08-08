#region << Usings >>

using System;
using System.Collections.Generic;
using ThatConference.Common.Validation;

#endregion

namespace ThatConference.Common.Exceptions
{
    /// <summary>
    /// This exception is thrown when rules are broken validating an Entity.
    /// </summary>
    [Serializable]
    public class BrokenRuleException : Exception
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets the list of broken rules that occurred.
        /// </summary>
        public List<BrokenRule> BrokenRules { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that takes a single Broken Rule.
        /// </summary>
        /// <param name="rule">The broken rule that occurred.</param>
        public BrokenRuleException(BrokenRule rule)
            : this(null, null)
        {
            BrokenRules.Add(rule);
        }

        /// <summary>
        /// Constructor that takes a list of Broken Rules.
        /// </summary>
        /// <param name="rules">The broken rule(s) that occurred.</param>
        public BrokenRuleException(IEnumerable<BrokenRule> rules)
            : this(null, null)
        {
            BrokenRules.AddRange(rules);
        }

        /// <summary>
        /// Constructor that takes a message and creates a Broken Rule and inner exception.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public BrokenRuleException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            BrokenRules = new List<BrokenRule>();
            if (!string.IsNullOrWhiteSpace(message))
            {
                BrokenRules.Add(new BrokenRule
                {
                    RuleMessage = message
                });
            }
        }

        #endregion

    }
}