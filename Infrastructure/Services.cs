using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Services;

namespace ESProjeto_Back.Infrastructure
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IStopPointService, StopPointService>();

            return services;
        }
    }
}
