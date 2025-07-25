using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEquiposDesarrollo.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        //relaciones
        public List<Tarea>? Tareas { get; set; }
        public List<Proyecto>? Proyectos { get; set; }
    }
}
