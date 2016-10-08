using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public virtual ICollection<CategoriaEmpresa> CategoriaEmpresas { get; set; }
    }
}