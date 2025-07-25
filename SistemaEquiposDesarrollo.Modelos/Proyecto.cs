using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEquiposDesarrollo.Modelos
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        //clave foraneas
        public int UsuarioId { get; set; }

        //relaciones
        public List<Tarea>? Tareas { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
