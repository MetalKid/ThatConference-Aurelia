#region << Usings >>

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Common.Security
{
      /// <summary>
    /// This class does two-way encryption with RijndaelManaged.
    /// </summary>
    public class AesTwoWayEncryption : ITwoWayEncryption
    {

        //Note: This is an example of the Key/Vector you need to send in.
        // These can be anything you desire but must be less than or equal to 255
        //private byte[] Key = 
        //{ 
        //    0, 0, 0, 0, 0, 0, 0, 0, 
        //    0, 0, 0, 0, 0, 0, 0, 0, 
        //    0, 0, 0, 0, 0, 0, 0, 0, 
        //    0, 0, 0, 0, 0, 0, 0, 0 
        //};
        //private byte[] Vector = 
        //{ 
        //    0, 0, 0, 0, 0, 0, 0, 0,
        //    0, 0, 0, 0, 0, 0, 0, 0 
        //};

        #region << ITwoWayEncryption Methods >>

        /// <summary>
        /// Returns a randomly generated encyrption key.
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateEncryptionKey()
        {
            //Generate a Key. 
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }

        /// <summary>
        /// Returns a randomly generated vector.
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateEncryptionVector()
        {
            //Generate a Vector 
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }

        /// <summary>
        /// Encrypts the value.
        /// </summary>
        /// <param name="textValue">The value to encrypt.</param>
        /// <param name="key">The key to encrypt the value with.</param>
        /// <param name="vector">The vector to encrypt the value with.</param>
        /// <returns>An encrypted string.</returns>
        public string EncryptToString(string textValue, byte[] key, byte[] vector)
        {
            return Convert.ToBase64String(Encrypt(textValue, key, vector));
        }

        /// <summary>
        /// Decrypts the value.
        /// </summary>
        /// <param name="encryptedValue">The encrypted value.</param>
        /// <param name="key">The key that was used to encrypt the original value with.</param>
        /// <param name="vector">The vectory that was used to encyrpt the original value with.</param>
        /// <returns>A decrypted string.</returns>
        public string DecryptString(string encryptedValue, byte[] key, byte[] vector)
        {
            return Decrypt(Convert.FromBase64String(encryptedValue), key, vector);
        }

        #endregion

        #region << Helper Methods >>
        
        /// <summary>
        /// Encrypt some text and return an encrypted byte array. 
        /// </summary>
        /// <param name="textValue">The value to encrypt.</param>
        /// <param name="key">The key to encrypt the value with.</param>
        /// <param name="vector">The vector to encrypt the value with.</param>
        /// <returns>An encrypted string.</returns>
        private byte[] Encrypt(string textValue, byte[] key, byte[] vector)
        {
            RijndaelManaged rm = new RijndaelManaged();
            ICryptoTransform encryptorTransform = rm.CreateEncryptor(key, vector);

            Byte[] bytes = Encoding.UTF8.GetBytes(textValue);
            byte[] encrypted;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(memoryStream, encryptorTransform, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();

                    memoryStream.Position = 0;
                    encrypted = new byte[memoryStream.Length];
                    memoryStream.Read(encrypted, 0, encrypted.Length);
                }
            }

            return encrypted;
        }

        /// <summary>
        /// Decryption when working with byte arrays.     
        /// </summary>
        /// <param name="encryptedValue">The encrypted value.</param>
        /// <param name="key">The key that was used to encrypt the original value with.</param>
        /// <param name="vector">The vectory that was used to encyrpt the original value with.</param>
        /// <returns>A decrypted string.</returns>
        /// <returns></returns>
        private string Decrypt(byte[] encryptedValue, byte[] key, byte[] vector)
        {
            RijndaelManaged rm = new RijndaelManaged();
            ICryptoTransform decryptorTransform = rm.CreateDecryptor(key, vector);

            Byte[] decryptedBytes;
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                using (CryptoStream decryptStream = new CryptoStream(encryptedStream, decryptorTransform, CryptoStreamMode.Write))
                {
                    decryptStream.Write(encryptedValue, 0, encryptedValue.Length);
                    decryptStream.FlushFinalBlock();

                    encryptedStream.Position = 0;
                    decryptedBytes = new Byte[encryptedStream.Length];
                    encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                }
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        #endregion

    }
}