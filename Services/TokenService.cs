using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IUserRepository userRepository, ITokenRepository tokenRepo) {
            _userRepository = userRepository;
            _tokenRepository = tokenRepo;
        }

        public Task StoreToken(Token token)
        {
            _tokenRepository.StoreToken(token);
            return Task.CompletedTask;
        }
        
        public Token? FindByToken(string refreshToken)
        {
            return _tokenRepository.FindByToken(refreshToken);
        }

    }
}
