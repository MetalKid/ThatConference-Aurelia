#region << Usings >>

using System.Collections.Generic;
using ThatConference.Common;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Validation;
using ThatConference.Data.Common;

#endregion

namespace ThatConference.Services.Common.Interfaces
{
    /// <summary>
    /// This interface defines how to run business rules for a given entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to validate.</typeparam>
    /// <typeparam name="TScope">The type of scope that identifies the user.</typeparam>
    public interface IBusinessRuleValidation<in TEntity, in TScope> : IIoC
        where TEntity : EntityBase
        where TScope : class, IScope
    {

        /// <summary>
        /// Ensures that the given entitiy is valid and has not broken any business related rules.
        /// </summary>
        /// <param name="brokenRules">The container of all the rules that have been broken.</param>
        /// <param name="scope">The configuration information about the currently logged in user.</param>
        /// <param name="entity">The entity to be validated.</param>
        void ApplyRules(IList<BrokenRule> brokenRules, TScope scope, TEntity entity);

    }
}