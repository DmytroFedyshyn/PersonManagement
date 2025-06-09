using PersonManagement.BLL.Interfaces;
using PersonManagement.DAL.Interfaces;
using PersonManagement.PL.Extensions;

namespace PersonManagement.PL.Infrastructure;
public static class ServiceRegistration
{
    public static void RegisterBusinessLogicServices(this IServiceCollection services)
    {
        services.RegisterServicesFromAssembly(typeof(IPersonService).Assembly);
    }

    public static void RegisterDataAccessServices(this IServiceCollection services)
    {
        services.RegisterServicesFromAssembly(typeof(IPersonRepository).Assembly);
    }
}
