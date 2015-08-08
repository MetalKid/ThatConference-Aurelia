#region << Usings >>

using System.Collections.Generic;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Validation;

#endregion

namespace ThatConference.Data.Common.Interfaces
{
    /// <summary>
    /// This interface defines methods to validate that the entity is not breaking any database rules.
    /// </summary>
    /// <typeparam name="TContext">The connection to the database.</typeparam>
    /// <typeparam name="TEntity">The type of entity being returned.</typeparam>
    internal interface IValidatableRepositoryInternal<in TContext, in TEntity>
        where TEntity : class, IEntity
    {

        /// <summary>
        /// Validates the given parent and all of its children before being saved.
        /// </summary>
        /// <param name="brokenRules">The list that contains any broken rules that may have occurred.</param>
        /// <param name="ctx">The connection to the database.</param>
        /// <param name="entity">The current record being inspected.</param>
        /// <returns>The list of Broken Rules, if any.</returns>
        void ValidateEntity(IList<BrokenRule> brokenRules, TContext ctx, TEntity entity);

    }
}