using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalEcuaFilms.Data;
using ProyectoFinalEcuaFilms.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace ProyectoFinalEcuaFilms.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsuarioController : ControllerBase
    {
        private readonly DbContextConeccion _context;
        private readonly IConfiguration _configuration;
        public UsuarioController(DbContextConeccion context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> PostCliente(DataUserConnect user)
        {
            Console.WriteLine(user.correo);
            Console.WriteLine(user.password);

            var userTemp = await _context.credenciales_usuarios.FirstOrDefaultAsync
                (x => x.correo.ToLower().Equals(user.correo));
            if (userTemp == null){ 
                return BadRequest("UserNotFound"); 
            }
            else if (userTemp.password.Equals(user.password))   {
                //return Ok("UserFound");
                return Ok(JsonConvert.SerializeObject(CrearToken(userTemp)));
            }
            else{
                return BadRequest("keyError");
            }                

        }

        private string CrearToken(Credenciales_usuarios user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.idCredencialUsuario.ToString()),
                new Claim(ClaimTypes.Name, user.correo),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                                        GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }


    }
}
