using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class TipoServicio
    {
        [Key]
        public int TipoServicioID { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        
        public virtual ICollection<Turno> Turnos { get; set; }
    }
}