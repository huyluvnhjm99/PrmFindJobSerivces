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
    public class skillController : ApiController
    {
        private fjEntities db = new fjEntities();

        // GET: api/skill
        public IQueryable<skill> Getskills()
        {
            return db.skills;
        }

        // GET: api/skill/5
        [ResponseType(typeof(skill))]
        public IHttpActionResult Getskill(int id)
        {
            skill skill = db.skills.Find(id);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // PUT: api/skill/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putskill(int id, skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.id)
            {
                return BadRequest();
            }

            db.Entry(skill).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!skillExists(id))
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

        // POST: api/skill
        [ResponseType(typeof(skill))]
        public IHttpActionResult Postskill(skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.skills.Add(skill);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = skill.id }, skill);
        }

        // DELETE: api/skill/5
        [ResponseType(typeof(skill))]
        public IHttpActionResult Deleteskill(int id)
        {
            skill skill = db.skills.Find(id);
            if (skill == null)
            {
                return NotFound();
            }

            db.skills.Remove(skill);
            db.SaveChanges();

            return Ok(skill);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool skillExists(int id)
        {
            return db.skills.Count(e => e.id == id) > 0;
        }
    }
}