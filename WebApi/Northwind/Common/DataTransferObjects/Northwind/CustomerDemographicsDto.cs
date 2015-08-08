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
    /// This class represents data coming from the [dbo].[CustomerDemographics] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class CustomerDemographicsDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [CustomerTypeID] column.
        /// </summary>
        public string CustomerTypeID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [CustomerDesc] column.
        /// </summary>
        public string CustomerDesc { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[CustomerCustomerDemo] table via column(s) [CustomerTypeID].
        /// </summary>
        [MapListItem("CustomerID", "CustomerTypeID")]
        public ICollection<CustomerCustomerDemoDto> CustomerCustomerDemos { get; set; }

        #endregion

    }
}