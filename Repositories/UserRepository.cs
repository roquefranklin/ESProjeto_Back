using ESProjeto_Back.Data;
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

        public int Criar(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.Id;
        }
        public User? GetUser(int id)
        {
            return _context.Users.Find(id);
        }
    }
}
