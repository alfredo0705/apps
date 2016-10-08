using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class Empresa
    {
        [Key]
        public string NitEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string Imagen { get; set; }

        
        public virtual ICollection<HoraAtencion> HoraAtenciones { get; set; }
        public virtual ICollection<CategoriaEmpresa> CategoriaEmpresas { get; set; }
    }
}