﻿using ESProjeto_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace ESProjeto_Back.Data
{
    public class MotofretaContext : DbContext
    {

        public MotofretaContext(DbContextOptions<MotofretaContext> opts) : base(opts)
        {
        
        }

        public DbSet<User> Users { get; set; }
    }
}
