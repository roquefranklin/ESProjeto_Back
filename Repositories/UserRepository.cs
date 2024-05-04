﻿using ESProjeto_Back.Data;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;

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
            var bankUser = _context.Users.Find(user.Id);
            bankUser.Nome = user.Nome;
            bankUser.NickName = user.NickName;
            bankUser.PhoneNumber = user.PhoneNumber;
            bankUser.Perfil = user.Perfil;
            _context.SaveChanges();

        }
    }
}
