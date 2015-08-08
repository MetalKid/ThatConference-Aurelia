#region << Usings >>

using System;
using System.Web.Mvc;
using ThatConference.Common.Exceptions;

#endregion

namespace ThatConference.UI.Web.Mvc.Common.Attributes
{
    /// <summary>
    /// This attribute is used to ensure that the non-get is being sent by the original user.
    /// </summary>
    public class VerifySessionTokenAttribute: FilterAttribute, IActionFilter
    {

        #region << Properties >>

        /// <summary>
        /// Gets the error message to display when the session token does not match.
        /// </summary>
        public virtual string ErrorMessage
        {
            get { return "Your session has expired. Please refresh the current page in your browser."; }
        }

        #endregion

        #region << IActionFilter Methods >>

        /// <summary>
        /// Determines if the request's token is valid to avoid cross-side attacks.  
        /// Throws an exception if they do not match.  This only applies to non-GET requests.
        /// </summary>
        /// <param name="filterContext">The information about the current request.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || 
                filterContext.RequestContext == null ||
                filterContext.RequestContext.HttpContext == null ||
                filterContext.RequestContext.HttpContext.Session == null ||
                filterContext.RequestContext.HttpContext.Request == null ||
                filterContext.RequestContext.HttpContext.Request.Url == null ||
                string.Equals(
                    filterContext.RequestContext.HttpContext.Request.HttpMethod,
                    "GET",
                    StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request.Url == null)
            {
                return;
            }
            string previousDomain = null;
            if (request.UrlReferrer != null)
            {
                previousDomain = request.UrlReferrer.Authority;
            }

            // Only non-GETs need to be checked for the token
            string token =
                request.Headers[WebConstants.SESSION_TOKEN] ?? 
                request.Form[WebConstants.SESSION_TOKEN] ?? 
                request.QueryString[WebConstants.SESSION_TOKEN];

            string currentDomain = request.Url.Authority;
            if (token == null && currentDomain != previousDomain)
            {
                return;
            }

            var session = filterContext.RequestContext.HttpContext.Session;
            if (token != session[WebConstants.SESSION_TOKEN] as string)
            {
                // Invalid request
                throw new UserFriendlyException(ErrorMessage);
            }
        }

        /// <summary>
        /// Not used or needed.
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Nothing to do
        }

        #endregion

    }
}