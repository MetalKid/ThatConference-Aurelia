#region << Usings >>

using System;
using System.Collections.Generic;

#endregion

namespace ThatConference.Common.Interfaces
{
    /// <summary>
    /// This interface defines classes that contain their own instance manager.
    /// </summary>
    public interface IInstanceManager
    {

        /// <summary>
        /// Returns the instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="throwExceptionIfNone">
        /// If true, an exception will be thrown if no instances of the given type were registered; 
        /// otherwise, default (normally null) is returned.
        /// </param>
        /// <returns>The instance of the given type.</returns>
        T GetInstance<T>(bool throwExceptionIfNone = true);

        /// <summary>
        /// Returns the instance(s) of the given type.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="throwExceptionIfNone">
        /// If true, an exception will be thrown if no instances of the given type were registered; 
        /// otherwise, default (normally null) is returned.
        /// </param>
        /// <returns>The list of instances of the given type.</returns>
        IList<T> GetInstances<T>(bool throwExceptionIfNone = true);

        /// <summary>
        /// Returns the instance of the given type.
        /// </summary>
        /// <param name="type">The type of instance to get.</param>
        /// <param name="throwExceptionIfNone">
        /// If true, an exception will be thrown if no instances of the given type were registered; 
        /// otherwise, default (normally null) is returned.
        /// </param>
        /// <param name="genericArguments">Any generic arguments to use when creating the type, if any.</param>
        /// <returns>The instance of the given type.</returns>
        dynamic GetInstance(Type type, bool throwExceptionIfNone = true, params Type[] genericArguments);
      
        /// <summary>
        /// Returns the instance of the given type.
        /// </summary>
        /// <param name="type">The type of instance to get.</param>
        /// <param name="throwExceptionIfNone">
        /// If true, an exception will be thrown if no instances of the given type were registered; 
        /// otherwise, default (normally null) is returned.
        /// </param>
        /// <param name="genericArguments">Any generic arguments to use when creating the type, if any.</param>
        /// <returns>The instance of the given type.</returns>
        IList<dynamic> GetInstances(Type type, bool throwExceptionIfNone = true, params Type[] genericArguments);

    }
}
