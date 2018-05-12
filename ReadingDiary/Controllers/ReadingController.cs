﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ReadingDiary.Models;

namespace ReadingDiary.Controllers
{
    public class ReadingController : ApiController
    {
        private ReadingDiaryEntities db = new ReadingDiaryEntities();

        // GET: api/Reading
        public IQueryable<Reading> GetReadings()
        {
            return db.Readings;
        }

        // GET: api/Reading/5
        [ResponseType(typeof(Reading))]
        public IHttpActionResult GetReading(int id)
        {
            Reading reading = db.Readings.Find(id);
            if (reading == null)
            {
                return NotFound();
            }

            return Ok(reading);
        }

        // PUT: api/Reading/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReading(int id, Reading reading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reading.Id)
            {
                return BadRequest();
            }

            db.Entry(reading).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReadingExists(id))
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

        // POST: api/Reading
        [ResponseType(typeof(Reading))]
        public IHttpActionResult PostReading(Reading reading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Readings.Add(reading);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reading.Id }, reading);
        }

        // DELETE: api/Reading/5
        [ResponseType(typeof(Reading))]
        public IHttpActionResult DeleteReading(int id)
        {
            Reading reading = db.Readings.Find(id);
            if (reading == null)
            {
                return NotFound();
            }

            db.Readings.Remove(reading);
            db.SaveChanges();

            return Ok(reading);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReadingExists(int id)
        {
            return db.Readings.Count(e => e.Id == id) > 0;
        }
    }
}