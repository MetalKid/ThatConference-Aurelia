#region << Usings >>

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;
using ThatConference.Common.Enums;
using ThatConference.Common.Exceptions;
using ThatConference.Common.Helpers;
using ThatConference.Common.Interfaces;
using ThatConference.Common.Logging;
using ThatConference.UI.Web.Mvc.Common.Attributes;
using ThatConference.UI.Web.Mvc.Common.Json;

#endregion

namespace ThatConference.UI.Web.Mvc.Common
{
    /// <summary>
    /// This class should be the base controller all Web MVC controllers should inherit.
    /// </summary>
    [VerifySessionToken]
    public class ThatConferenceControllerBase : Controller
    {

        #region << Properties >>

        /// <summary>
        /// Gets the manager of the instances that were IoC injected into this service.
        /// </summary>
        protected InstanceManager Instances { get; private set; }

        /// <summary>
        /// Returns the message the user sees when a Concurrency error happens.
        /// </summary>
        public virtual string ConcurrencyErrorMessage
        {
            get { return "Another user has made a change to this record. Please refresh the page and try again."; }
        }

        /// <summary>
        /// Returns the message the user sees when a Concurrency error happens.
        /// </summary>
        public virtual string GenericErrorMessage
        {
            get { return "An unexpected error has occurred.  Please try again."; }
        }

        /// <summary>
        /// Gets the title of the message the user sees during a successful save.
        /// </summary>
        public virtual string SaveSuccessTile
        {
            get { return "Save Successful"; }
        }

        /// <summary>
        /// Gets the title of the message the user sees during a failed save.
        /// </summary>
        public virtual string SaveFailedTile
        {
            get { return "Save Failed"; }
        }

        /// <summary>
        /// Gets the title of the message the user sees during a successful delete.
        /// </summary>
        public virtual string DeleteSuccessTile
        {
            get { return "Delete Successful"; }
        }

        /// <summary>
        /// Gets the title of the message the user sees during a failed delete.
        /// </summary>
        public virtual string DeleteFailedTile
        {
            get { return "Delete Failed"; }
        }

        #endregion

        #region << Constructors >>

        /// <summary>
        /// Constructor that takes in all dependencies.
        /// </summary>
        /// <param name="instances">Dependencies of this class.</param>
        public ThatConferenceControllerBase(params object[] instances)
        {
            Instances = InstanceManager.Create(instances);
        }

        #endregion

        #region << Ajax Methods >>

