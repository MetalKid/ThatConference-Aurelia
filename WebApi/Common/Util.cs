#region << Usings >>

using System.IO;
using System.Text;

// NuGet Package (SharpSerializer) - Allows XML Serialization of Dictionaries and other complex types
using Polenter.Serialization; 

#endregion

namespace ThatConference.Common
{
    /// <summary>
    /// This class contains various methods that support other functions throughout the program.
    /// </summary>
    public static class Util
    {

        #region << Serialization Methods >>

        /// <summary>
        /// Returns the XML representation of the given object.
        /// </summary>
        /// <param name="entity">The object to serialize into XML.</param>
        /// <returns>XML String representation of object.</returns>
        public static string SerializeToXml(object entity)
        {
            if (entity == null) return string.Empty;

            string result;

            var settings = new SharpSerializerXmlSettings
            {
                IncludeAssemblyVersionInTypeName = true,
                IncludeCultureInTypeName = true,
                IncludePublicKeyTokenInTypeName = true
            };
            SharpSerializer ser = new SharpSerializer(settings);
            using (MemoryStream memStream = new MemoryStream())
            {
                using (var sw = new StreamWriter(memStream))
                {
                    ser.Serialize(entity, memStream);
                    sw.Flush();

                    memStream.Position = 0;
                    using (var sr = new StreamReader(memStream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the fully rehydrated object based upon the given XML.
        /// </summary>
        /// <param name="xml">The xml that represents object.</param>
        /// <returns>Fully rehydrated object.</returns>
        public static object DeserializeFromXml(string xml)
        {
            using (var memStream = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
            {
                var settings = new SharpSerializerXmlSettings
                {
                    IncludeAssemblyVersionInTypeName = true,
                    IncludeCultureInTypeName = true,
                    IncludePublicKeyTokenInTypeName = true
                };
                SharpSerializer ser = new SharpSerializer(settings);
                return ser.Deserialize(memStream);
            }
        }

        /// <summary>
        /// Returns the byte array representation of the object.
        /// </summary>
        /// <param name="entity">The object to serialize.</param>
        /// <returns>Byte array representation of the object.</returns>
        public static byte[] SerializeToBinary(object entity)
        {
            byte[] result;

            var settings = new SharpSerializerBinarySettings
            {
                IncludeAssemblyVersionInTypeName = true,
                IncludeCultureInTypeName = true,
                IncludePublicKeyTokenInTypeName = true
            };
            SharpSerializer ser = new SharpSerializer(settings);
            using (MemoryStream memStream = new MemoryStream())
            {
                var sw = new BinaryWriter(memStream);
                ser.Serialize(entity, memStream);
                sw.Flush();

                memStream.Position = 0;
                var sr = new BinaryReader(memStream);
                result = sr.ReadBytes((int)memStream.Length);
            }

            return result;
        }

        /// <summary>
        /// Returns the fully rehydrated object based upon the given byte array.
        /// </summary>
        /// <param name="data">The byte array that represents the object.</param>
        /// <returns>Fully rehydrated object.</returns>
        public static object DeserializeFromBinary(byte[] data)
        {
            using (var memStream = new MemoryStream(data))
            {
                var settings = new SharpSerializerBinarySettings
                {
                    IncludeAssemblyVersionInTypeName = true,
                    IncludeCultureInTypeName = true,
                    IncludePublicKeyTokenInTypeName = true
                };
                SharpSerializer ser = new SharpSerializer(settings);
                return ser.Deserialize(memStream);
            }
        }

        #endregion

    }
}