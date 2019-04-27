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
    public class HuoneController : ApiController
    {
        private kotidbEntities db = new kotidbEntities();

        // GET: api/Huone
        public IQueryable<Huone> GetHuones()
        {
            return db.Huones;
        }

        // GET: api/Huone/5
        [ResponseType(typeof(Huone))]
        public async Task<IHttpActionResult> GetHuone(int id)
        {
            Huone huone = await db.Huones.FindAsync(id);
            if (huone == null)
            {
                return NotFound();
            }

            return Ok(huone);
        }

        // PUT: api/Huone/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHuone(int id, Huone huone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != huone.HuoneId)
            {
                return BadRequest();
            }

            db.Entry(huone).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HuoneExists(id))
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

        // POST: api/Huone
        [ResponseType(typeof(Huone))]
        public async Task<IHttpActionResult> PostHuone(Huone huone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Huones.Add(huone);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = huone.HuoneId }, huone);
        }

        // DELETE: api/Huone/5
        [ResponseType(typeof(Huone))]
        public async Task<IHttpActionResult> DeleteHuone(int id)
        {
            Huone huone = await db.Huones.FindAsync(id);
            if (huone == null)
            {
                return NotFound();
            }

            db.Huones.Remove(huone);
            await db.SaveChangesAsync();

            return Ok(huone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HuoneExists(int id)
        {
            return db.Huones.Count(e => e.HuoneId == id) > 0;
        }
    }
}