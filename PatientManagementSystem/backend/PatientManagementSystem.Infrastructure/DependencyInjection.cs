using Microsoft.Extensions.DependencyInjection;

namespace PatientManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            //services.AddCoreDI();
            return services;
        }

    }
}
