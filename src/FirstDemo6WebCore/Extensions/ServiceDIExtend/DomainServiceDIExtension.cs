using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.Extensions.ServiceDIExtend
{
    /// <summary>
    /// 
    /// </summary>
    public static class DomainServiceDIExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddServiceDI(this IServiceCollection services, Type baseInterfaceType, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var refAssembyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (var asslembyNames in refAssembyNames)
            {
                Assembly.Load(asslembyNames);
            }
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()?.ToList();
            var allInterfaces = new List<Type>();
            foreach (var assembly in assemblies)
            {
                allInterfaces.AddRange(assembly.GetTypes().Where(t => t.IsInterface)?.ToList());                
            }
            var derivedInterfaces = allInterfaces.Where(i => baseInterfaceType.IsAssignableFrom(i) && i != baseInterfaceType);

            foreach (var interfaceType in derivedInterfaces)
            {
                var implementedInterfaces = interfaceType.Assembly.GetTypes().Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();           

                foreach (var implementedInterface in implementedInterfaces)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Transient:
                            services.AddTransient(interfaceType, implementedInterface);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(interfaceType, implementedInterface);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(interfaceType, implementedInterface);
                            break;
                    }
                }
            }
            return services;
        }
    }
}
