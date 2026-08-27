using Microsoft.AspNetCore.Identity;
using Pelicula_MVC.Data;
using Peliculas_MVC.Models;

namespace Peliculas_MVC.Data
{
    public class DbSeeder
    {
        public static async Task Seed(PeliculaDbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            //crear rol Admin si no existe
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }


            //Crear usuario admin si no existe
            var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminUser == null) 
            {
                adminUser = new Usuario
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Nombre = "Admin",
                    Apellido = "Sistema",
                    ImagenUrlPerfil = "/images/default-avatar.png"
                };

            }

            var result = await userManager.CreateAsync(adminUser, "Admin123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }




            // 3) Verificar si ya hay datos cargados
            if (context.Peliculas.Any() || context.Plataformas.Any() || context.Generos.Any())
                return;

            // 4) Plataformas
            var plataformas = new List<Plataforma>
        {
            new Plataforma { Nombre = "Netflix", Url = "https://www.netflix.com", LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/0/08/Netflix_2015_logo.svg" },
            new Plataforma { Nombre = "Prime Video", Url = "https://www.primevideo.com", LogoUrl = "https://i.pinimg.com/474x/f5/de/23/f5de23352bd2620c5a1b2e193e6c8f20.jpg" },
            new Plataforma { Nombre = "Disney+", Url = "https://www.disneyplus.com", LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3e/Disney%2B_logo.svg" },
            new Plataforma { Nombre = "Max", Url = "https://www.max.com", LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/3/37/Max_2025_logo.svg" }
        };

            // 5) Géneros
            var generos = new List<Genero>
        {
            new Genero { Descripcion = "Acción" },
            new Genero { Descripcion = "Drama" },
            new Genero { Descripcion = "Comedia" },
            new Genero { Descripcion = "Ciencia Ficción" },
            new Genero { Descripcion = "Animación" }
        };

            context.Plataformas.AddRange(plataformas);
            context.Generos.AddRange(generos);
            context.SaveChanges(); // Guardamos para que EF Core les asigne un ID y las empiece a trackear

            var p = plataformas.ToDictionary(x => x.Nombre);
            var g = generos.ToDictionary(x => x.Descripcion);

            // 6) Tus 20 Películas personalizadas
            var peliculas = new List<Pelicula>
        {
            // --- NETFLIX ---
            new Pelicula {
                Titulo = "Naked",
                Sinopsis = "Un hombre brillante pero autodestructivo deambula por las calles de Londres, interactuando con diversos personajes.",
                FechaLanzamiento = new DateTime(1993, 11, 5),
                MinutosDuracion = 131,
                PosterUrlPortada = "https://m.media-amazon.com/images/M/MV5BMzczYjlkYWQtNmMzMi00YWY0LWE5ZWYtNmRkMjA1MDczN2ZiXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                Genero = g["Drama"], Plataforma = p["Netflix"]
            },
            new Pelicula {
                Titulo = "Susurros del corazón (Whisper of the Heart)",
                Sinopsis = "Una joven estudiante amante de los libros descubre que todos los libros que elige en la biblioteca fueron prestados previamente por la misma persona.",
                FechaLanzamiento = new DateTime(1995, 7, 15),
                MinutosDuracion = 111,
                PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/9/93/Whisper_of_the_Heart_%28Movie_Poster%29.jpg",
                Genero = g["Animación"], Plataforma = p["Netflix"]
            },
            new Pelicula {
                Titulo = "Rushmore",
                Sinopsis = "Un adolescente excéntrico se enamora de una maestra en su prestigiosa academia.",
                FechaLanzamiento = new DateTime(1998, 12, 11),
                MinutosDuracion = 93,
                PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/4/42/Rushmoreposter.png",
                Genero = g["Comedia"], Plataforma = p["Netflix"]
            },
            new Pelicula {
                Titulo = "3 Women",
                Sinopsis = "La relación entre dos mujeres compañeras de cuarto se vuelve cada vez más extraña y absorbente.",
                FechaLanzamiento = new DateTime(1977, 4, 11),
                MinutosDuracion = 124,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSlrqfiOas-UUMrZihnBMgfDKkyfjfp8UBpwlVI_XxyHQ&s=10",
                Genero = g["Drama"], Plataforma = p["Netflix"]
            },
            new Pelicula {
                Titulo = "Tesis",
                Sinopsis = "Una estudiante de cine que prepara una tesis sobre la violencia audiovisual encuentra una cinta snuff en su facultad.",
                FechaLanzamiento = new DateTime(1996, 4, 12),
                MinutosDuracion = 125,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVVk5Lx9PtEDk8_O3M87OmAZD8DIgWPDFOqzrpYs4skw&s=10",
                Genero = g["Drama"], Plataforma = p["Netflix"]
            },

            // --- PRIME VIDEO ---
            new Pelicula {
                Titulo = "Zoolander",
                Sinopsis = "Un modelo masculino de pocas luces es lavado del cerebro para cometer un asesinato político.",
                FechaLanzamiento = new DateTime(2001, 9, 28),
                MinutosDuracion = 90,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRiP6dEhoY24pbA6674mgxPlBYiPIKoV_g1zAghLu4ouw&s=10",
                Genero = g["Comedia"], Plataforma = p["Prime Video"]
            },
            new Pelicula {
                Titulo = "Crónica de un niño solo",
                Sinopsis = "La cruda realidad de un niño que entra a un reformatorio infantil.",
                FechaLanzamiento = new DateTime(1964, 5, 5),
                MinutosDuracion = 79,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSTm3qoAUZpskswcD_RNGHgkqCyJST5c6rogQrlnUlPdg&s=10",
                Genero = g["Drama"], Plataforma = p["Prime Video"]
            },
            new Pelicula {
                Titulo = "Donnie Darko",
                Sinopsis = "Un adolescente perturbado es guiado por un conejo gigante a cometer una serie de crímenes.",
                FechaLanzamiento = new DateTime(2001, 10, 26),
                MinutosDuracion = 113,
                PosterUrlPortada = "https://upload.wikimedia.org/wikipedia/en/d/db/Donnie_Darko_poster.jpg",
                Genero = g["Ciencia Ficción"], Plataforma = p["Prime Video"]
            },
            new Pelicula {
                Titulo = "Carnival of Souls",
                Sinopsis = "Tras sobrevivir a un accidente automovilístico, una mujer comienza a tener visiones aterradoras.",
                FechaLanzamiento = new DateTime(1973, 9, 26),
                MinutosDuracion = 78,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQgzq2xoveRLfumIwqGpWPoR0w1BHIq6LCH1dYLV63lBQ&s=10",
                Genero = g["Drama"], Plataforma = p["Prime Video"]
            },
            new Pelicula {
                Titulo = "Sex, Lies, and Videotape",
                Sinopsis = "La llegada de un visitante peculiar altera para siempre el matrimonio de una pareja.",
                FechaLanzamiento = new DateTime(1989, 8, 18),
                MinutosDuracion = 100,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoX65Ma6x7rx641Uevye4Q8QtJs8w1ZY6W42B4zuK_-A&s=10",
                Genero = g["Drama"], Plataforma = p["Prime Video"]
            },

            // --- DISNEY+ ---
            new Pelicula {
                Titulo = "Bleeder",
                Sinopsis = "Un joven adicto a las películas pierde el control de su vida cuando su novia queda embarazada.",
                FechaLanzamiento = new DateTime(1999, 8, 6),
                MinutosDuracion = 98,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSWFQJzvZUw5EvKT4ti5TvySf7NUd2uG96QpdWv_ZFOA&s=10",
                Genero = g["Acción"], Plataforma = p["Disney+"]
            },
            new Pelicula {
                Titulo = "Boyhood",
                Sinopsis = "La historia de un niño creciendo a lo largo de 12 años.",
                FechaLanzamiento = new DateTime(2014, 7, 11),
                MinutosDuracion = 165,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSGZNbMPhcV8ilWnirJ67HmeUTMYGUWSnuYp01MXg-0RA&s=10",
                Genero = g["Drama"], Plataforma = p["Disney+"]
            },
            new Pelicula {
                Titulo = "Suicide Club",
                Sinopsis = "Una ola de suicidios grupales inexplicables sacude Japón.",
                FechaLanzamiento = new DateTime(2002, 3, 9),
                MinutosDuracion = 99,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTe4R3hSFgzHHKfvTJob2wt083Cpd7GVekV4H4rbBSbSw&s=10",
                Genero = g["Drama"], Plataforma = p["Disney+"]
            },
            new Pelicula {
                Titulo = "Cold Fish",
                Sinopsis = "Un hombre dueño de una tienda de peces se ve involucrado con un peligroso asesino en serie.",
                FechaLanzamiento = new DateTime(2010, 9, 7),
                MinutosDuracion = 146,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRW09p3XzNIdOVqxt02bMPEBcRekMavxJHNngg89kyFYg&s=10",
                Genero = g["Drama"], Plataforma = p["Disney+"]
            },
            new Pelicula {
                Titulo = "Mauvais Sang",
                Sinopsis = "En un París futuro, un joven planea robar un virus de una peligrosa corporación.",
                FechaLanzamiento = new DateTime(1986, 11, 26),
                MinutosDuracion = 116,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRP0ORlv6CF-kPYcPSWEL5F3R0AoYcN5mP5Q9uHpfTKjw&s=10",
                Genero = g["Drama"], Plataforma = p["Disney+"]
            },

            // --- MAX ---
            new Pelicula {
                Titulo = "Mister Lonely",
                Sinopsis = "Un imitador de Michael Jackson se muda a un castillo habitado por otros imitadores famosos.",
                FechaLanzamiento = new DateTime(2007, 5, 22),
                MinutosDuracion = 112,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQESyWZ5jB-fYgLFzafLI38weNs4V8r0il3bY0j6NRBTQ&s=10",
                Genero = g["Comedia"], Plataforma = p["Max"]
            },
            new Pelicula {
                Titulo = "Gallipoli",
                Sinopsis = "Dos jóvenes velocistas australianos se unen al ejército durante la Primera Guerra Mundial.",
                FechaLanzamiento = new DateTime(1981, 8, 13),
                MinutosDuracion = 110,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoh_wXznPLFnWi0SeIjNOZ_2rLZv3YKCmjG9sq_F8MemobjnkZXmwb9rH0&s=10",
                Genero = g["Acción"], Plataforma = p["Max"]
            },
            new Pelicula {
                Titulo = "After Life",
                Sinopsis = "Los muertos deben elegir un único recuerdo para llevarse consigo a la eternidad.",
                FechaLanzamiento = new DateTime(1998, 9, 11),
                MinutosDuracion = 119,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRue7NVQWXZpU0hSQf836rtusIgW5nEWsWU-MWkbKjTA&s=10",
                Genero = g["Drama"], Plataforma = p["Max"]
            },
            new Pelicula {
                Titulo = "Punch-Drunk Love",
                Sinopsis = "Un hombre con graves problemas de manejo de ira encuentra el amor verdadero.",
                FechaLanzamiento = new DateTime(2002, 11, 1),
                MinutosDuracion = 95,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTteR_DWu8eGGdnlMe0AkrHgAoln_m6CKr15nlc_V3L9Q&s=10",
                Genero = g["Comedia"], Plataforma = p["Max"]
            },
            new Pelicula {
                Titulo = "Lesson of the Evil",
                Sinopsis = "Un popular profesor de secundaria, que en realidad es un psicópata, decide eliminar a sus alumnos problemáticos.",
                FechaLanzamiento = new DateTime(2012, 11, 10),
                MinutosDuracion = 129,
                PosterUrlPortada = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFVlKQo1D7-B34hOZAyXfrK0V63kKjkEUZ_k3gRcqz8A&s=10",
                Genero = g["Acción"], Plataforma = p["Max"]
            }
        };

            context.Peliculas.AddRange(peliculas);
            context.SaveChanges();
        }

    }
}
