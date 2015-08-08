#region << Usings >>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#endregion

namespace ThatConference.Common.Helpers
{
    /// <summary>
    /// This class works with Enums in general.
    /// </summary>
    public static class EnumHelper
    {

        /// <summary>
        /// Returns a list of Name/Value pairs based upon the given enum.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="exclude">Enums to exclude from the list, if any.</param>
        /// <returns>List of NameValueDto.</returns>
        public static IList<NameValueDto> GetEnumNameValue<T>(params T[] exclude) 
            where T : struct, IConvertible
        {
            List<NameValueDto> result = new List<NameValueDto>();
            List<string> excludes = new List<string>();
            if (exclude != null)
            {
                excludes.AddRange(exclude.Select(item => Enum.GetName(typeof (T), item)));
            }

            foreach (int val in Enum.GetValues(typeof(T)))
            {
                string name = Enum.GetName(typeof(T), val);
                if (!excludes.Contains(name))
                {
                    result.Add(new NameValueDto
                    {
                        Value = val.ToString(CultureInfo.InvariantCulture), 
                        Name = name
                    });
                }
            }
            return result;
        }

    }
}
