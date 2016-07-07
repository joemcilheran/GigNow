using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GigNow.Models;
using Microsoft.AspNet.Identity;

namespace GigNow.Controllers
{
    public class VenueRelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueRelationships
        public ActionResult Index()
        {
            var venueRelationships = db.VenueRelationships.Include(v => v.Listener).Include(v => v.Venue);
            return View(venueRelationships.ToList());
        }

        // GET: VenueRelationships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueRelationship venueRelationship = db.VenueRelationships.Find(id);
            if (venueRelationship == null)
            {
                return HttpNotFound();
            }
            return View(venueRelationship);
        }

        // GET: VenueRelationships/Create
        public ActionResult Create(int? venueId)
        {
            var userId = User.Identity.GetUserId();
            var Listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
            VenueRelationship venuerelationship = new VenueRelationship
            {
                ListenerId = Listener.ListenerID,
                VenueId = venueId
            };
            db.VenueRelationships.Add(venuerelationship);
            db.SaveChanges();
            return RedirectToAction("VenueView", "Venues", new { VenueId = venueId });
        }

        // GET: VenueRelationships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueRelationship venueRelationship = db.VenueRelationships.Find(id);
            if (venueRelationship == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId", venueRelationship.ListenerId);
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "Name", venueRelationship.VenueId);
            return View(venueRelationship);
        }

        // POST: VenueRelationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VenueRelationshipId,ListenerId,VenueId")] VenueRelationship venueRelationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueRelationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId", venueRelationship.ListenerId);
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "Name", venueRelationship.VenueId);
            return View(venueRelationship);
        }

        // GET: VenueRelationships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueRelationship venueRelationship = db.VenueRelationships.Find(id);
            if (venueRelationship == null)
            {
                return HttpNotFound();
            }
            return View(venueRelationship);
        }

        // POST: VenueRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueRelationship venueRelationship = db.VenueRelationships.Find(id);
            db.VenueRelationships.Remove(venueRelationship);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
