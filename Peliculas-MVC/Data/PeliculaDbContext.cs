using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Peliculas_MVC.Models;

namespace Pelicula_MVC.Data
{
    public class PeliculaDbContext : IdentityDbContext<Usuario>
    {
        public PeliculaDbContext(DbContextOptions<PeliculaDbContext> options) : base(options)
        {
        }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Plataforma> Plataformas { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
    }
}
