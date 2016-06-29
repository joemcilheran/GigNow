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
    public class ArtistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Artists
        public ActionResult Index()
        {
            var artists = db.Artists.Include(a => a.address).Include(a => a.AppUser).Include(a => a.photo).Include(a => a.track1).Include(a => a.track2).Include(a => a.track3).Include(a => a.video);
            return View(artists.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress");
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Name");
            ViewBag.track1Id = new SelectList(db.Tracks, "TraclId", "Name");
            ViewBag.track2Id = new SelectList(db.Tracks, "TraclId", "Name");
            ViewBag.track3Id = new SelectList(db.Tracks, "TraclId", "Name");
            ViewBag.VideoId = new SelectList(db.Videos, "VideoId", "Name");
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistId,Name,AddressId,ContactName,Genre1,Genre2,Genre3,Type,NumberOfMembers,PhotoId,VideoId,track1Id,track2Id,track3Id,UserId")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.AddressId);
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", artist.UserId);
            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Name", artist.PhotoId);
            ViewBag.track1Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track1Id);
            ViewBag.track2Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track2Id);
            ViewBag.track3Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track3Id);
            ViewBag.VideoId = new SelectList(db.Videos, "VideoId", "Name", artist.VideoId);
            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.AddressId);
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", artist.UserId);
            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Name", artist.PhotoId);
            ViewBag.track1Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track1Id);
            ViewBag.track2Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track2Id);
            ViewBag.track3Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track3Id);
            ViewBag.VideoId = new SelectList(db.Videos, "VideoId", "Name", artist.VideoId);
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistId,Name,AddressId,ContactName,Genre1,Genre2,Genre3,Type,NumberOfMembers,PhotoId,VideoId,track1Id,track2Id,track3Id,UserId")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.AddressId);
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", artist.UserId);
            ViewBag.PhotoId = new SelectList(db.Photos, "PhotoId", "Name", artist.PhotoId);
            ViewBag.track1Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track1Id);
            ViewBag.track2Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track2Id);
            ViewBag.track3Id = new SelectList(db.Tracks, "TraclId", "Name", artist.track3Id);
            ViewBag.VideoId = new SelectList(db.Videos, "VideoId", "Name", artist.VideoId);
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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
