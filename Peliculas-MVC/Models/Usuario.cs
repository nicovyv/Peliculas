using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Peliculas_MVC.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string  ImagenUrlPerfil { get; set; }
        public List<Pelicula>? PeliculasFavoritas { get; set; }
        public List<Review>? ReviewsUsuario { get; set; }
    }
}
