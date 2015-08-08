#region << Usings >>

using System;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Common
{
    /// <summary>
    /// This class stores basic result data of the process plus data returned by the process.
    /// </summary>
    /// <typeparam name="T">The type of data being returned.</typeparam>
    [Serializable]
    public class DataResult<T> : Result, IDataResult<T>
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets the data involved with this result.
        /// </summary>
        public T Data { get; set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataResult() { }
        
        /// <summary>
        /// Constructor that takes the data as a parameter.
        /// </summary>
        /// <param name="item">The data to be passed back to the caller.</param>
        public DataResult(T item)
        {
            Data = item;
        }

        #endregion

        #region << Merge Methods >>

        /// <summary>
        /// Merges another result into this one, usually from a sub-process being run.
        /// </summary>
        /// <param name="result">The result to merge in.</param>
        public virtual void MergeData(IDataResult<T> result)
        {
            Data = result.Data;
            base.Merge(result);
        }

        #endregion

        #region << Json Methods >>

        /// <summary>
        /// Converts this result to base Result, regardless of what it was.
        /// </summary>
        /// <param name="getData">This anonymous function returns the Json of the given data.</param>
        /// <returns>An anonymous class with data the client utilizes.</returns>
        public virtual object ToJsonData(Func<T, object> getData = null)
        {
            return
                new
                {
                    MessageTitle, // MessageTile MUST be first
                    BrokenRules,
                    IsSuccessful,
                    MessageIconType,
                    MessageText,
                    RedirectUrl,
                    RowsAffected,
                    WarningMessages,
                    Data = getData == null ? Data : getData(Data),
                    Timestamps
                };
        }

        #endregion

    }
}