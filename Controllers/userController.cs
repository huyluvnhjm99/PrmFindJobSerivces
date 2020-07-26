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
    [RoutePrefix("api/user")]
    public class userController : ApiController
    {
        private fjEntities db = new fjEntities();

        // GET: api/user
        public IQueryable<user> Getusers()
        {
            return db.users;
        }

        [Route("gmail")]
        [ResponseType(typeof(user))]
        public IQueryable<user> Getuser(string gmail)
        {
            return db.users.Where(e => e.gmail == gmail);
        }


        // GET: api/user/5

        // PUT: api/user/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser(string id, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.gmail)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        // POST: api/user
        [ResponseType(typeof(user))]
        public IHttpActionResult Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (userExists(user.gmail))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.gmail }, user);
        }

        // DELETE: api/user/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Deleteuser(string id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(string id)
        {
            return db.users.Count(e => e.gmail == id) > 0;
        }
    }
}