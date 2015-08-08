#region << Usings >>

using System;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Common
{
    /// <summary>
    /// This class is inherited by all DataTransferObjects that move data from the UI to the Service.
    /// </summary>
    [Serializable]
    public abstract class DataTransferObjectBase : IDataTransferObject
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets whether this Dto is the representation of a null Entity.
        /// </summary>
        public bool IsNull { get; set; }

        #endregion

    }
}