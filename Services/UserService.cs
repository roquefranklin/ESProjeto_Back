using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Services
{
    public class UserService : IUserService
    {

        private static IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> Listar()
        {
            return _userRepository.Listar();
        }

        public Guid Criar(User user)
        {
            return _userRepository.Criar(user);
        }
        public User? getUser(int id)
        {
            return _userRepository.GetUser(id);
        }
        public User? getUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }
    }
}
