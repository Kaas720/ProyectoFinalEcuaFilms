
using Microsoft.EntityFrameworkCore;
using ProyectoFinalEcuaFilms.Model;

namespace ProyectoFinalEcuaFilms.Data
{
    public class DbContextConeccion : DbContext
    {   
        public DbContextConeccion(DbContextOptions<DbContextConeccion> options) : base(options)
        {

        }
        public DbSet<Peliculas> peliculas { get; set; }
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Credenciales_usuarios> credenciales_usuarios { get; set; }

    }
}
