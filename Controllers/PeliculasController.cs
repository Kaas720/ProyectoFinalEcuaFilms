using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalEcuaFilms.Data;
using ProyectoFinalEcuaFilms.Model;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoFinalEcuaFilms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeliculasController : ControllerBase
    {
        private readonly DbContextConeccion _context;

        public PeliculasController(DbContextConeccion context)
        {
            _context = context;
        }

        // GET: api/Peliculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peliculas>>> Getpeliculas()
        {
          if (_context.peliculas == null)
          {
              return NotFound();
          }
            return await _context.peliculas.ToListAsync();
        }

        // GET: api/Peliculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peliculas>> GetPeliculas(int id)
        {
          if (_context.peliculas == null)
          {
              return NotFound();
          }
            var peliculas = await _context.peliculas.FindAsync(id);

            if (peliculas == null)
            {
                return NotFound();
            }

            return peliculas;
        }
        // GET: api/Peliculas/5
        [HttpGet("BusquedaPorIdCategor/{id}")]
        public async Task<IEnumerable<Peliculas>> GetPeliculasCategoriasId(string id)
        {
            int id_int = 0;
            try
            {
                id_int = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            var result = _context.peliculas.AsQueryable();
            IQueryable<Peliculas> peliculas = _context.peliculas;
            if (id_int==0)
            {
                if(id!="Peliculas")
                {
                    peliculas = peliculas.Where(e => e.nombrePelicula.StartsWith(id));
                }    
               
            }
            else
            {
                peliculas = peliculas.Where(e => e.idCategoria == id_int);
            }
            if (peliculas == null)
            {
                return null;
            }
            return await peliculas.ToListAsync();
        }
        // PUT: api/Peliculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeliculas(int id, Peliculas peliculas)
        {
            if (id != peliculas.Id)
            {
                return BadRequest();
            }

            _context.Entry(peliculas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Peliculas>> PostPeliculas(Peliculas peliculas)
        {
          if (_context.peliculas == null)
          {
              return Problem("Entity set 'DbContextConeccion.peliculas'  is null.");
          }
            _context.peliculas.Add(peliculas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeliculas", new { id = peliculas.Id }, peliculas);
        }

        // DELETE: api/Peliculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeliculas(int id)
        {
            if (_context.peliculas == null)
            {
                return NotFound();
            }
            var peliculas = await _context.peliculas.FindAsync(id);
            if (peliculas == null)
            {
                return NotFound();
            }

            _context.peliculas.Remove(peliculas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculasExists(int id)
        {
            return (_context.peliculas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
