#region << Usings >>

using System;
using System.Threading.Tasks;
using ThatConference.Common.Logging;

#endregion

namespace ThatConference.Common.Helpers
{
    /// <summary>
    /// This class contains methods that aid in asynchronous Task programming.
    /// </summary>
    public static class TaskHelper
    {

        /// <summary>
        /// Handles logging any Task exceptions that occur.
        /// </summary>
        /// <param name="task">The task that has an error.</param>
        /// <param name="context">Information about where the task ran (i.e. method name).</param>
        public static void HandleTaskErrors(Task task, string context)
        {
            if (task.Exception != null)
            {
                Logger.LogError(context, task.Exception);
            }
            else if (task.IsCanceled)
            {
                Logger.LogError(context + " - The task was cancelled.");
            }
        }

        /// <summary>
        /// Returns a task with automatic error handling.
        /// </summary>
        /// <param name="action">The code to run on a new thread.</param>
        /// <param name="context">Information about what is running, if any.</param>
        /// <returns>Task with automatic error handling.</returns>
        public static Task Run(Action action, string context = null)
        {
            return Task.Run(action).ContinueWith(task => HandleTaskErrors(task, context));
        }

    }
}