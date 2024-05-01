using ESProjeto_Back.Data;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public static MotofretaContext _context { get; set; }
        public TokenRepository(MotofretaContext context)
        {
            _context = context;
        }
        public Task StoreToken(Token token)
        {
            _context.Add(token);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Token? FindByToken(string refreshToken)
        {
            return _context.Token.FirstOrDefault((token) => token.token == refreshToken);
        }
    }
}
