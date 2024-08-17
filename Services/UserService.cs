using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;
using ESProjeto_Back.Utilities;

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

        public bool ValidateNewUser(NewUser newUser)
        {
            if (!newUser.confirmPassword())
            {
                return false;
            }

            bool validEmail = RegexValidations.EmailValidation(newUser.Email);
            if(!validEmail)
            {
                return false;
            }

            bool emailAlreadyRegister = _userRepository.GetUserByEmail(newUser.Email) != null;
            if (emailAlreadyRegister)
            {
                return false;
            }

            return true;
        }

        public void updateUser(User user)
        {
            _userRepository.updateUser(user);
        }

        public async Task GenerateForgotCodeAndsendForgotPasswordEmail(User user)
        {
            try
            {
                string recoveryCode = GenerateRecoveryCode();

                await _userRepository.SetRecoveryCode(user, recoveryCode);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GenerateRecoveryCode()
        {
            Random generator = new Random();
            String recoveryCode = generator.Next(0, 1000000).ToString("D6");
            return recoveryCode;
        }

        public async Task<bool> newUserPassword(NewPassword newPassword, User user)
        {

            bool isCodeCorrect = user.RecoveryCode != "" && user.RecoveryCode == newPassword.forgotenCode;
            
            if (!isCodeCorrect) {
                return false;
            }

            _userRepository.SetNewPassword(user, newPassword);
            _userRepository.ClearRecoveryCode(user);

            return true;

        }
    }
}
