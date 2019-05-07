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
    public class SaunaController : ApiController
    {
        private kotidbEntities db = new kotidbEntities();

        // GET: api/Sauna
        public IQueryable<Sauna> GetSaunas()
        {
            return db.Saunas;
        }

        // GET: api/Sauna/5
        [ResponseType(typeof(Sauna))]
        public async Task<IHttpActionResult> GetSauna(int id)
        {
            Sauna sauna = await db.Saunas.FindAsync(id);
            if (sauna == null)
            {
                return NotFound();
            }

            return Ok(sauna);
        }

        // PUT: api/Sauna/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSauna(int id, Sauna sauna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sauna.SaunaId)
            {
                return BadRequest();
            }

            db.Entry(sauna).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaunaExists(id))
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

        // POST: api/Sauna
        [ResponseType(typeof(Sauna))]
        public async Task<IHttpActionResult> PostSauna(Sauna sauna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Saunas.Add(sauna);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sauna.SaunaId }, sauna);
        }

        // DELETE: api/Sauna/5
        [ResponseType(typeof(Sauna))]
        public async Task<IHttpActionResult> DeleteSauna(int id)
        {
            Sauna sauna = await db.Saunas.FindAsync(id);
            if (sauna == null)
            {
                return NotFound();
            }

            db.Saunas.Remove(sauna);
            await db.SaveChangesAsync();

            return Ok(sauna);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaunaExists(int id)
        {
            return db.Saunas.Count(e => e.SaunaId == id) > 0;
        }
    }
}