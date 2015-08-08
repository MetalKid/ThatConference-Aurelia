#region << Usings >>

using ThatConference.Common;
using ThatConference.Common.Filters;

#endregion

namespace ThatConference.Services.Common.Interfaces
{
    /// <summary>
    /// This interface defines a method to assign data to get a unique record back from the database.
    /// </summary>
    /// <typeparam name="TFilter">The type of filter to set the primary key values to.</typeparam>
    /// <typeparam name="TDtoIn">The type of dto to get the primary key values from.</typeparam>
    public interface IAssignFilterDataForSave<in TFilter, in TDtoIn>
        where TFilter : FilterBase
        where TDtoIn : SaveableDataTransferObjectBase
    {

        /// <summary>
        /// Assigns the filter data needed to load an entity based upon the given dto for saving.
        /// </summary>
        /// <param name="filter">The filter to assign the primary key properties to.</param>
        /// <param name="dto">The dto to get the primary key values from.</param>
        void AssignFilterDataForSave(TFilter filter, TDtoIn dto);

    }
}
