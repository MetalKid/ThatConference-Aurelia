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
    /// This class represents data coming from the [dbo].[EmployeeTerritories] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class EmployeeTerritoriesDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [EmployeeID] column.
        /// </summary>
        public int EmployeeID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [TerritoryID] column.
        /// </summary>
        public string TerritoryID { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Employees] table via column(s) [EmployeeID].
        /// </summary>
        public EmployeesDto Employees { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Territories] table via column(s) [TerritoryID].
        /// </summary>
        public TerritoriesDto Territories { get; set; }

        #endregion

    }
}