using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalEcuaFilms.Model
{
    public class Credenciales_usuarios
    {
        [Key]
        public int idCredencialUsuario { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }
    }
}
