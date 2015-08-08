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
    /// This class represents data coming from the [dbo].[Region] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class RegionDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [RegionID] column.
        /// </summary>
        public int RegionID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [RegionDescription] column.
        /// </summary>
        public string RegionDescription { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Territories] table via column(s) [RegionID].
        /// </summary>
        [MapListItem("TerritoryID")]
        public ICollection<TerritoriesDto> TerritorieTerritorieses { get; set; }

        #endregion

    }
}