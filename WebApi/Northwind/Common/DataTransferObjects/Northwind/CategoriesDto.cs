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
    /// This class represents data coming from the [dbo].[Categories] table.
    /// </summary>
    [Serializable]
    [MapAllProperties]
    public class CategoriesDto : SaveableDataTransferObjectBase
    {

        #region << Entity Properties >>
        
        /// <summary>
        /// Gets or sets the value for the [CategoryID] column.
        /// </summary>
        public int CategoryID { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [CategoryName] column.
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Description] column.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the value for the [Picture] column.
        /// </summary>
        public byte[] Picture { get; set; }

        #endregion
        
        #region << Entity Navigation Properties >>
        
        /// <summary>
        /// Gets or sets the data associated with the [dbo].[Products] table via column(s) [CategoryID].
        /// </summary>
        [MapListItem("ProductID")]
        public ICollection<ProductsDto> ProductProductses { get; set; }

        #endregion

    }
}