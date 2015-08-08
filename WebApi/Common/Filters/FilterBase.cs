#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using ThatConference.Common.Interfaces;
using QueryFilter;
using QueryFilter.Interfaces;

#endregion

namespace ThatConference.Common.Filters
{
    /// <summary>
    /// This class is inherited by all Filters being passed from the UI to the Repository.
    /// </summary>
    [Serializable]
    public abstract class FilterBase : ITimestamp, IFilterGroup
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets the maximum number records to return.
        /// </summary>
        public int? MaxRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the page number to fetch.
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of records to fetch.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Gets or sets the Timestamp of the record to get.
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets top level filter groupings.
        /// </summary>
        public IList<FilterGroup> FilterGroups { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Default constructor that initializes FilterGroups.
        /// </summary>
        protected FilterBase()
        {
            FilterGroups = new List<FilterGroup>();
        }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Adds new Filter Groups for specific And/Or clauses.
        /// </summary>
        /// <param name="items">The FilterGroups to add.</param>
        public void AddGroups(params FilterGroup[] items)
        {
            if (items == null || items.Length == 0 || !items.Any())
            {
                throw new InvalidOperationException("At least 1 group and statement are required.");
            }
            foreach (var item in items)
            {
                FilterGroups.Add(item);
            }
        }

        #endregion

    }
}