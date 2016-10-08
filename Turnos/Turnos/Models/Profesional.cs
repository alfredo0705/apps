using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class Profesional
    {
        [Key]
        public int ProfesionalID { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string NitEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }

        public virtual ICollection<ProfesionalServicio> ProfesionalServicios { get; set; }
    }
}