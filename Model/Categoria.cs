using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalEcuaFilms.Model
{
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }
        [Required]
        public string? nombreCategoria { get; set; }
        [Required]
        public string? visibilidad { get; set; }
    }
}
