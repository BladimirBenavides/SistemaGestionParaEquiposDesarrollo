using System.ComponentModel.DataAnnotations;

namespace SistemaEquiposDesarrollo.Modelos
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        //clave foraneas
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }

        //relaciones
        public Usuario? Usuario { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}
