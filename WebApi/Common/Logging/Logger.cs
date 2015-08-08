#region << Usings >>

using System;
using System.IO;
using log4net;

#endregion

namespace ThatConference.Common.Logging
{
    /// <summary>
    /// This class helps log data via Log4Net.
    /// </summary>
    public static class Logger
    {

        #region << Variables >>

        private static ILog _logger;

        #endregion

        #region << Properties >>
        
        /// <summary>
        /// Gets the logging instance.
        /// </summary>
        private static ILog Log
        {
            get
            {
                if (_logger != null)
                {
                    return _logger;
                }

                _logger = LogManager.GetLogger("File");
                return _logger;
            }
        }

        #endregion

        #region << Log Metohds >>
        
        /// <summary>
        /// Initializes the log4net logger.
        /// </summary>
        /// <param name="filePath">The file path of the configuration to load.</param>
        public static void Initialize(string filePath)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(filePath));
        }

        /// <summary>
        /// Log error message to the default Trace listener setup for application.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogError(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Log error message to the default Trace listener setup for application.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="ex">The exception to log.</param>
        public static void LogError(string message, Exception ex)
        {
            Log.Error(message, ex);
        }

        /// <summary>
        /// Log informational message to the default Trace listener setup for application
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogInfo(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// Log Warning message to the default Trace listener setup for application
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogWarning(string message)
        {
            Log.Warn(message);
        }

        /// <summary>
        /// Log Debug message to the default Trace listener setup for application
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogVerbose(string message)
        {
            Log.Debug(message);
        }

        #endregion

    }
}