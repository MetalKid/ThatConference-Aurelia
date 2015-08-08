namespace ThatConference.Common.Enums
{
    /// <summary>
    /// This enum defines the various states an entity can be in.
    /// </summary>
    public enum DataStateEnum
    {
        /// <summary>
        /// Record just came from the database and has no changes.
        /// </summary>
        Unchanged,
        /// <summary>
        /// This is a new record to be inserted into the database.
        /// </summary>
        New,
        /// <summary>
        /// This is a record that came from the database and was changed somehow.
        /// </summary>
        Modified,
        /// <summary>
        /// This record is to be deleted from the database.
        /// </summary>
        Deleted
    }
}