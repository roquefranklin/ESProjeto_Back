using ESProjeto_Back.Models;

namespace ESProjeto_Back.Interfaces
{
    public interface IUserService
    {

        public List<User> Listar();
        public Guid Criar(User user);
        public User? getUser(int id);
        public User? getUserByEmail(string email);
        public bool ValidateNewUser(NewUser newUser);
    }
}
