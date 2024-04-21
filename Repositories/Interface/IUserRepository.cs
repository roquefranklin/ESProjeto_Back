using ESProjeto_Back.Models;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface IUserRepository
    {
        public List<User> Listar();
        public Guid Criar(User user);
        User? GetUser(int id);
        User? GetUserByEmail(string email);
    }
}
