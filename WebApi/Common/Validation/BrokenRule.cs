#region << Usings >>

using System;

#endregion

namespace ThatConference.Common.Validation
{
    /// <summary>
    /// This class stores information about a rule an entity has broken.
    /// </summary>
    public class BrokenRule
    {
     
        #region << Properties >>

        /// <summary>
        /// Gets or sets the message to display for this exception.
        /// </summary>
        public string RuleMessage { get; set; }

        /// <summary>
        /// Gets or sets the field this message is related to. (The UI can put this error in the right place.)
        /// </summary>
        public string Relation { get; set; }

        /// <summary>
        /// Gets or sets the Key of this entity when working with lists of items.
        /// </summary>
        public string Id { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BrokenRule() { }

        /// <summary>
        /// Constructor the defaults the message.
        /// </summary>
        /// <param name="ruleMessage">The broken rule message.</param>
        public BrokenRule(string ruleMessage)
            : this(ruleMessage, string.Empty, string.Empty) { }

        /// <summary>
        /// Constructor that defaults the message and relation fields.
        /// </summary>
        /// <param name="ruleMessage">The broken rule message.</param>_
        /// <param name="relation">For the UI to use to put the error on the screen somewhow.</param>
        public BrokenRule(string ruleMessage, string relation)
        {
            RuleMessage = ruleMessage;
            Relation = relation;
        }
        
        /// <summary>
        /// Constructor that defaults the message and relation fields.
        /// </summary>
        /// <param name="ruleMessage">The broken rule message.</param>_
        /// <param name="relation">For the UI to use to put the error on the screen somewhow.</param>
        /// <param name="id">Primary Key of the entity with the broken rule.</param>
        public BrokenRule(string ruleMessage, string relation, string id)
        {
            RuleMessage = ruleMessage;
            Relation = relation;
            Id = id;
        }

        #endregion

    }
}