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
    /// This class represents data coming from the [dbo].[CustomerCustomerDemo] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class CustomerCustomerDemoDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [CustomerID] column.
        /// </summary>
        public string CustomerID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [CustomerTypeID] column.
        /// </summary>
        public string CustomerTypeID { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[CustomerDemographics] table via column(s) [CustomerTypeID].
        /// </summary>
        public CustomerDemographicsDto CustomerDemographics { get; set; }
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Customers] table via column(s) [CustomerID].
        /// </summary>
        public CustomersDto Customers { get; set; }

        #endregion

    }
}