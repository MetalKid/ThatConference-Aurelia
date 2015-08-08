#region << Usings >>

using System;
using ClassyMapper.Attributes;

#endregion

namespace ThatConference.Common
{
    /// <summary>
    /// This class stores common Name/Value pairs, normally for lookup data.
    /// </summary>
    [Serializable]
    public class NameValueDto : DataTransferObjectBase
    {

        /// <summary>
        /// Gets or sets the Name or Description.
        /// </summary>
        [MapProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Value or Id/Key.
        /// </summary>
        [MapProperty("Value")]
        public string Value { get; set; }

    }
}