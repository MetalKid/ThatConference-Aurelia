#region << Usings >>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ThatConference.Common.Helpers;

#endregion

namespace ThatConference.UI.Web.Mvc.Common.Json
{
    /// <summary>
    /// This class is to ensure we can receive large JSON data from the client because the default is a bit too small.
    /// </summary>
    public sealed class LargeValueProviderFactory : ValueProviderFactory
    {

        #region << ValueProviderFactory Methods >>

        /// <summary>
        /// Returns a value-provider object for the specified controller context.
        /// </summary>
        /// <param name="controllerContext">
        /// An object that encapsulates information about the current HTTP request.
        /// </param>
        /// <returns>A value-provider object.</returns>
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            Guard.IsNotNull(controllerContext, "controllerContext");

            object jsonData = GetDeserializedObject(controllerContext);
            if (jsonData == null)
            {
                return null;
            }

            var bag = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            LoadJsonData(bag, String.Empty, jsonData);
            return new DictionaryValueProvider<object>(bag, CultureInfo.CurrentCulture);
        }

        #endregion

        #region << Helper Methods >>
        /// <summary>
        /// Deserializes the data into a .NET object and reutrns it.
        /// </summary>
        /// <param name="controllerContext">The information about the request.</param>
        /// <returns>.NET object.</returns>
        private static object GetDeserializedObject(ControllerContext controllerContext)
        {
            if (!controllerContext.HttpContext.Request.IsAjaxRequest())
            {
                return null;
            }

            var reader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            string bodyText = reader.ReadToEnd();
            if (string.IsNullOrEmpty(bodyText))
            {
                return null;
            }

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };

            return serializer.DeserializeObject(bodyText);
        }
        
        private static void LoadJsonData(IDictionary<string, object> bag, string prefix, object value)
        {
            // See if value is a dictionary
            var dict = value as IDictionary<string, object>;
            if (dict != null)
            {
                foreach (var entry in dict)
                {
                    LoadJsonData(bag, MakePropertyKey(prefix, entry.Key), entry.Value);
                }
                return;
            }

            // See if value is a list
            var list = value as IList;
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    LoadJsonData(bag, MakeArrayKey(prefix, i), list[i]);
                }
                return;
            }

            // Must be a primitive
            bag[prefix] = value;
        }

        private static string MakeArrayKey(string prefix, int index)
        {
            return string.Format("{0}[{1}]", prefix, index.ToString(CultureInfo.InvariantCulture));
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return (string.IsNullOrEmpty(prefix)) ? propertyName : 
                string.Format(CultureInfo.InvariantCulture, "{0}.{1}", prefix,  propertyName);
        }

        #endregion

    }
}