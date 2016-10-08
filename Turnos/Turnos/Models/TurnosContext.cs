using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Turnos.Models
{
    public class TurnosContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TurnosContext() : base("name=DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Turnos.Models.Empresa> Empresas { get; set; }

        public System.Data.Entity.DbSet<Turnos.Models.HoraAtencion> HoraAtencions { get; set; }

        public System.Data.Entity.DbSet<Turnos.Models.TipoServicio> TipoServicios { get; set; }

        public System.Data.Entity.DbSet<Turnos.Models.Turno> Turnoes { get; set; }

        public System.Data.Entity.DbSet<Turnos.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<Turnos.Models.Profesional> Profesionales { get; set; }

        public System.Data.Entity.DbSet<Turnos.Models.ProfesionalServicio> ProfesionalServicios { get; set; }

        public System.Data.Entity.DbSet<Turnos.Models.Categoria> Categorias { get; set; }
    }
}
