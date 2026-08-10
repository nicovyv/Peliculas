using System.ComponentModel.DataAnnotations;

namespace Peliculas_MVC.Models
{
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }
        public List<Pelicula>? GeneroPeliculas { get; set; }
    }
}
