#region << Usings >>

using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using LightInject;
using ThatConference.Common.Interfaces;
using ThatConference.Data.Northwind;
using ThatConference.Data.Northwind.Interfaces;
using ThatConference.Northwind.Common;
using ThatConference.Northwind.Data.Repositories.IoC;

#endregion

[assembly: CompositionRootType(typeof(IocInitialize))]
namespace ThatConference.Northwind.Data.Repositories.IoC
{
    /// <summary>
    /// This class initializes all interfaces and their instances with the IoC implementation.
    /// </summary>
    public class IocInitialize : ICompositionRoot
    {
     
        /// <summary>
        /// Composes services by adding services to the serviceRegistry.
        /// </summary>
        /// <param name="serviceRegistry">The target LightInject.IServiceRegistry.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<INorthwindContext>(
                factory =>
                    new NorthwindContext(
                        ConfigurationManager.ConnectionStrings[AppSettings.NorthwindConnStringName].ConnectionString));

            // Register all classes the implement the IIoC and register that interface for all classes
            Type iocCheck = typeof(IIoC);
            foreach (var type in Assembly.GetAssembly(GetType()).GetTypes().Where(a => !a.IsAbstract))
            {
                if (!iocCheck.IsAssignableFrom(type))
                {
                    continue;
                }

                var interfaces = type.GetInterfaces();
                foreach (var item in interfaces.Where(item => item != iocCheck))
                {
                    serviceRegistry.Register(item, type, new PerContainerLifetime());
                }
            }
        }

    }
}
