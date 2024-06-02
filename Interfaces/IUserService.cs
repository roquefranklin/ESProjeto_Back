using ESProjeto_Back.Data.Dtos;
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
        public void updateUser(User user);
        public Task GenerateForgotCodeAndsendForgotPasswordEmail(User user);
        public Task<bool> newUserPassword(NewPassword newPassword, User user);
    }
}
