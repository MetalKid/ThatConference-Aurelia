#region << Usings >>

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ThatConference.Common.Helpers;

#endregion

namespace ThatConference.UI.Web.Mvc.Common.Json
{
    /// <summary>
    /// This class is used to make sure we can send back large JSON files because the default is a bit too small.
    /// </summary>
    /// <remarks>This class is from the web.</remarks>
    public sealed class LargeJsonResult : JsonResult
    {

        #region << Constants >>

        private const string JSON_REQUEST_GET_NOT_ALLOWED = 
            "This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.";

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LargeJsonResult()
        {
            MaxJsonLength = 10000000; //~ 10 MB
            RecursionLimit = 100;
        }

        #endregion

        #region << ExecuteResult >>

        /// <summary>
        /// Serializes the data into JSON.
        /// </summary>
        /// <param name="context">Data to serialize.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            Guard.IsNotNull(context, "context");
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(JSON_REQUEST_GET_NOT_ALLOWED);
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            var serializer = new JavaScriptSerializer();
            if (MaxJsonLength.HasValue)
            {
                serializer.MaxJsonLength = MaxJsonLength.Value;
            }
            if (RecursionLimit.HasValue)
            {
                serializer.RecursionLimit = RecursionLimit.Value;
            }
            response.Write(serializer.Serialize(Data));
        }

        #endregion

    }
}