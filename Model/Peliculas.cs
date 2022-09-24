using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalEcuaFilms.Model
{
    public class Peliculas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? nombrePelicula { get; set; }
        [Required]
        public string? sipnosisPelicula { get; set; }
        [Required]
        public string? imagenPrincipal { get; set; }
        [Required]
        public string? imagenSegundaria { get; set; }
        [Required]
        public string? imagenTercera { get; set; }
        [Required]
        public string? imagenCuarta { get; set; }
        [Required]
        public string? limkPelicula { get; set; }
        [Required]
        public int calificacion { get; set; }
        [ForeignKey("Categoria")]
        public int idCategoria { get; set; }

    }
}
