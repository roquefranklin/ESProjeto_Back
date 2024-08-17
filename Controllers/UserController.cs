using Microsoft.Net.Http.Headers;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ESProjeto_Back.Data.Dtos;

namespace ESProjeto_Back.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {

        private static IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUser()
        {

            try
            {
                var users = _userService.Listar();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userService.getUser(id);

                if (user == null)
                {
                    return NotFound($"Usuario de Id {id} não encontrado!");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var user = _userService.getUserByEmail(email);

                if (user == null)
                {
                    return NotFound($"Usuario de email {email} não encontrado!");
                }

                user.RecoveryCode = string.Empty;

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] NewUser newUser)
        {
            try
            {

                bool validNewUser = _userService.ValidateNewUser(newUser);
                if (!validNewUser)
                {
                    return BadRequest("Dados Inválidos");
                }

                User user = new User
                {
                    Email = newUser.Email,
                    Password = newUser.Password,
                    Nome = newUser.Nome,
                    NickName = newUser.NickName,
                    Perfil = string.Empty,
                    PhoneNumber = string.Empty
                };

                Guid newUserId = _userService.Criar(user);

                return Ok(newUserId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] string email)
        {
            try
            {

                User? user = _userService.getUserByEmail(email);
                    
                if (user == null)
                {
                    return BadRequest($"Usuário de Email {user} não encontrado");
                }

                _userService.GenerateForgotCodeAndsendForgotPasswordEmail(user);

                return Ok("Email para Recuperar Senha será Enviado Logo Mais!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("new-password")]
        public async Task<IActionResult> NewPassword([FromBody] NewPassword newPassword)
        {
            try
            {

                if (!newPassword.PasswordMatch())
                {
                    return BadRequest($"Senhas Não São Iguais");
                }

                User? user = _userService.getUserByEmail(newPassword.Email);

                if (user == null )
                    return BadRequest($"Usuário de Email \"{user}\" não encontrado");

                bool result = await _userService.newUserPassword(newPassword, user);
                
                if (!result) {
                    return BadRequest($"Codigo Inválido");
                }

                return Ok("Senha Atualizada com Sucesso");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult updateUserInfo([FromBody] UpdateUser updateUser)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                if (identity == null)
                {
                    return BadRequest("Erro ao identificar usuário, calims não encontadas em token");
                }

                IEnumerable<Claim> claims = identity.Claims;

                string userEmail = claims.FirstOrDefault(claim => claim.Type.Contains(ClaimTypes.Email)).Value ?? "";

                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest($"Toekn não possui claim {JwtRegisteredClaimNames.Email} email de usuário!");
                }

                bool emailEmpty = string.IsNullOrEmpty(updateUser.Email);
                if (emailEmpty)
                {
                    return BadRequest("Campo de email está vazio");
                }

                User user = _userService.getUserByEmail(userEmail)!;
                if (user == null)
                {
                    return BadRequest($"Usuário do Email {userEmail} ,do token, não encontrado");
                }

                bool emailNotEquals = !updateUser.Email.Equals(user.Email);
                if (emailNotEquals)
                {
                    return BadRequest($"Email do token({user.Email}) e email para alteração({updateUser.Email}) não são iguais");
                }

                bool hasNoAlteration = updateUser.EmptyAlterations();
                if (hasNoAlteration)
                {
                    return BadRequest("Todos os campos alteráveis estão vazios!");
                }
                user.Nome = string.IsNullOrEmpty(updateUser.Nome) ? user.Nome : updateUser.Nome;
                user.NickName = string.IsNullOrEmpty(updateUser.NickName) ? user.NickName : updateUser.NickName;
                user.PhoneNumber = string.IsNullOrEmpty(updateUser.PhoneNumber) ? user.PhoneNumber : updateUser.PhoneNumber;
                user.Perfil = string.IsNullOrEmpty(updateUser.Perfil) ? user.Perfil : updateUser.Perfil;
                _userService.updateUser(user);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
