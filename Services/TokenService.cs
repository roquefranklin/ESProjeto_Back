using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;

        public TokenService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
    }
}
