#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Common.Helpers
{
    /// <summary>
    /// This class is used to store instances that can be looked up by their interface(s).
    /// </summary>
    public class InstanceManager : IInstanceManager
    {

        #region << Static Methods >>

        /// <summary>
        /// Creates an instance of this class and returns it.
        /// </summary>
        /// <param name="instances">The instances to load into this class.</param>
        /// <returns>Instance of InstanceManager.</returns>
        public static InstanceManager Create(params object[] instances)
        {
            return new InstanceManager(instances);
        }

        #endregion

        #region << Variables >>

        private readonly IDictionary<Type, IList<object>> _instances = new Dictionary<Type, IList<object>>();

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Private constructor that processes the given instances.
        /// </summary>
        /// <param name="instances"></param>
        private InstanceManager(params object[] instances)
        {
            LoadInstances(instances);
        }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Returns the instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="throwExceptionIfNone">
        /// If true, an exception will be thrown if no instances of the given type were registered; 
        /// otherwise, default (normally null) is returned.
        /// </param>
        /// <returns>The instance of the given type.</returns>
        public T GetInstance<T>(bool throwExceptionIfNone = true)
        {
            var instances = GetInstances<T>(throwExceptionIfNone);
            return instances == null ? default(T) : instances.SingleOrDefault();
        }

        /// <summary>
        /// Returns the instance(s) of the given type.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="throwExceptionIfNone">
        /// If true, an exception will be thrown if no instances of the given type were registered; 
        /// otherwise, default (normally null) is returned.
        /// </param>
        /// <returns>The list of instances of the given type.</returns>
        public IList<T> GetInstances<T>(bool throwExceptionIfNone = true)
        {
            IList<object> instances;
            if (_instances.TryGetValue(typeof(T), out instances))
            {
                return instances.Cast<T>().ToList();
            }

            if (throwExceptionIfNone)
            {
                throw new NotSupportedException(typeof(T).FullName + " type was not IoC injected.");
            }
            return null;
        }

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
        public dynamic GetInstance(Type type, bool throwExceptionIfNone = true, params Type[] genericArguments)
        {
            var instances = GetInstances(type, throwExceptionIfNone, genericArguments);
            return instances == null ? null : instances.SingleOrDefault();
        }

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
        public IList<dynamic> GetInstances(Type type, bool throwExceptionIfNone = true, params Type[] genericArguments)
        {
            Type actual = type;
            if (genericArguments != null && genericArguments.Length > 0)
            {
                actual = actual.MakeGenericType(genericArguments);
            }

            IList<dynamic> result;
            _instances.TryGetValue(actual, out result);

            if (throwExceptionIfNone && (result == null || result.Count == 0))
            {
                throw new NotSupportedException(type.FullName + " type was not IoC injected.");
            }

            return result;
        }

        #endregion

        #region << Helper Methods >>

        /// <summary>
        /// Assigns each interface type for each instance to a dictionary for later use.
        /// </summary>
        /// <param name="instances">The instances to load.</param>
        private void LoadInstances(IEnumerable<object> instances)
        {
            if (instances == null)
            {
                return;
            }

            var iiocType = typeof (IIoC);
            var iiocGroupType = typeof (IIoCGroup);
            foreach (var obj in instances)
            {
                if (obj is IIoCGroup)
                {
                    List<object> propertyInstances = new List<object>();

                    foreach (var prop in
                        obj.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance))
                    {
                        var val = prop.GetValue(obj, null);
                        if (val == null)
                        {
                            continue;
                        }
                        if (val is IIoC)
                        {
                            propertyInstances.Add(val);
                        }
                    }
                    LoadInstances(propertyInstances);
                }

                foreach (
                    var type in obj.GetType().GetInterfaces().Where(type => type != iiocType && type != iiocGroupType))
                {
                    if (!_instances.ContainsKey(type))
                    {
                        _instances.Add(type, new List<object>());
                    }
                    _instances[type].Add(obj);
                }
            }
        }

        #endregion

    }
}