        /// <summary>
        /// Replaces the normal Json with the LargeJsonResult to ensure the maximum data can be sent to the user.
        /// </summary>
        /// <param name="data">Data being serialized</param>
        /// <param name="contentType">The type of data.</param>
        /// <param name="contentEncoding">The encoding of the data.</param>
        /// <returns>JsonResult.</returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Replaces the normal Json with the LargeJsonResult to ensure the maximum data can be sent to the user.
        /// </summary>
        /// <param name="data">Data being serialized</param>
        /// <param name="contentType">The type of data.</param>
        /// <param name="contentEncoding">The encoding of the data.</param>
        /// <param name="behavior">The type of JSON behavior (allow Get or not).</param>
        /// <returns>JsonResult.</returns>
        protected override JsonResult Json(
            object data,
            string contentType,
            Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new LargeJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #endregion

        #region << Result Methods >>

        /// <summary>
        /// Runs code with common Result/Exception code being handled automatically.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="successMessage">The success message to show.</param>
        /// <param name="failureMessage">The failure message to show.</param>
        /// <param name="minRowsAffected">
        /// The minimum number of rows that should be affected in order to be considered a success.
        /// </param>
        /// <param name="parameters">The parameters that were sent into the calling method.</param>
        /// <returns>The result.</returns>
        protected T RunSave<T>(
            Func<T> code,
            string successMessage,
            string failureMessage,
            int minRowsAffected,
            params object[] parameters) where T : IResult
        {
            T result = Run(code, parameters);
            ApplySaveResultMessage(result, successMessage, failureMessage, minRowsAffected);
            return result;
        }

        /// <summary>
        /// Runs code with common Result/Exception code being handled automatically.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="successMessage">The success message to show.</param>
        /// <param name="failureMessage">The failure message to show.</param>
        /// <param name="minRowsAffected">
        /// The minimum number of rows that should be affected in order to be considered a success.
        /// </param>
        /// <param name="parameters">The parameters that were sent into the calling method.</param>
        /// <returns>The result.</returns>
        protected async Task<T> RunSaveAsync<T>(
            Func<Task<T>> code,
            string successMessage,
            string failureMessage,
            int minRowsAffected,
            params object[] parameters) where T : IResult
        {
            T result = await RunAsync(code, parameters);
            ApplySaveResultMessage(result, successMessage, failureMessage, minRowsAffected);
            return result;
        }

        /// <summary>
        /// Runs code with common Result/Exception code being handled automatically.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="successMessage">The success message to show.</param>
        /// <param name="failureMessage">The failure message to show.</param>
        /// <param name="minRowsAffected">
        /// The minimum number of rows that should be affected in order to be considered a success.
        /// </param>
        /// <param name="parameters">The parameters that were sent into the calling method.</param>
        /// <returns>The result.</returns>
        protected T RunDelete<T>(
            Func<T> code,
            string successMessage,
            string failureMessage,
            int minRowsAffected,
            params object[] parameters) where T : IResult
        {
            T result = Run(code, parameters);
            ApplyDeleteResultMessage(result, successMessage, failureMessage, minRowsAffected);
            return result;
        }

        /// <summary>
        /// Runs code with common Result/Exception code being handled automatically.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="successMessage">The success message to show.</param>
        /// <param name="failureMessage">The failure message to show.</param>
        /// <param name="minRowsAffected">
        /// The minimum number of rows that should be affected in order to be considered a success.
        /// </param>
        /// <param name="parameters">The parameters that were sent into the calling method.</param>
        /// <returns>The result.</returns>
        protected async Task<T> RunDeleteAsync<T>(
            Func<Task<T>> code,
            string successMessage,
            string failureMessage,
            int minRowsAffected,
            params object[] parameters) where T : IResult
        {
            T result = await RunAsync(code, parameters);
            ApplyDeleteResultMessage(result, successMessage, failureMessage, minRowsAffected);
            return result;
        }

        /// <summary>
        /// Runs code with common Result/Exception code being handled automatically.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="parameters">The parameters that were sent into the calling method.</param>
        /// <returns>The result.</returns>
        protected virtual T Run<T>(Func<T> code, params object[] parameters) where T : IResult
        {
            T result = default(T);
            try
            {
                result = code();
                Verify(result);
            }
            catch (Exception ex)
            {
                if (!result.LoggedExceptions.Contains(ex))
                {
                    result.HandledExceptions.Add(ex);
                }
            }
            finally
            {
                Verify(result);
            }

            return result;
        }

        /// <summary>
        /// Runs code with common Result/Exception code being handled automatically.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="code">The anonymous method to call to run the business related logic.</param>
        /// <param name="parameters">The parameters that were sent into the calling method.</param>
        /// <returns>The result.</returns>
        protected async virtual Task<T> RunAsync<T>(Func<Task<T>> code, params object[] parameters) where T : IResult
        {
            T result = default(T);
            try
            {
                result = await code();
                Verify(result);
            }
            catch (Exception ex)
            {
                if (!result.LoggedExceptions.Contains(ex))
                {
                    result.HandledExceptions.Add(ex);
                }
            }
            finally
            {
                Verify(result);
            }

            return result;
        }

        /// <summary>
        /// Verifies that the result is successful; if not, all exceptions are logged and a general message
        /// is returned to the user.
        /// </summary>
        /// <param name="result">The result object to inspect.</param>
        protected virtual void Verify(IResult result)
        {
            if (result == null)
            {
                return;
            }

            foreach (var ex in result.HandledExceptions.Except(result.LoggedExceptions))
            {
                LogException(ex);
                result.LoggedExceptions.Add(ex);
            }

            result.Verify(ConcurrencyErrorMessage);

            if (!result.IsSuccessful)
            {
                throw new UserFriendlyException(GenericErrorMessage);
            }
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <typeparam name="T">The type of exception.</typeparam>
        /// <param name="ex">The exception being logged.</param>
        /// <param name="message">An optional message to log with the exception.</param>
        protected virtual void LogException<T>(T ex, string message = null) where T : Exception
        {
            Logger.LogError(message, ex);
        }

        /// <summary>
        /// Applies settings to show results to the user when a insert or update occurs.
        /// </summary>
        /// <param name="result">The object holding the data.</param>
        /// <param name="successMessage">The success message to show.</param>
        /// <param name="failureMessage">The failure message to show.</param>
        /// <param name="minRowsAffected">
        /// The minimum number of rows that should be affected in order to be considered a success.
        /// </param>
        protected void ApplySaveResultMessage(
            IResult result,
            string successMessage,
            string failureMessage,
            int minRowsAffected)
        {
            ApplyResultMessage(result, SaveSuccessTile, successMessage, SaveFailedTile, failureMessage, minRowsAffected);
        }

        /// <summary>
        /// Applies settings to show results to the user when a delete occurs.
        /// </summary>
        /// <param name="result">The object holding the data.</param>
        /// <param name="successMessage">The success message to show.</param>
        /// <param name="failureMessage">The failure message to show.</param>
        /// <param name="minRowsAffected">
        /// The minimum number of rows that should be affected in order to be considered a success.
        /// </param>
        protected void ApplyDeleteResultMessage(
            IResult result,
            string successMessage,
            string failureMessage,
            int minRowsAffected)
        {
            ApplyResultMessage(
                result,
                DeleteSuccessTile,
                successMessage,
                DeleteFailedTile,
                failureMessage,
                minRowsAffected);
        }

        /// <summary>
        /// Applies settings to show results to the user.
        /// </summary>
        /// <param name="result">The object holding the data.</param>
        /// <param name="successTitle">The title of the success message to show.</param>
        /// <param name="successMessage">The success message to show.</param>
        /// <param name="failureTitle">The title of the failure message to show.</param>
        /// <param name="failureMessage">The failure message to show.</param>
        /// <param name="minRowsAffected">
        /// The minimum number of rows that should be affected in order to be considered a success.
        /// </param>
        protected void ApplyResultMessage(
            IResult result,
            string successTitle,
            string successMessage,
            string failureTitle,
            string failureMessage,
            int minRowsAffected)
        {
            bool isSuccess = result.IsSuccessful && result.RowsAffected >= minRowsAffected;
            if (isSuccess)
            {
                result.MessageTitle = successTitle;
                result.MessageText = successMessage;
                result.MessageIconType = MessageIconTypeEnum.Success;
            }
            else
            {
                result.MessageTitle = failureTitle;
                result.MessageText = failureMessage;
                result.MessageIconType = MessageIconTypeEnum.Failure;
            }
        }

        #endregion

        #region << Mobile Methods >>

        /// <summary>
        /// Returns whether the current request is for a Mobile page.
        /// </summary>
        /// <returns></returns>
        protected bool IsMobileRequest()
        {
            bool isMobile = Request.Browser.IsMobileDevice;

            if (Request.UserAgent != HttpContext.GetOverriddenUserAgent()) // Browser is overridden
            {
                isMobile = HttpContext.GetOverriddenBrowser().IsMobileDevice;
            }

            return isMobile;
        }

        #endregion

        #region << View Rendering Methods >>

        /// <summary>
        /// Returns the HTML of the rendered view. 
        /// </summary>
        /// <param name="viewName">The name of the view to render.</param>
        /// <param name="model">The data to render the view with.</param>
        /// <remarks>Should only use this for returning HTML to a jQuery call.</remarks>
        /// <returns>View HTML.</returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion

    }
}
