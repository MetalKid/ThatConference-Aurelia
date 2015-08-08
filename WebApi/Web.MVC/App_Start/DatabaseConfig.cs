#region << Usings >>

using ThatConference.Northwind.Interfaces.Services;
using ThatConference.Common.Helpers;

#endregion

namespace ThatConference.Northwind.Web.Mvc
{
    /// <summary>
    /// Initializes the database(s).
    /// </summary>
    public class DatabaseConfig
    {

        /// <summary>
        /// Initializes all databases.
        /// </summary>
        public static void Initialize()
        {
            using (IoCConfig.Container.BeginScope())
            {
                var service = IoCConfig.Container.TryGetInstance<IDatabaseService>();
                TaskHelper.Run(service.Initialize);
            }
        }

    }
}
