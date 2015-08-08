#region << Usings >>

using System.Configuration;

#endregion

namespace ThatConference.Northwind.Common
{
    /// <summary>
    /// This class is the gatekeeper to all the AppSettings in the configuration file. 
    /// </summary>
    /// <remarks>
    /// Putting AppSetting references in a class allows default values 
    /// and no need for constants for the key names since they are only used once.
    /// </remarks>
    public static class AppSettings
    {

        /// <summary>
        /// Gets the connection string to use for Northwind.
        /// </summary>
        public static string NorthwindConnStringName
        {
            get { return ConfigurationManager.AppSettings["NorthwindConnStringName"]; }
        }

        /// <summary>
        /// Gets the assembly name to load StructureMap with services.
        /// </summary>
        public static string ServiceAssembly
        {
            get { return ConfigurationManager.AppSettings["ServiceAssembly"]; }
        }

        /// <summary>
        /// Gets the assembly name to load StructureMap with repository.
        /// </summary>
        public static string RepositoryAssembly
        {
            get { return ConfigurationManager.AppSettings["RepositoryAssembly"]; }
        }

    }
}