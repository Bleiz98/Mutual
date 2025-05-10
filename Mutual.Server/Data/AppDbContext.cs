using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mutual.Server.Models;

namespace Mutual.server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
    }
}