using System.Reflection;

namespace PersonManagement.PL.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection RegisterServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var typesWithInterfaces = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Implementation = t,
                Interfaces = t.GetInterfaces()
                    .Where(i => i.Assembly == assembly && i.Name.StartsWith('I'))
            })
            .Where(x => x.Interfaces.Any());

        foreach (var type in typesWithInterfaces)
        {
            foreach (var service in type.Interfaces)
            {
                services.AddScoped(service, type.Implementation);
            }
        }

        return services;
    }
}
