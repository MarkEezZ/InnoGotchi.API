using InnoGotchi.API.Entities.Configuration;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BodyConfiguration());
            modelBuilder.ApplyConfiguration(new NoseConfiguration());
            modelBuilder.ApplyConfiguration(new EyesConfiguration());
            modelBuilder.ApplyConfiguration(new MouthConfiguration());
            modelBuilder.ApplyConfiguration(new FarmConfiguration());
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GuestsConfiguration());
            modelBuilder.ApplyConfiguration(new OwnersConfiguration());
        }

        public DbSet<Body> Bodies { get; set; }
        public DbSet<Eyes> Eyes { get; set; }
        public DbSet<Nose> Noses { get; set; }
        public DbSet<Mouth> Mouthes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Owners> Owners { get; set; }
    }
}
