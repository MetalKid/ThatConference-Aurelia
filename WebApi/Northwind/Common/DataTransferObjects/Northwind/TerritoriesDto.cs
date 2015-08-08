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
    /// This class represents data coming from the [dbo].[Territories] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class TerritoriesDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [TerritoryID] column.
        /// </summary>
        public string TerritoryID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [TerritoryDescription] column.
        /// </summary>
        public string TerritoryDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [RegionID] column.
        /// </summary>
        public int RegionID { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Region] table via column(s) [RegionID].
        /// </summary>
        public RegionDto Region { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[EmployeeTerritories] table via column(s) [TerritoryID].
        /// </summary>
        [MapListItem("EmployeeID", "TerritoryID")]
        public ICollection<EmployeeTerritoriesDto> EmployeeTerritorieEmployeeTerritorieses { get; set; }

        #endregion

    }
}