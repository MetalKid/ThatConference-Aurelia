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
    /// This class represents data coming from the [dbo].[Shippers] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class ShippersDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [ShipperID] column.
        /// </summary>
        public int ShipperID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [CompanyName] column.
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Phone] column.
        /// </summary>
        public string Phone { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Orders] table via column(s) [ShipVia].
        /// </summary>
        [MapListItem("OrderID")]
        public ICollection<OrdersDto> OrderOrderses { get; set; }

        #endregion

    }
}