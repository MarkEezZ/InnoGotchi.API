using InnoGotchi.API.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Eyes> Eyes { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Nose> Noses { get; set; }
        public DbSet<Mouth> Mouthes { get; set; }
        public DbSet<Body> Bodies { get; set; }
    }
}
