#region << Usings >>

using System.Web.Http;
using LightInject;
using ThatConference.Northwind.Common;
using ThatConference.Northwind.Web.Mvc.Helpers;

#endregion

namespace ThatConference.Northwind.Web.Mvc
{
    public static class IoCConfig
    {

        #region << Variables >>

        private static readonly ServiceContainer _container = new ServiceContainer();

        #endregion

        #region << Properties >>

        /// <summary>
        /// Gets the IoC container that holds all of the regustered interfaces.
        /// </summary>
        public static ServiceContainer Container { get { return _container; } }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// Configures IoC for use throughout the application for Services and Repositories.
        /// </summary>
        public static void Register()
        {
            _container.RegisterAssembly(AppSettings.RepositoryAssembly + "*.dll");
            _container.RegisterAssembly(AppSettings.ServiceAssembly + "*.dll");
            _container.Register(factory => ScopeKeyHelper.GetScopeKey());
        }

        /// <summary>
        /// Configures IoC for use for Mvc type classes.
        /// </summary>
        public static void RegisterMvc()
        {
            _container.RegisterControllers();
            _container.RegisterApiControllers();
            _container.EnableMvc();
            _container.EnableWebApi(GlobalConfiguration.Configuration);
        }

        #endregion

    }
}