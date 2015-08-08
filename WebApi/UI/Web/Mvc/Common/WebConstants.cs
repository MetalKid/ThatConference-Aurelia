namespace ThatConference.UI.Web.Mvc.Common
{
    /// <summary>
    /// This class defines constants to be used by a web project.
    /// </summary>
    public static class WebConstants
    {

        /// <summary>
        /// Defines constant to see if the exception is allowed to go through.
        /// </summary>
        public const string ALLOW_EXCEPTION = "AllowException";

        /// <summary>
        /// Defines the constant to lookup the error that previously happened.
        /// </summary>
        public const string ERROR_DATA = "ErrorData";

        /// <summary>
        /// Defines the constant to lookup the Token in Session.
        /// </summary>
        public const string SESSION_TOKEN = "Session-Token";

        /// <summary>
        /// Defines the constant to lookup the Last Elmah Id in Session.
        /// </summary>
        public const string SESSION_KEY_LAST_ELMAH_ID = "LastElmahId";

        /// <summary>
        /// Defines the constant to lookup the Scope Key in Session.
        /// </summary>
        public const string SESSION_KEY_SCOPE = "ScopeKey";

    }
}