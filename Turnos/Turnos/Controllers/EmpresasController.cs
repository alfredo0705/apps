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
using Turnos.Models;

namespace Turnos.Controllers
{
    public class EmpresasController : ApiController
    {
        private TurnosContext db = new TurnosContext();

        // GET: api/Empresas
        public IQueryable<Empresa> GetEmpresas()
        {
            return db.Empresas;
        }

        // GET: api/Empresas/5
        [ResponseType(typeof(Empresa))]
        public IHttpActionResult GetEmpresa(string id)
        {
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        // PUT: api/Empresas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmpresa(string id, Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empresa.NitEmpresa)
            {
                return BadRequest();
            }

            db.Entry(empresa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        // POST: api/Empresas
        [ResponseType(typeof(Empresa))]
        public IHttpActionResult PostEmpresa(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empresas.Add(empresa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmpresaExists(empresa.NitEmpresa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = empresa.NitEmpresa }, empresa);
        }

        // DELETE: api/Empresas/5
        [ResponseType(typeof(Empresa))]
        public IHttpActionResult DeleteEmpresa(string id)
        {
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return NotFound();
            }

            db.Empresas.Remove(empresa);
            db.SaveChanges();

            return Ok(empresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpresaExists(string id)
        {
            return db.Empresas.Count(e => e.NitEmpresa == id) > 0;
        }
    }
}