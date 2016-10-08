using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Nombres { get; set; }
        public string Celular { get; set; }
        public string Imagen { get; set; }

        public virtual ICollection<Turno> Turnos { get; set; }
    }
}