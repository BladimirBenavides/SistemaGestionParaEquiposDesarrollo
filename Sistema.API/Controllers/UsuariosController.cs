using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using SistemaEquiposDesarrollo.Modelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sistema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private DbConnection conexion;
        public UsuariosController(IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("DefaultConection");
            conexion = new SqlConnection(conectionString); 
            conexion.Open();
        }
        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            var usuarios = conexion.Query<Usuario>("SELECT * FROM usuarios").ToList();
            return usuarios;
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            var usuario = conexion.QuerySingle<Usuario>("SELECT * FROM usuarios WHERE Id = @Id", new { Id = id });
            return usuario;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario)
        {
            conexion.Execute("INSERT INTO usuarios (Id, Nombre, Correo, Contraseña) VALUES (@Id, @Nombre, @Correo, @Contraseña)", 
                new {Id = usuario.Id, Nombre = usuario.Nombre, Correo = usuario.Correo, Contraseña = usuario.Contraseña});
            return usuario;
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public Usuario Put(int id, [FromBody] Usuario usuario)
        {
            conexion.Execute("UPDATE usuarios SET Nombre = @Nombre, Correo = @Correo, Contraseña = @Contraseña WHERE Id = @Id", 
                new { Id = id, Nombre = usuario.Nombre, Correo = usuario.Correo, Contraseña = usuario.Contraseña });
            return usuario;
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
