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
    public class ProyectosController : ControllerBase
    {
        private DbConnection conexion;
        public ProyectosController(IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("DefaultConection");
            conexion = new SqlConnection(conectionString);
            conexion.Open();
        }
        // GET: api/<ProyectosController>
        [HttpGet]
        public IEnumerable<Proyecto> Get()
        {
            var proyectos = conexion.Query<Proyecto>("SELECT * FROM proyectos").ToList();
            return proyectos;
        }

        // GET api/<ProyectosController>/5
        [HttpGet("{id}")]
        public Proyecto Get(int id)
        {
            var proyecto = conexion.QuerySingle<Proyecto>("SELECT * FROM proyectos WHERE Id = @Id", new { Id = id });
            return proyecto;
        }

        // POST api/<ProyectosController>
        [HttpPost]
        public Proyecto Post([FromBody] Proyecto proyecto)
        {
            conexion.Execute("INSERT INTO proyectos (Id, Nombre, Descripcion, FechaInicio, FechaFin, UsuarioId) VALUES (@Id, @Nombre, @Descripcion, @FechaInicio, @FechaFin, @UsuarioId)", 
                new { Id = proyecto.Id, Nombre = proyecto.Nombre, Descripcion = proyecto.Descripcion, FechaInicio = proyecto.FechaInicio, FechaFin = proyecto.FechaFin, UsuarioId = proyecto.UsuarioId });
            return proyecto;
        }

        // PUT api/<ProyectosController>/5
        [HttpPut("{id}")]
        public Proyecto Put(int id, [FromBody] Proyecto proyecto)
        {
            conexion.Execute("UPDATE proyectos SET Nombre = @Nombre, Descripcion = @Descripcion, FechaInicio = @FechaInicio, FechaFin = @FechaFin, UsuarioId = @UsuarioId WHERE Id = @Id", 
                new { Id = id, Nombre = proyecto.Nombre, Descripcion = proyecto.Descripcion, FechaInicio = proyecto.FechaInicio, FechaFin = proyecto.FechaFin, UsuarioId = proyecto.UsuarioId });
            return proyecto;
        }

        // DELETE api/<ProyectosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM proyectos WHERE Id = @Id", new { Id = id });
        }
    }
}
