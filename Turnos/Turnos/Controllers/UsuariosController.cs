using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Turnos.Clases;
using Turnos.Models;

namespace Turnos.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private TurnosContext db = new TurnosContext();

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

        // GET: api/Usuarios

        public IQueryable<Usuario> GetUsuarios()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Usuarios;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.UsuarioID)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            var db2 = new TurnosContext();

            var currentUser = db2.Usuarios.Find(id);
            if (currentUser.Email != usuario.Email)
            {
                UsersHelper.updateEmail(currentUser.Email, usuario.Email);
            }
            db2.Dispose();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
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

        //// DELETE: api/Usuarios/5
        //[ResponseType(typeof(Usuario))]
        //public IHttpActionResult DeleteUsuario(int id)
        //{
        //    Usuario usuario = db.Usuarios.Find(id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Usuarios.Remove(usuario);
        //    db.SaveChanges();
        //    UsersHelper.DeleteUser(usuario.Email);
        //    return Ok(usuario);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.Count(e => e.UsuarioID == id) > 0;
        }
    }
}