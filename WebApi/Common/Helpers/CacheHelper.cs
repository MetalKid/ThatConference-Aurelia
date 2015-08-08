#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using ThatConference.Common.Enums;

#endregion

namespace ThatConference.Common.Helpers
{
    /// <summary>
    /// This class is a threadsafe cache for storing and retrieving items of T, with housekeeping to remove
    /// old/expired entries.
    /// </summary>
    /// <typeparam name="T">The type of data being stored</typeparam>
    public class Cache<T>
    {

        #region << CacheObject SubClass >>

        /// <summary>
        /// This class holds all information about the object being cached.
        /// </summary>
        private class CacheObject
        {
            #region << Properties >>

            public T Data { get; set; }
            public DateTime InsertDate { get; set; }
            public TimeSpan LifeSpan { get; set; }
            public CacheExpirationBehaviorEnum CacheExpirationBehavior { get; set; }

            #endregion

            #region << Constructor >>

            public CacheObject()
            {
                CacheExpirationBehavior = CacheExpirationBehaviorEnum.RemoveAfterFirstQuery;
                LifeSpan = TimeSpan.FromMinutes(1);
                InsertDate = DateTime.UtcNow;
                Data = default(T);
            }

            #endregion
        }

        #endregion

        #region << Variables >>

        private readonly Dictionary<Guid, CacheObject> _cacheData = new Dictionary<Guid, CacheObject>();
        private readonly object _lockObject = new object();

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Adds a new item to the cache.
        /// </summary>
        /// <param name="item">The data being stored.</param>
        /// <param name="cacheExpirationBehavior">How to expire the item in the cache.</param>
        /// <param name="lifeSpan">How long the data should live in the cache.</param>
        /// <param name="ownerId">Optional parameter of what owns this data.</param>
        /// <returns>The key to this data in the cache.</returns>
        public Guid Add(T item, CacheExpirationBehaviorEnum cacheExpirationBehavior, TimeSpan lifeSpan,
            Guid? ownerId = null)
        {
            var id = ownerId.HasValue ? ownerId.Value : Guid.NewGuid();

            lock (_lockObject)
            {
                _cacheData.Add(id, 
                    new CacheObject
                    {
                        Data = item, 
                        CacheExpirationBehavior = cacheExpirationBehavior,
                        LifeSpan = lifeSpan
                    }
                );
            }

            return id;
        }

        /// <summary>
        /// Returns whether the given key exists in the cache.
        /// </summary>
        /// <param name="id">The key to the data in the cache.</param>
        /// <returns>True if the item exists; false otherwise.</returns>
        public bool Contains(Guid id)
        {
            lock (_lockObject)
            {
                return _cacheData.ContainsKey(id);
            }
        }

        /// <summary>
        /// Removes data from the cache with the given key.
        /// </summary>
        /// <param name="id">The key to the data in the cache.</param>
        public void Remove(Guid id)
        {
            lock (_lockObject)
            {
                _cacheData.Remove(id);
            }
        }

        /// <summary>
        /// Returns the data.
        /// </summary>
        /// <param name="id">The key to the data in the cache.</param>
        /// <returns>The data that is stored in the cache.</returns>
        public T Retrieve(Guid id)
        {
            T result;

            if (TryRetrieve(id, out result))
            {
                return result;
            }

            throw new ArgumentException("Object not found in cache.", "id");
        }

        /// <summary>
        /// Updates the data in the cache.
        /// </summary>
        /// <param name="id">The key to the data in the cache.</param>
        /// <param name="value">The data to update the cache with.</param>
        public void UpdateValue(Guid id, T value)
        {
            lock (_lockObject)
            {
                if (_cacheData.ContainsKey(id))
                {
                    _cacheData[id].Data = value;
                }
            }
        }

        /// <summary>
        /// Attempts to retrieve the data with the given key.
        /// </summary>
        /// <param name="id">The key to the data in the cache.</param>
        /// <param name="value">The data from the cache.</param>
        /// <returns>True if the key exists; false otherwise.</returns>
        public bool TryRetrieve(Guid id, out T value)
        {
            lock (_lockObject)
            {
                HouseKeeping();

                CacheObject cachedObject;
                if (_cacheData.TryGetValue(id, out cachedObject))
                {
                    value = cachedObject.Data;

                    if (cachedObject.CacheExpirationBehavior == CacheExpirationBehaviorEnum.KeepAliveUntilExpired)
                    {
                        cachedObject.InsertDate = DateTime.UtcNow;
                    }

                    if (cachedObject.CacheExpirationBehavior == CacheExpirationBehaviorEnum.RemoveAfterFirstQuery)
                    {
                        _cacheData.Remove(id);
                    }

                    return true;
                }
            }

            value = default(T);
            return false;
        }

        #endregion

        #region << Helper Methods >>

        /// <summary>
        /// Removes old entries from the cache.
        /// </summary>
        private void HouseKeeping()
        {
            lock (_lockObject)
            {
                _cacheData
                    .Where(x => x.Value.InsertDate + x.Value.LifeSpan < DateTime.UtcNow)
                    .Select(x => x.Key)
                    .ToList()
                    .ForEach(x => _cacheData.Remove(x));
            }
        }

        #endregion

    }
}
