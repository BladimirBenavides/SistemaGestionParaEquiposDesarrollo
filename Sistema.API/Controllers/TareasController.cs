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
    public class TareasController : ControllerBase
    {
        private DbConnection conexion;
        public TareasController(IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("DefaultConection");
            conexion = new SqlConnection(conectionString);
            conexion.Open();
        }
        // GET: api/<TareasController>
        [HttpGet]
        public IEnumerable<Tarea> Get()
        {
            var tareas = conexion.Query<Tarea>("SELECT * FROM tareas").ToList();
            return tareas;
        }

        // GET api/<TareasController>/5
        [HttpGet("Proyecto/{id}")]
        public Tarea Get(int id)
        {
            var tarea = conexion.QuerySingle<Tarea>("SELECT * FROM tareas WHERE Id = @Id", new { Id = id });
            var proyecto = conexion.QuerySingle<Proyecto>("SELECT * FROM proyectos WHERE Id = @Id", new { Id = tarea.ProyectoId });
            tarea.Proyecto = proyecto;
            return tarea;
        }

        // POST api/<TareasController>
        [HttpPost]
        public Tarea Post([FromBody] Tarea tarea)
        {
            conexion.Execute("INSERT INTO tareas (Id, Nombre, Descripcion, Estado, UsuarioId, ProyectoId) VALUES (@Id, @Nombre, @Descripcion, @Estado, @UsuarioId, @ProyectoId)", 
                new { Id = tarea.Id, Nombre = tarea.Nombre, Descripcion = tarea.Descripcion, Estado = tarea.Estado, UsuarioId = tarea.UsuarioId, ProyectoId = tarea.ProyectoId });
            return tarea;
        }

        // PUT api/<TareasController>/5
        [HttpPut("{id}")]
        public Tarea Put(int id, [FromBody] Tarea tarea)
        {
            conexion.Execute("UPDATE tareas SET Nombre = @Nombre, Descripcion = @Descripcion, Estado = @Estado, UsuarioId = @UsuarioId, ProyectoId = @ProyectoId WHERE Id = @Id", 
                new { Id = id, Nombre = tarea.Nombre, Descripcion = tarea.Descripcion, Estado = tarea.Estado, UsuarioId = tarea.UsuarioId, ProyectoId = tarea.ProyectoId });
            return tarea;
        }

        // DELETE api/<TareasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            conexion.Execute("DELETE FROM tareas WHERE Id = @Id", new { Id = id });
        }
    }
}
