using ESProjeto_Back.Repositories;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Infrastructure
{
    public static class Repositories
    {

        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {

            repositories.AddScoped<IUserRepository, UserRepository>();
            repositories.AddScoped<ITokenRepository, TokenRepository>();

            return repositories;
        }

    }
}
