using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GigNow.Models;

namespace GigNow.Controllers
{
    public class ArtistRelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArtistRelationships
        public ActionResult Index()
        {
            var artistRelationships = db.ArtistRelationships.Include(a => a.Artist).Include(a => a.Listener);
            return View(artistRelationships.ToList());
        }

        // GET: ArtistRelationships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistRelationship artistRelationship = db.ArtistRelationships.Find(id);
            if (artistRelationship == null)
            {
                return HttpNotFound();
            }
            return View(artistRelationship);
        }

        // GET: ArtistRelationships/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId");
            return View();
        }

        // POST: ArtistRelationships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistRelationshipId,ListenerId,ArtistId")] ArtistRelationship artistRelationship)
        {
            if (ModelState.IsValid)
            {
                db.ArtistRelationships.Add(artistRelationship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artistRelationship.ArtistId);
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId", artistRelationship.ListenerId);
            return View(artistRelationship);
        }

        // GET: ArtistRelationships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistRelationship artistRelationship = db.ArtistRelationships.Find(id);
            if (artistRelationship == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artistRelationship.ArtistId);
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId", artistRelationship.ListenerId);
            return View(artistRelationship);
        }

        // POST: ArtistRelationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistRelationshipId,ListenerId,ArtistId")] ArtistRelationship artistRelationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistRelationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", artistRelationship.ArtistId);
            ViewBag.ListenerId = new SelectList(db.Listeners, "ListenerID", "UserId", artistRelationship.ListenerId);
            return View(artistRelationship);
        }

        // GET: ArtistRelationships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistRelationship artistRelationship = db.ArtistRelationships.Find(id);
            if (artistRelationship == null)
            {
                return HttpNotFound();
            }
            return View(artistRelationship);
        }

        // POST: ArtistRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistRelationship artistRelationship = db.ArtistRelationships.Find(id);
            db.ArtistRelationships.Remove(artistRelationship);
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
