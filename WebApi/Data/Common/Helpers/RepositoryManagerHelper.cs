#region << Usings >>

using System;
using System.Linq;
using ThatConference.Common.Interfaces;
using ThatConference.Data.Common.Interfaces;

#endregion

namespace ThatConference.Data.Common.Helpers
{
    /// <summary>
    /// This class helps to get the correct Repository Manager.
    /// </summary>
    public static class RepositoryManagerHelper
    {

        /// <summary>
        /// Returns the repository manager that can work with the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity being worked with.</typeparam>
        /// <returns>Instance of IRepositoryManager.</returns>
        public static IRepositoryManager GetRepositoryManager<TEntity>(IInstanceManager instances)
            where TEntity : class, IEntity
        {
            var managers = instances.GetInstances<IRepositoryManager>(false);
            if (managers != null && managers.Count > 0)
            {
                var manager = managers.FirstOrDefault(m => m.ContainsEntityType<TEntity>());
                if (manager != null)
                {
                    return manager;
                }
            }
            throw new NotImplementedException(
                "No RepositoryManager found that contains a Repository for entity type '" + typeof (TEntity).Name + "'.");
        }

    }
}
