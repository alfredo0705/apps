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
    public class HoraAtencionesController : ApiController
    {
        private TurnosContext db = new TurnosContext();

        // GET: api/HoraAtenciones
        public IQueryable<HoraAtencion> GetHoraAtencions()
        {
            return db.HoraAtencions;
        }

        // GET: api/HoraAtenciones/5
        [ResponseType(typeof(HoraAtencion))]
        public IHttpActionResult GetHoraAtencion(int id)
        {
            HoraAtencion horaAtencion = db.HoraAtencions.Find(id);
            if (horaAtencion == null)
            {
                return NotFound();
            }

            return Ok(horaAtencion);
        }

        // PUT: api/HoraAtenciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHoraAtencion(int id, HoraAtencion horaAtencion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horaAtencion.HoraAtencionID)
            {
                return BadRequest();
            }

            db.Entry(horaAtencion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoraAtencionExists(id))
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

        // POST: api/HoraAtenciones
        [ResponseType(typeof(HoraAtencion))]
        public IHttpActionResult PostHoraAtencion(HoraAtencion horaAtencion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HoraAtencions.Add(horaAtencion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = horaAtencion.HoraAtencionID }, horaAtencion);
        }

        // DELETE: api/HoraAtenciones/5
        [ResponseType(typeof(HoraAtencion))]
        public IHttpActionResult DeleteHoraAtencion(int id)
        {
            HoraAtencion horaAtencion = db.HoraAtencions.Find(id);
            if (horaAtencion == null)
            {
                return NotFound();
            }

            db.HoraAtencions.Remove(horaAtencion);
            db.SaveChanges();

            return Ok(horaAtencion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HoraAtencionExists(int id)
        {
            return db.HoraAtencions.Count(e => e.HoraAtencionID == id) > 0;
        }
    }
}