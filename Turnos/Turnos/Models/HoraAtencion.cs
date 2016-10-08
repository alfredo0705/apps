using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class HoraAtencion
    {
        [Key]
        public int HoraAtencionID { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }

        public string NitEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}