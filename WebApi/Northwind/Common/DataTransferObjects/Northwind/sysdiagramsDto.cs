#region << Usings >>

using System;
using System.Collections.Generic;
using ClassyMapper.Attributes;
using ThatConference.Common;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Northwind.Common.DataTransferObjects.Northwind
{
    /// <summary>
    /// This class represents data coming from the [dbo].[sysdiagrams] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class sysdiagramsDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [name] column.
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [principal_id] column.
        /// </summary>
        public int principalid { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [diagram_id] column.
        /// </summary>
        public int diagramid { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [version] column.
        /// </summary>
        public int? version { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [definition] column.
        /// </summary>
        public byte[] definition { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>

        #endregion

    }
}