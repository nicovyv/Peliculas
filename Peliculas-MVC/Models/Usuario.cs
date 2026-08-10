using Microsoft.AspNetCore.Identity;

namespace Peliculas_MVC.Models
{
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string  ImagenUrlPerfil { get; set; }
        public List<Pelicula>? PeliculasFavoritas { get; set; }
        public List<Review>? ReviewsUsuario { get; set; }
    }
}
