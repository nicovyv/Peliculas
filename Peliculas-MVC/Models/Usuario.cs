using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
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




    public class UsuarioViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Clave { get; set; }
        [PasswordPropertyText]
        public string ConfirmarClave { get; set; }
    }


    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [PasswordPropertyText]
        public string Clave { get; set; }
        public bool Recordarme { get; set; }
    }

}
