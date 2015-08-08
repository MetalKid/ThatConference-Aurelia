namespace ThatConference.Common.Interfaces
{
    /// <summary>
    /// This interface is implemented by Hashing algorythm classes.
    /// </summary>
    public interface IHashing
    {

        /// <summary>
        /// Returns a hashed version of the password.
        /// </summary>
        /// <param name="userName">The user's userName.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>Hashed string.</returns>
        string CreateHash(string userName, string password);

        /// <summary>
        /// Returns whether the given password matches the hash.
        /// </summary>
        /// <param name="username">The user's given userName.</param>
        /// <param name="password">The user's given password.</param>
        /// <param name="hashedOriginalPassword">The hashed version of the password.</param>
        /// <returns>True if the given password is correct; false otherwise.</returns>
        bool VerifyHash(string username, string password, string hashedOriginalPassword);

    }
}