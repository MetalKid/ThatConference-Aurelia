#region << Usings >>

using System;
using ClassyMapper.Attributes;
using ThatConference.Common.Enums;

#endregion

namespace ThatConference.Common
{
    /// <summary>
    /// This class is inherited by all DataTransferObjects that move data from the UI to the Service and can
    /// save data back.
    /// </summary>
    [Serializable]
    public abstract class SaveableDataTransferObjectBase : DataTransferObjectBase
    {

        /// <summary>
        /// Gets or sets which way to save this Dto.
        /// </summary>
        [MapProperty]
        public DataStateEnum DataState { get; set; }

    }
}
