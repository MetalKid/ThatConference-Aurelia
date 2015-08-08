namespace ThatConference.Common.Interfaces
{
    /// <summary>
    /// This interface defines methods used in two-way encyryption.
    /// </summary>
    public interface ITwoWayEncryption
    {

        /// <summary>
        /// Returns a randomly generated encyrption key.
        /// </summary>
        /// <returns></returns>
        byte[] GenerateEncryptionKey();

        /// <summary>
        /// Returns a randomly generated vector.
        /// </summary>
        /// <returns></returns>
        byte[] GenerateEncryptionVector();

        /// <summary>
        /// Encrypts the value.
        /// </summary>
        /// <param name="textValue">The value to encrypt.</param>
        /// <param name="key">The key to encrypt the value with.</param>
        /// <param name="vector">The vector to encrypt the value with.</param>
        /// <returns>An encrypted string.</returns>
        string EncryptToString(string textValue, byte[] key, byte[] vector);

        /// <summary>
        /// Decrypts the value.
        /// </summary>
        /// <param name="encryptedString">The encrypted value.</param>
        /// <param name="key">The key that was used to encrypt the original value with.</param>
        /// <param name="vector">The vectory that was used to encyrpt the original value with.</param>
        /// <returns>A decrypted string.</returns>
        string DecryptString(string encryptedString, byte[] key, byte[] vector);

    }
}
