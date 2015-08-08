#region << Usings >>

using System;
using System.Web;
using ThatConference.Common.Logging;

#endregion

namespace ThatConference.Northwind.Web.Mvc.Helpers
{
    /// <summary>
    /// This helper class is used to catch any errors that happen during an Elmah log which will then
    /// use Log4Net as a backup.
    /// </summary>
    internal static class LoggingHelper
    {

        /// <summary>
        /// Attempts to log the given exception.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        /// <param name="customMessage">A custom mesesage to log with the exception.</param>
        public static void LogError(Exception ex, string customMessage = null)
        {
            if (!string.IsNullOrWhiteSpace(customMessage))
            {
                ex = new Exception(customMessage, ex);
            }
            try
            {
                var ctx = HttpContext.Current;
                if (ctx != null)
                {
                    Elmah.ErrorSignal.FromContext(ctx).Raise(ex);
                }
                else
                {
                    Elmah.ErrorSignal.Get(null).Raise(ex);
                }
            }
            catch (Exception elmahEx)
            {
                Logger.LogError("Elmah failed to log due to this exception:", elmahEx);
                Logger.LogError("Elmah was trying to log this error:", ex);
            }
        }

    }
}