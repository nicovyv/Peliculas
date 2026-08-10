namespace Peliculas_MVC.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string  ImagenUrlPerfil { get; set; }
        public List<Pelicula>? PeliculasFavoritas { get; set; }
    }
}
