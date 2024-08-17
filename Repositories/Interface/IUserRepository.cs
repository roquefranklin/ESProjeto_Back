using ESProjeto_Back.Data.Dtos;
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
        public Task SetRecoveryCode(User user, string recoveryCode);
        public Guid SetNewPassword(User user, NewPassword newPassword);
        public Guid ClearRecoveryCode(User user);
    }
}
