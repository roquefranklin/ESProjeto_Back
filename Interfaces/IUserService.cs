using ESProjeto_Back.Models;

namespace ESProjeto_Back.Interfaces
{
    public interface IUserService
    {

        public List<User> Listar();
        public int Criar(User user);
        public User? getUser(int id);
    }
}
