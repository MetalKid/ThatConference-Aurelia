namespace ThatConference.Common.Enums
{
    /// <summary>
    /// This defines what type of hashing to perform.
    /// </summary>
    public enum HashingTypeEnum
    {
        /// <summary>
        /// An unknown hashing type. Do not use.
        /// </summary>
        Unknown,
        /// <summary>
        /// Secure Hash Algorithm - 32 bit
        /// </summary>
        Sha256,
        /// <summary>
        /// Secure Hash Algorithm - 48 bit, but truncated version of Sha512.
        /// </summary>
        Sha384,
        /// <summary>
        /// Secure Hash Algorithm 64 bit.
        /// </summary>
        Sha512
    }
}