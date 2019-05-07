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
    public class LammitysController : ApiController
    {
        private kotidbEntities db = new kotidbEntities();

        // GET: api/Lammitys
        public IQueryable<Lammity> GetLammitys()
        {
            return db.Lammitys;
        }

        // GET: api/Lammitys/5
        [ResponseType(typeof(Lammity))]
        public async Task<IHttpActionResult> GetLammity(int id)
        {
            Lammity lammity = await db.Lammitys.FindAsync(id);
            if (lammity == null)
            {
                return NotFound();
            }

            return Ok(lammity);
        }

        // PUT: api/Lammitys/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLammity(int id, Lammity lammity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lammity.LammitinId)
            {
                return BadRequest();
            }

            db.Entry(lammity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LammityExists(id))
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

        // POST: api/Lammitys
        [ResponseType(typeof(Lammity))]
        public async Task<IHttpActionResult> PostLammity(Lammity lammity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lammitys.Add(lammity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lammity.LammitinId }, lammity);
        }

        // DELETE: api/Lammitys/5
        [ResponseType(typeof(Lammity))]
        public async Task<IHttpActionResult> DeleteLammity(int id)
        {
            Lammity lammity = await db.Lammitys.FindAsync(id);
            if (lammity == null)
            {
                return NotFound();
            }

            db.Lammitys.Remove(lammity);
            await db.SaveChangesAsync();

            return Ok(lammity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LammityExists(int id)
        {
            return db.Lammitys.Count(e => e.LammitinId == id) > 0;
        }
    }
}