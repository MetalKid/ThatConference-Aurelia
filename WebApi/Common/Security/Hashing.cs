#region << Usings >>

using System;
using System.Security.Cryptography;
using System.Text;
using ThatConference.Common.Enums;
using ThatConference.Common.Helpers;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Common.Security
{
    /// <summary>
    /// This class is used to hash data.
    /// </summary>
    public class Hashing : IHashing
    {

        #region << Constants >>

        private const string HASH_START_SHA2 = "SHA2:";

        #endregion

        #region << Properties >>

        /// <summary>
        /// Gets the type of hashing to perform.
        /// </summary>
        public HashingTypeEnum HashingType { get; private set; }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Construtor that takes the type of hash to perform.
        /// </summary>
        /// <param name="hasingType"></param>
        public Hashing(HashingTypeEnum hasingType)
        {
            HashingType = hasingType;
        }

        #endregion

        #region << Hash Methods >>

        /// <summary>
        /// Returns a hashed version of the password.
        /// </summary>
        /// <param name="userName">The user's User Name.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>Hashed string.</returns>
        public string CreateHash(string userName, string password)
        {
            Guard.IsNotNullOrEmpty(userName, "userName");
            Guard.IsNotNullOrEmpty(password, "password");

            // Generate a random salt 
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[5];
            csprng.GetBytes(salt);

            return CreateHash(userName, password, salt);
        }

        /// <summary>
        /// Returns whether the given password matches the hash.
        /// </summary>
        /// <param name="username">The user's userName.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="hashedOriginalPassword">The hashed version of the password.</param>
        /// <returns>True if the given password is correct; false otherwise.</returns>
        public bool VerifyHash(string username, string password, string hashedOriginalPassword)
        {
            if (!hashedOriginalPassword.StartsWith(HASH_START_SHA2) || 
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            string[] parts = hashedOriginalPassword.Split(new[] { ':' });
            if (parts.Length != 3)
            {
                return false;
            }

            string saltPartOfHash = parts[2];

            // Calculate Hash again...
            string stringToHash = string.Format("{0}-{1}-{2}", username, password, saltPartOfHash);

            string referenceHash = ComputeHash(stringToHash);

            return parts[1] == referenceHash;
        }

        /// <summary>
        /// Returns a hashed version of the password.
        /// </summary>
        /// <param name="userName">The user's userName.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="salt">The random bytes to add.</param>
        /// <returns>Hashed string.</returns>
        private string CreateHash(string userName, string password, byte[] salt)
        {
            string saltString = BitConverter.ToString(salt).Replace("-", "");

            // The final string to hash:
            string stringToHash = string.Format("{0}-{1}-{2}", userName, password, saltString);
            string hashedString = ComputeHash(stringToHash);

            string valueToStoreInDatabase = String.Format("{0}{1}:{2}", HASH_START_SHA2, hashedString, saltString);

            return valueToStoreInDatabase;
        }

        /// <summary>
        /// Returns the hash of the input.
        /// </summary>
        /// <param name="input">The value to hash.</param>
        /// <returns>Hashed string.</returns>
        private string ComputeHash(string input)
        {
            // Calculate hash. Make it look pretty.
            HashAlgorithm provider;

            switch (HashingType)
            {
                case HashingTypeEnum.Sha256:
                    provider = new SHA256CryptoServiceProvider();
                    break;
                case HashingTypeEnum.Sha384:
                    provider = new SHA384CryptoServiceProvider();
                    break;
                case HashingTypeEnum.Sha512:
                    provider = new SHA512CryptoServiceProvider();
                    break;
                default:
                    throw new NotImplementedException(
                        "Hashing type '" + HashingType.ToString() + "' is not implemented.");
            }

            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = provider.ComputeHash(inputBytes);

            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "");

            return hashedString;
        }

        #endregion

    }
}