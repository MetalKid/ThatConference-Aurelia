#region << Usings >>

using System;

#endregion

namespace ThatConference.Common.Interfaces
{
    /// <summary>
    /// This interface stores basic result data of the process plus data returned by the process.
    /// </summary>
    /// <typeparam name="T">The type of data being returned.</typeparam>
    public interface IDataResult<out T> : IResult
    {

        /// <summary>
        /// Gets the data involved with this result.
        /// </summary>
        T Data { get; }

        /// <summary>
        /// Converts this result to base Result, regardless of what it was.
        /// </summary>
        /// <param name="getData">This anonymous function returns the Json of the given data.</param>
        /// <returns>An anonymous class with data the client utilizes.</returns>
        object ToJsonData(Func<T, object> getData);

    }
}