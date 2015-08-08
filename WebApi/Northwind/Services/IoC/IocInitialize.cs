#region << Usings >>

using System;
using System.Linq;
using System.Reflection;
using LightInject;
using ThatConference.Common.Interfaces;
using ThatConference.Northwind.Common.Interfaces;
using ThatConference.Northwind.Services.Helpers;
using ThatConference.Northwind.Services.IoC;

#endregion

[assembly: CompositionRootType(typeof(IocInitialize))]
namespace ThatConference.Northwind.Services.IoC
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
            serviceRegistry.Register(factory => 
                ScopeHelper.GetScope(factory.GetInstance<INorthwindScopeKey>()));

            // Register all classes the implement the IIoC and register that interface for all classes
            Type iocCheck = typeof(IIoC);
            Type iocGroupCheck = typeof(IIoCGroup);
            foreach (var type in Assembly.GetAssembly(GetType()).GetTypes().Where(a => !a.IsAbstract))
            {
                if (!iocCheck.IsAssignableFrom(type))
                {
                    continue;
                }

                var interfaces = type.GetInterfaces();
                foreach (var item in interfaces.Where(item => item != iocCheck && item != iocGroupCheck))
                {
                    serviceRegistry.Register(item, type);
                }
            }
        }
    }
}
