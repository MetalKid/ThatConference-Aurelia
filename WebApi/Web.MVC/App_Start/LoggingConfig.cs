#region << Usings >>

using ThatConference.Common.Logging;

#endregion

namespace ThatConference.Northwind.Web.Mvc
{
    public static class LoggingConfig
    {

        /// <summary>
        /// Configures all logging methods.
        /// </summary>
        /// <param name="log4NetConfigFilePath">The path to the file containing the log4net configuration Xml.</param>
        public static void Register(string log4NetConfigFilePath)
        {
            Logger.Initialize(log4NetConfigFilePath);
        }

    }
}
