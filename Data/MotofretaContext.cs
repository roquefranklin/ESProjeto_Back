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
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<StopPoint> StopPoints { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
