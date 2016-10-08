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
    public class TipoServiciosController : ApiController
    {
        private TurnosContext db = new TurnosContext();

        // GET: api/TipoServicios
        public IQueryable<TipoServicio> GetTipoServicios()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.TipoServicios;
        }

        // GET: api/TipoServicios/5
        [ResponseType(typeof(TipoServicio))]
        public IHttpActionResult GetTipoServicio(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            TipoServicio tipoServicio = db.TipoServicios.Find(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            return Ok(tipoServicio);
        }

        // PUT: api/TipoServicios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoServicio(int id, TipoServicio tipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoServicio.TipoServicioID)
            {
                return BadRequest();
            }

            db.Entry(tipoServicio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoServicioExists(id))
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

        // POST: api/TipoServicios
        [ResponseType(typeof(TipoServicio))]
        public IHttpActionResult PostTipoServicio(TipoServicio tipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoServicios.Add(tipoServicio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoServicio.TipoServicioID }, tipoServicio);
        }

        // DELETE: api/TipoServicios/5
        [ResponseType(typeof(TipoServicio))]
        public IHttpActionResult DeleteTipoServicio(int id)
        {
            TipoServicio tipoServicio = db.TipoServicios.Find(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            db.TipoServicios.Remove(tipoServicio);
            db.SaveChanges();

            return Ok(tipoServicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoServicioExists(int id)
        {
            return db.TipoServicios.Count(e => e.TipoServicioID == id) > 0;
        }
    }
}