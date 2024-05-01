using ESProjeto_Back.Models;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface ITokenRepository
    {
        Token? FindByToken(string refreshToken);
        public Task StoreToken(Token token);

    }
}
