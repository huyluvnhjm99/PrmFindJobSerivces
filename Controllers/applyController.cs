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
using PrmFindJobSerivces.Models;

namespace PrmFindJobSerivces.Controllers
{
    public class applyController : ApiController
    {
        private fjEntities db = new fjEntities();

        // GET: api/apply
        public IQueryable<apply> Getapplies()
        {
            return db.applies;
        }

        // GET: api/apply/5
        [ResponseType(typeof(apply))]
        public IHttpActionResult Getapply(int id)
        {
            apply apply = db.applies.Find(id);
            if (apply == null)
            {
                return NotFound();
            }

            return Ok(apply);
        }

        // PUT: api/apply/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapply(int id, apply apply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != apply.id)
            {
                return BadRequest();
            }

            db.Entry(apply).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!applyExists(id))
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

        // POST: api/apply
        [ResponseType(typeof(apply))]
        public IHttpActionResult Postapply(apply apply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.applies.Add(apply);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = apply.id }, apply);
        }

        // DELETE: api/apply/5
        [ResponseType(typeof(apply))]
        public IHttpActionResult Deleteapply(int id)
        {
            apply apply = db.applies.Find(id);
            if (apply == null)
            {
                return NotFound();
            }

            db.applies.Remove(apply);
            db.SaveChanges();

            return Ok(apply);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool applyExists(int id)
        {
            return db.applies.Count(e => e.id == id) > 0;
        }
    }
}