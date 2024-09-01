using ESProjeto_Back.Data;
using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ESProjeto_Back.Repositories
{
    public class UserRepository : IUserRepository
    {

        public static MotofretaContext _context;

        public UserRepository(MotofretaContext context)
        {
            _context = context;
        }

        public List<User> Listar()
        {
            return _context.Users.ToList();
        }

        public Guid Criar(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.Id;
        }
        public User? GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public void updateUser(User user)
        {
            User? bankUser = _context.Users.Find(user.Id);
            bankUser.Nome = user.Nome;
            bankUser.NickName = user.NickName;
            bankUser.PhoneNumber = user.PhoneNumber;
            bankUser.Perfil = user.Perfil;
            _context.SaveChanges();

        }

        public async Task SetRecoveryCode(User user, string recoveryCode)
        {

            if(recoveryCode.Length != 6 || !int.TryParse(recoveryCode, out _) )
            {
                throw new Exception("The code has to be a 6 lenght string of numbers");
            }

            User? dataBaseUser = _context.Users.Find(user.Id);

            if(dataBaseUser == null)
            {
                throw new Exception("User Not Found!");
            }

            dataBaseUser.RecoveryCode = recoveryCode;
            await _context.SaveChangesAsync();
        }

        public Guid SetNewPassword(User user, NewPassword newPassword)
        {
            User? dataBaseUser = _context.Users.FirstOrDefault(userDb => userDb.Email == user.Email);

            if (dataBaseUser == null)
                return Guid.Empty;

            dataBaseUser.Password = newPassword.Password;
            dataBaseUser.RecoveryCode = "";
            _context.SaveChanges();
            return dataBaseUser.Id;
        }

        public Guid ClearRecoveryCode(User user)
        {

            User? dataBaseUser = _context.Users.FirstOrDefault(userDb => userDb.Email == user.Email);

            if (dataBaseUser == null)
                return Guid.Empty;

            dataBaseUser.RecoveryCode = null;
            _context.SaveChanges();
            return dataBaseUser.Id;

        }
    }
}
