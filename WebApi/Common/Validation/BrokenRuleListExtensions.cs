#region << Usings >>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace ThatConference.Common.Validation
{
    /// <summary>
    /// This extends IList of BrokenRule since all of these methods require a list of broken rules to work.
    /// </summary>
    public static class BrokenRuleListExtensions
    {

        #region << Email Methods >>

        /// <summary>
        /// Ensures the given email address is valid.
        /// </summary>
        /// <param name="brokenRules">The current list of broken rules.</param>
        /// <param name="emailAddress">The email address to validate.</param>
        /// <param name="entityName">The name to show in the broken rule message.</param>
        /// <param name="message">The error message to display.</param>
        /// <param name="relation">The name the UI can reference this broken rule.</param>
        /// <param name="id">The primary key of the entity.</param>
        public static void EmailAddress(
            this IList<BrokenRule> brokenRules,
            string emailAddress,
            string entityName,
            string relation = null,
            Lazy<string> id = null,
            string message = "{0} is invalid.")
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return;
            }
            Regex regex = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
            Match match = regex.Match(emailAddress);
            if (!match.Success)
            {
                brokenRules.Add(
                    new BrokenRule(string.Format(message, entityName), relation ?? entityName, id == null ? null : id.Value));
            }
        }

        #endregion

        #region << Numeric Methods >>

        /// <summary>
        /// Ensures the given check exists.
        /// </summary>
        /// <param name="brokenRules">The current list of broken rules.</param>
        /// <param name="maxLength">The max length of the string.</param>
        /// <param name="value">The string the check.</param>
        /// <param name="entityName">The name to show in the broken rule message.</param>
        /// <param name="message">The error message to display.</param>
        /// <param name="relation">The name the UI can reference this broken rule.</param>
        /// <param name="id">The primary key of the entity.</param>
        public static void MaxLength(
            this IList<BrokenRule> brokenRules,
            int maxLength,
            string value,
            string entityName,
            string relation = null,
            Lazy<string> id = null,
            string message = "{0} cannot be longer than {1} characters.")
        {
            if (value != null && value.Length > maxLength)
            {
                brokenRules.Add(
                    new BrokenRule(
                        string.Format(message, maxLength),
                        relation ?? entityName,
                        id == null ? null : id.Value));
            }
        }

        #endregion

        #region << True/False Methods >>

        /// <summary>
        /// Ensures the given check exists.
        /// </summary>
        /// <param name="brokenRules">The current list of broken rules.</param>
        /// <param name="isEmptyCheck">True if the value is empty; false otherwise.</param>
        /// <param name="entityName">The name to show in the broken rule message.</param>
        /// <param name="relation">The name the UI can reference this broken rule.</param>
        /// <param name="id">The primary key of the entity.</param>
        /// <param name="message">The error message to display.</param>
        public static void Required(
            this IList<BrokenRule> brokenRules,
            bool isEmptyCheck,
            string entityName,
            string relation = null,
            Lazy<string> id = null,
            string message = "{0} is required.")
        {
            if (isEmptyCheck)
            {
                brokenRules.Add(
                    new BrokenRule(
                        string.Format(message, entityName),
                        relation ?? entityName,
                        id == null ? null : id.Value));
            }
        }

        /// <summary>
        /// Ensures that the given check exists.
        /// </summary>
        /// <param name="brokenRules">The current list of broken rules.</param>
        /// <param name="exists">True if the value exists; false otherwise.</param>
        /// <param name="entityName">The name to show in the broken rule message.</param>
        /// <param name="message">The error message to display.</param>
        /// <param name="relation">The name the UI can reference this broken rule.</param>
        /// <param name="id">The primary key of the entity.</param>
        public static void DoesNotExist(
            this IList<BrokenRule> brokenRules,
            bool exists,
            string entityName,
            string relation = null,
            Lazy<string> id = null,
            string message = "{0} does not exist.")
        {
            if (!exists)
            {
                brokenRules.Add(
                    new BrokenRule(
                        string.Format(message, entityName),
                        relation ?? entityName,
                        id == null ? null : id.Value));
            }
        }

        /// <summary>
        /// Ensures that the field is not already in use.
        /// </summary>
        /// <param name="brokenRules">The current list of broken rules.</param>
        /// <param name="isInUseCheck">True if the value is already in use; false otherwise.</param>
        /// <param name="entityName">The name to show in the broken rule message.</param>
        /// <param name="message">The error message to display.</param>
        /// <param name="relation">The name the UI can reference this broken rule.</param>
        /// <param name="id">The primary key of the entity.</param>
        public static void AlreadyInUse(
            this IList<BrokenRule> brokenRules,
            bool isInUseCheck,
            string entityName,
            string relation = null,
            Lazy<string> id = null,
            string message = "{0} is already in use.")
        {
            if (isInUseCheck)
            {
                brokenRules.Add(
                    new BrokenRule(
                        string.Format(message, entityName),
                        relation ?? entityName,
                        id == null ? null : id.Value));
            }
        }

        #endregion

    }
}
