using Microsoft.EntityFrameworkCore;
using Cine.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cine.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Colombia> Colombia { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }

        public DbSet<Funcion> Funcion { get; set; }

        public DbSet<Pelicula> Pelicula { get; set; }

        public DbSet<Owner> Owners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Colombia>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Genero>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Ciudad>().HasIndex("ColombiaId", "Name").IsUnique();
            modelBuilder.Entity<Pelicula>().HasIndex("GeneroId", "Name").IsUnique();
            modelBuilder.Entity<Funcion>().HasIndex("PeliculaId", "Name").IsUnique();
        }
    }
}
