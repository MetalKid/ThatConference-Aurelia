#region << Usings >>

using System;

#endregion

namespace ThatConference.Common.Helpers
{
    /// <summary>
    /// This class is used to ensure incoming parameters are valid.
    /// </summary>
    public static class Guard
    {

        /// <summary>
        /// Throws an ArgumentNullException if the given param is null.
        /// </summary>
        /// <param name="param">The object to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNotNull(object param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if the given param is null or empty string.
        /// </summary>
        /// <param name="param">The string to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNotNullOrEmpty(string param, string paramName)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentException("String value must not be null or empty.", paramName);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if the given param is null, empty string, or white space.
        /// </summary>
        /// <param name="param">The string to check.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNotNullOrWhiteSpace(string param, string paramName)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentException("String value must not be null, empty, or whitespaces only.", paramName);
            }
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if the given value is less than or equal to the check value.
        /// </summary>
        /// <param name="value">The value being inspected.</param>
        /// <param name="check">The value to compare against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThan<T>(T value, T check, string paramName)
            where T : IComparable<T>
        {
            if (value.CompareTo(check) <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    string.Format("Parameter must be greater than {0}.", check));
            }
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if the given value is greater than or equal to the check value.
        /// </summary>
        /// <param name="value">The value being inspected.</param>
        /// <param name="check">The value to compare against.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsLessThan<T>(T value, T check, string paramName)
            where T : IComparable<T>
        {
            if (value.CompareTo(check) >= 0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    string.Format("Parameter must be less than {0}", check));
            }
        }

        /// <summary>
        /// Throws an ArgumentException if the given statement is false.
        /// </summary>
        /// <param name="isValid">Throws an exception if this is false.</param>
        /// <param name="message">The message in the exception..</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsTrue(bool isValid, string message, string paramName)
        {
            if (!isValid)
            {
                throw new ArgumentException(message, paramName);
            }
        }

    }
}
