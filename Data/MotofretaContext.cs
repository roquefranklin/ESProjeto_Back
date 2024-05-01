using ESProjeto_Back.Data.Configuration;
using ESProjeto_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace ESProjeto_Back.Data
{
    public class MotofretaContext : DbContext
    {

        public MotofretaContext(DbContextOptions<MotofretaContext> opts) : base(opts)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Token { get; set; }
    }
}
