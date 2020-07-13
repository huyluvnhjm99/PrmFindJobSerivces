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
    public class jobController : ApiController
    {
        private fjEntities db = new fjEntities();

        // GET: api/job
        public IQueryable<job> Getjobs()
        {
            return db.jobs;
        }

        // GET: api/job/5
        [ResponseType(typeof(job))]
        public IHttpActionResult Getjob(int id)
        {
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/job/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putjob(int id, job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.id)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!jobExists(id))
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

        // POST: api/job
        [ResponseType(typeof(job))]
        public IHttpActionResult Postjob(job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.jobs.Add(job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job.id }, job);
        }

        // DELETE: api/job/5
        [ResponseType(typeof(job))]
        public IHttpActionResult Deletejob(int id)
        {
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            db.jobs.Remove(job);
            db.SaveChanges();

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool jobExists(int id)
        {
            return db.jobs.Count(e => e.id == id) > 0;
        }
    }
}