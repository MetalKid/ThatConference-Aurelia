#region << Usings >>

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using ThatConference.Common.Exceptions;
using ThatConference.Common.Logging;
using ThatConference.Common.Validation;
using ThatConference.Northwind.Common;
using ThatConference.UI.Web.Mvc.Common;

#endregion

namespace ThatConference.Northwind.Web.Mvc.Attributes
{
    /// <summary>
    /// Use RedirectAction and RedirectController
    /// to set which action should be redirected to on exception.  If not set, will redirect back to
    /// the action that had the exception.  In both cases, all querystring parameters
    /// on the action that had the exception will be passed.
    /// </summary>
    public class HandleExceptionAttribute : ExceptionFilterAttribute 
    {

        #region << Properties >>

        /// <summary>
        /// Gets or sets the Action of the controller that was called.
        /// </summary>
        public string RedirectAction { get; set; }

        /// <summary>
        /// Gets or sets the name of the Controller that was called.
        /// </summary>
        public string RedirectController { get; set; }

        #endregion

        #region << Overridden Metohds >>

        public override void OnException(HttpActionExecutedContext ec)
        {
            if (ec.Exception == null)
            {
                return;
            }

            //if the exception is the result of bad user input we don't want to
            //log it - for all other cases we do
            var type = ec.Exception.GetType();
            HandleJsonException(ec);
        }

        #endregion

        #region << Helper Methods >>

        private static void HandleJsonException(HttpActionExecutedContext ec)
        {
            ErrorDto data = new ErrorDto();

            // Different data depending on exception type
            var exceptionType = ec.Exception.GetType();
            if (exceptionType == typeof(BrokenRuleException))
            {
                data.BrokenRules = GetDistinctBrokenRules(((BrokenRuleException) ec.Exception).BrokenRules);
            }
            else if (exceptionType == typeof(UserFriendlyException))
            {
                data.Message = ec.Exception.Message.Replace("\r\n", "<br/>");
            }
            else
            {
                data.Message = string.Format("An unexpected error has occurred. Please try agian.");
            }

            ec.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content =
                    new ObjectContent<ErrorDto>(
                        data,
                        new JsonMediaTypeFormatter() {UseDataContractJsonSerializer = false})
            };
        }

        private class ErrorDto
        {
            public List<BrokenRule> BrokenRules { get; set; }
            public string Message { get; set; }
        }

        private static List<BrokenRule> GetDistinctBrokenRules(IEnumerable<BrokenRule> rules)
        {
            List<BrokenRule> result = new List<BrokenRule>();

            foreach (var rule in rules)
            {
                if (
                    result.Count(
                        a => a.RuleMessage == rule.RuleMessage && a.Relation == rule.Relation && a.Id == rule.Id) == 0)
                {
                    result.Add(rule);
                }
            }

            return result;
        }

        #endregion

    }
}