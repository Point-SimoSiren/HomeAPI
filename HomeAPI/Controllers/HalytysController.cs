using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HomeAPI.Models;

namespace HomeAPI.Controllers
{
    public class HalytysController : ApiController
    {
        private kotidbEntities db = new kotidbEntities();

        // GET: api/Halytys
        public IQueryable<Halyty> GetHalytys()
        {
            return db.Halytys;
        }

        // GET: api/Halytys/5
        [ResponseType(typeof(Halyty))]
        public async Task<IHttpActionResult> GetHalyty(int id)
        {
            Halyty halyty = await db.Halytys.FindAsync(id);
            if (halyty == null)
            {
                return NotFound();
            }

            return Ok(halyty);
        }

        // PUT: api/Halytys/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHalyty(int id, Halyty halyty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != halyty.HalytinId)
            {
                return BadRequest();
            }

            db.Entry(halyty).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HalytyExists(id))
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

        // POST: api/Halytys
        [ResponseType(typeof(Halyty))]
        public async Task<IHttpActionResult> PostHalyty(Halyty halyty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Halytys.Add(halyty);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = halyty.HalytinId }, halyty);
        }

        // DELETE: api/Halytys/5
        [ResponseType(typeof(Halyty))]
        public async Task<IHttpActionResult> DeleteHalyty(int id)
        {
            Halyty halyty = await db.Halytys.FindAsync(id);
            if (halyty == null)
            {
                return NotFound();
            }

            db.Halytys.Remove(halyty);
            await db.SaveChangesAsync();

            return Ok(halyty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HalytyExists(int id)
        {
            return db.Halytys.Count(e => e.HalytinId == id) > 0;
        }
    }
}