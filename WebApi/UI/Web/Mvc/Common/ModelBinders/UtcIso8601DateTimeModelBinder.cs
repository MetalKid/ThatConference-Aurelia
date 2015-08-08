#region << Usings >>

using System;
using System.Globalization;
using System.Web.Mvc;

#endregion

namespace ThatConference.UI.Web.Mvc.Common.ModelBinders
{
    /// <summary>
    /// ModelBinder that will turn both DateTime values and strings containing date/time
    /// information in ISO-8601 notation into a UTC kind DateTime.
    /// </summary>
    public class UtcIso8601DateTimeModelBinder : DefaultModelBinder
    {
    
        /// <summary>
        /// Binds the model by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">
        /// The context within which the controller operates. The context information
        /// includes the controller, HTTP content, request context, and route data.
        /// </param>
        /// <param name="bindingContext">
        /// The context within which the model is bound. The context includes information
        /// such as the model object, model name, model type, property filter, and value provider.
        /// </param>
        /// <returns>The bound object.</returns>
        /// <exception cref="System.ArgumentNullException">The bindingContext parameter is null.</exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            // If input value is already of type DateTime, convert to UTC Kind and return.
            if (value.RawValue is DateTime)
            {
                return ((DateTime) value.RawValue).ToUniversalTime();
            }

            // Try to parse a date-time, assuming it is a ISO 8601 formatted string.
            // For "s" / "u" explanation, 
            // see http://msdn.microsoft.com/de-de/library/system.globalization.datetimeformatinfo(v=vs.110).aspx#properties
            DateTime result;

            var formatStrings = new []
            {
                "s",
                "u", // Produced by kendo.ToString("u") on client side
                "yyyy-MM-ddTHH:mm:ss.fffZ",  // Produced by JavaScript datetype to JSON, e.G. JSON.stringify(new Date())
                "yyyy-MM-ddTHH:mm:ssZ"  // ...because IE8 is a special child.
            };

            if (!DateTime.TryParseExact(value.AttemptedValue, formatStrings,CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out result))
            {
                return null;
            }

            result = result.ToUniversalTime();
            return result;
        }
    }
}
