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
    /// This class represents data coming from the [dbo].[Suppliers] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class SuppliersDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [SupplierID] column.
        /// </summary>
        public int SupplierID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [CompanyName] column.
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ContactName] column.
        /// </summary>
        public string ContactName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [ContactTitle] column.
        /// </summary>
        public string ContactTitle { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Address] column.
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [City] column.
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Region] column.
        /// </summary>
        public string Region { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [PostalCode] column.
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Country] column.
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Phone] column.
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Fax] column.
        /// </summary>
        public string Fax { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [HomePage] column.
        /// </summary>
        public string HomePage { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Products] table via column(s) [SupplierID].
        /// </summary>
        [MapListItem("ProductID")]
        public ICollection<ProductsDto> ProductProductses { get; set; }

        #endregion

    }
}