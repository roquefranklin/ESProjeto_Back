﻿using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESProjeto_Back.Controllers
{
    [ApiController]
    [Route("[Controller]")]
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

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto userDto) {

            try
            {
                var createdUser = _userService.Criar(new User()
                {
                    FirsName = userDto.FirsName,
                    LastName = userDto.LastName,
                    Age = userDto.Age,
                });

                return Ok(createdUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }


    }
}
