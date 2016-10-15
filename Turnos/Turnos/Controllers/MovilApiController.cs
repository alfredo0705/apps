using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Turnos.Clases;
using Turnos.Models;

namespace Turnos.Controllers
{
    [RoutePrefix("api/movil")]
    public class MovilApiController : ApiController
    {
        private TurnosContext db = new TurnosContext();
        private object catEmpresa;

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(JObject form)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var email = string.Empty;
            var password = string.Empty;
            dynamic jsonObject = form;

            try
            {
                email = jsonObject.Email.Value;
                password = jsonObject.Password.Value;
            }
            catch
            {
                return BadRequest("Llamado incorrecto");
            }

            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.Find(email, password);

            if (userASP == null)
            {
                return BadRequest("Clave o usuario incorrecto");
            }

            var user = db.Usuarios.Where(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Clave o usuario incorrecto");
            }

            return Ok(user);
        }

        [ResponseType(typeof(Usuario))]
        [Route("Register")]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(usuario);
            db.SaveChanges();
            UsersHelper.CreateUserASP(usuario.Email, "User");

            return CreatedAtRoute("DefaultApi", new { id = usuario.UsuarioID }, usuario);
        }

        [Route("getCategorias")]
        public IEnumerable<Categoria> GetCategorias()
        {
            List<Categoria> cate = new List<Categoria>();
            foreach (var cat in db.Categorias)
            {
                cate.Add(new Categoria
                {
                    CategoriaID = cat.CategoriaID,
                    Imagen = cat.Imagen,
                    Nombre = cat.Nombre
                });
            }
            IEnumerable<Categoria> categorias = cate;
            return categorias;
        }

        [Route("getEmpresas/{id}")]
        [ResponseType(typeof(Categoria))]
        public IEnumerable<Empresa> GetEmpresas(int id)
        {
            var catEmpresa = db.CategoriaEmpresas.Where(c => c.CategoriaID == id).ToList();
            if (catEmpresa.Count == 0)
            {
                return null;
            }

            var empresa = new List<Empresa>();

            foreach (var categ in catEmpresa)
            {
                var empresaConsulta = db.Empresas.Find(categ.NitEmpresa);
                empresa.Add(new Empresa
                {
                    Celular = empresaConsulta.Celular,
                    Contacto = empresaConsulta.Contacto,
                    Direccion = empresaConsulta.Direccion,
                    Email = empresaConsulta.Email,
                    Imagen = empresaConsulta.Imagen,
                    NitEmpresa = empresaConsulta.NitEmpresa,
                    Nombre = empresaConsulta.Nombre,
                    Telefono = empresaConsulta.Telefono
                });
            }
            IEnumerable<Empresa> empresas = empresa;

            return empresas;
        }

        [Route("getEmpresa/{id}")]
        public IHttpActionResult GetEmpresa(string id)
        {
            var empre = db.Empresas.Find(id);
            if (empre == null)
            {
                return BadRequest("Empresa no existe");
            }

            var empresa = new Empresa
            {
                Celular = empre.Celular,
                Contacto = empre.Contacto,
                Direccion = empre.Direccion,
                Email = empre.Email,
                Imagen = empre.Imagen,
                NitEmpresa = empre.NitEmpresa,
                Nombre = empre.Nombre,
                Telefono = empre.Telefono
            };
            
            return Ok(empresa);
        }

        [Route("getProfesionales/{id}")]
        [ResponseType(typeof(Categoria))]
        public IEnumerable<Profesional> GetProfesionales(string id)
        {
            var profesional = new List<Profesional>();

            foreach (var categ in db.Profesionales.Where(c => c.NitEmpresa == id))
            {                
                profesional.Add(new Profesional
                {
                    Imagen = categ.Imagen,
                    NitEmpresa = categ.NitEmpresa,
                    Nombre = categ.Nombre,
                    ProfesionalID = categ.ProfesionalID
                    
                });
            }
            IEnumerable<Profesional> profesionales = profesional;

            return profesionales;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
