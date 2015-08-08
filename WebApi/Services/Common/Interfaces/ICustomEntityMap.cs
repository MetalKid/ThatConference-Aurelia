#region << Usings >>

using ThatConference.Common;
using ThatConference.Data.Common;

#endregion

namespace ThatConference.Services.Common.Interfaces
{
    /// <summary>
    /// This interface allows custom mapping from a Dto to an Entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of Entity to map the data to.</typeparam>
    /// <typeparam name="TDto">The type of Dto to map the data from.</typeparam>
    public interface ICustomEntityMap<in TEntity, in TDto>
        where TEntity : EntityBase, new()
        where TDto : SaveableDataTransferObjectBase, new()
    {

        /// <summary>
        /// This method is called in case a custom mapping needs to occur during create or update of TDto.
        /// </summary>
        /// <param name="entity">The entity the data is being mapped to.</param>
        /// <param name="dto">The dto the data is being mapped from.</param>
        void CustomMap(TEntity entity, TDto dto);

    }
}
