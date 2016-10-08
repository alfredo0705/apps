using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class ProfesionalServicio
    {
        [Key]
        public int ProfesionalServicioID { get; set; }
        public int ProfesionalID { get; set; }
        public int TipoServicioID { get; set; }

        public virtual TipoServicio TipoServicio { get; set; }
        public virtual Profesional Profesional { get; set; }
    }
}