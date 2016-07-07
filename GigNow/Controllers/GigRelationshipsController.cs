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
    public class GigRelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GigRelationships
        public ActionResult Index()
        {
            var gigRelationships = db.GigRelationships.Include(g => g.Gig).Include(g => g.Listener);
            return View(gigRelationships.ToList());
        }

        // GET: GigRelationships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GigRelationship gigRelationship = db.GigRelationships.Find(id);
            if (gigRelationship == null)
            {
                return HttpNotFound();
            }
            return View(gigRelationship);
        }

        // GET: GigRelationships/Create
        public ActionResult Create(int? gigId)
        {
            var gig = db.Gigs.Find(gigId);
            var userId = User.Identity.GetUserId();
            var Listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
            GigRelationship gigrelationship = new GigRelationship
            {
                Listener = Listener,
                Gig = gig
            };
            db.GigRelationships.Add(gigrelationship);
            db.SaveChanges();
            return RedirectToAction("GigView", "Gigs", new { GigId = gig.GigId });
        }

        // GET: GigRelationships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GigRelationship gigRelationship = db.GigRelationships.Find(id);
            if (gigRelationship == null)
            {
                return HttpNotFound();
            }
            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", gigRelationship.Gig.GigId);
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId", gigRelationship.Listener.ListenerID);
            return View(gigRelationship);
        }

        // POST: GigRelationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GigRelationshipId,ListenerId,GigId")] GigRelationship gigRelationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gigRelationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", gigRelationship.Gig.GigId);
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId", gigRelationship.Listener.ListenerID);
            return View(gigRelationship);
        }

        // GET: GigRelationships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GigRelationship gigRelationship = db.GigRelationships.Find(id);
            if (gigRelationship == null)
            {
                return HttpNotFound();
            }
            return View(gigRelationship);
        }

        // POST: GigRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GigRelationship gigRelationship = db.GigRelationships.Find(id);
            db.GigRelationships.Remove(gigRelationship);
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
