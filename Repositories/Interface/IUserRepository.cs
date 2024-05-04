using ESProjeto_Back.Models;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface IUserRepository
    {
        public List<User> Listar();
        public Guid Criar(User user);
        public User? GetUser(int id);
        public User? GetUserByEmail(string email);
        public void updateUser(User user);
    }
}
