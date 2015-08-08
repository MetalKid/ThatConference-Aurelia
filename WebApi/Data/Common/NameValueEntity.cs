namespace ThatConference.Data.Common
{
    /// <summary>
    /// This class holds common Name/Value pair data for custom selects.
    /// </summary>
    public class NameValueEntity : EntityBase
    {

        /// <summary>
        /// Gets or sets the Name (Description).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Value (Id/Key).
        /// </summary>
        public string Value { get; set; }

    }
}