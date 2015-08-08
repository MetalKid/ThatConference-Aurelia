#region << Usings >>

using System;

#endregion

namespace ThatConference.Data.Common.Attributes
{
    /// <summary>
    /// This class allows precision and scale to be assigned to Entity Framework properties via Data Annotations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets the total number of digits available.
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// Gets or sets the number of digits to the right of the decimal point.
        /// </summary>
        public byte Scale { get; set; }

        #endregion

        #region << Cosntructors >>

        /// <summary>
        /// Constructor that sets precision and scale.
        /// </summary>
        /// <param name="precision">The total number of digits available.</param>
        /// <param name="scale">The number of decimals to the right of the decimal point.</param>
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }

        #endregion

    }
}