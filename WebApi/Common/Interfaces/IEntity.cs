#region << Usings >>

using ThatConference.Common.Enums;

#endregion

namespace ThatConference.Common.Interfaces
{
    /// <summary>
    /// This interface defines all common columns/methods for all Entities.
    /// </summary>
    public interface IEntity
    {
    
        #region << Properties >>

        /// <summary>
        /// Gets or sets the Timestamp column on the table.
        /// </summary>
        byte[] Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the state of this entity. (i.e. Unchanged, New, Modified, Deleted)
        /// </summary>
        DataStateEnum DataState { get; set; }

        #endregion

        #region << Methods >>

        /// <summary>
        /// Sets the timestamp based upon the base-64 encoded string.
        /// </summary>
        /// <param name="base64EncodedTimestamp">Timestamp in string representation.</param>
        void SetBase64Timestamp(string base64EncodedTimestamp);

        #endregion

    }
}