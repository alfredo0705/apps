using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class CategoriaEmpresa
    {
        [Key]
        public int CategoriaEmpresaID { get; set; }
        public int CategoriaID { get; set; }
        public string NitEmpresa { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}