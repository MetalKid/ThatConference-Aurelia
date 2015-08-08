namespace ThatConference.Common.Enums
{
    /// <summary>
    /// This enum defines when a cache item should expire.
    /// </summary>
    public enum CacheExpirationBehaviorEnum
    {
        /// <summary>
        /// The item should get removed from the cache as soon as it has been retrieved for the first time.
        /// </summary>
        RemoveAfterFirstQuery,
        /// <summary>
        /// The item should get removed from the cache once its initial lifespan has expired.
        /// </summary>
        RemoveOnlyWhenExpired,
        /// <summary>
        /// The item should get removed from the cache once its lifespan has expired. Each time it gets
        /// retrieved, the lifespan starts anew.
        /// </summary>
        KeepAliveUntilExpired
    }
}