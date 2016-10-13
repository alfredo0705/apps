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
    public class CategoriaEmpresasController : ApiController
    {
        private TurnosContext db = new TurnosContext();

        public IQueryable<CategoriaEmpresa> GetCategoriaEmpresas()
        {
            return db.CategoriaEmpresas;
        }

        // GET: api/CategoriaEmpresas/5
        [ResponseType(typeof(CategoriaEmpresa))]
        public IHttpActionResult GetCategoriaEmpresa(int id)
        {
            CategoriaEmpresa categoriaEmpresa = db.CategoriaEmpresas.Find(id);
            if (categoriaEmpresa == null)
            {
                return NotFound();
            }

            return Ok(categoriaEmpresa);
        }

        // PUT: api/CategoriaEmpresas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoriaEmpresa(int id, CategoriaEmpresa categoriaEmpresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriaEmpresa.CategoriaEmpresaID)
            {
                return BadRequest();
            }

            db.Entry(categoriaEmpresa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaEmpresaExists(id))
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

        // POST: api/CategoriaEmpresas
        [ResponseType(typeof(CategoriaEmpresa))]
        public IHttpActionResult PostCategoriaEmpresa(CategoriaEmpresa categoriaEmpresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoriaEmpresas.Add(categoriaEmpresa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoriaEmpresa.CategoriaEmpresaID }, categoriaEmpresa);
        }

        // DELETE: api/CategoriaEmpresas/5
        [ResponseType(typeof(CategoriaEmpresa))]
        public IHttpActionResult DeleteCategoriaEmpresa(int id)
        {
            CategoriaEmpresa categoriaEmpresa = db.CategoriaEmpresas.Find(id);
            if (categoriaEmpresa == null)
            {
                return NotFound();
            }

            db.CategoriaEmpresas.Remove(categoriaEmpresa);
            db.SaveChanges();

            return Ok(categoriaEmpresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaEmpresaExists(int id)
        {
            return db.CategoriaEmpresas.Count(e => e.CategoriaEmpresaID == id) > 0;
        }
    }
}