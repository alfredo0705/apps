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
    public class TurnosController : ApiController
    {
        private TurnosContext db = new TurnosContext();

        // GET: api/Turnos
        public IQueryable<Turno> GetTurnoes()
        {
            return db.Turnoes;
        }

        // GET: api/Turnos/5
        [ResponseType(typeof(Turno))]
        public IHttpActionResult GetTurno(int id)
        {
            Turno turno = db.Turnoes.Find(id);
            if (turno == null)
            {
                return NotFound();
            }

            return Ok(turno);
        }

        // PUT: api/Turnos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTurno(int id, Turno turno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != turno.TurnoID)
            {
                return BadRequest();
            }

            db.Entry(turno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoExists(id))
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

        // POST: api/Turnos
        [ResponseType(typeof(Turno))]
        public IHttpActionResult PostTurno(Turno turno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Turnoes.Add(turno);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = turno.TurnoID }, turno);
        }

        // DELETE: api/Turnos/5
        [ResponseType(typeof(Turno))]
        public IHttpActionResult DeleteTurno(int id)
        {
            Turno turno = db.Turnoes.Find(id);
            if (turno == null)
            {
                return NotFound();
            }

            db.Turnoes.Remove(turno);
            db.SaveChanges();

            return Ok(turno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TurnoExists(int id)
        {
            return db.Turnoes.Count(e => e.TurnoID == id) > 0;
        }
    }
}