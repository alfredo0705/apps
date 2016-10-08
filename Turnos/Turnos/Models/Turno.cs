using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class Turno
    {
        [Key]
        public int TurnoID { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int TipoServicioID { get; set; }
        public virtual TipoServicio TipoServicio { get; set; }
        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}