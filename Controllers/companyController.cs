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
    public class companyController : ApiController
    {
        private fjEntities db = new fjEntities();

        // GET: api/company
        public IQueryable<company> Getcompanies()
        {
            return db.companies;
        }

        // GET: api/company/5
        [ResponseType(typeof(company))]
        public IHttpActionResult Getcompany(string id)
        {
            company company = db.companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/company/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcompany(string id, company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.company_name)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!companyExists(id))
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

        // POST: api/company
        [ResponseType(typeof(company))]
        public IHttpActionResult Postcompany(company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.companies.Add(company);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (companyExists(company.company_name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = company.company_name }, company);
        }

        // DELETE: api/company/5
        [ResponseType(typeof(company))]
        public IHttpActionResult Deletecompany(string id)
        {
            company company = db.companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.companies.Remove(company);
            db.SaveChanges();

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool companyExists(string id)
        {
            return db.companies.Count(e => e.company_name == id) > 0;
        }
    }
}