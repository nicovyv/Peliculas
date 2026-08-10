using System.ComponentModel.DataAnnotations;

namespace Peliculas_MVC.Models
{
    public class Plataforma
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Url]
        public string Url { get; set; }
        [Url]
        public string LogoUrl { get; set; }
        public List<Pelicula>? PlataformaPeliculas { get; set; }
    }
}
