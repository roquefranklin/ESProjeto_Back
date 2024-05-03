using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetUser() {

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

    }
}
