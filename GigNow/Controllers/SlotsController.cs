﻿using System;
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
    public class SlotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Slots
        public ActionResult Index(List<Slot> bill)
        {
            return View(bill);
        }

        // GET: Slots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slot slot = db.Slots.Find(id);
            if (slot == null)
            {
                return HttpNotFound();
            }
            return View(slot);
        }

        // GET: Slots/Create
        public ActionResult Create(int gigId)
        {
            var Gig = db.Gigs.Find(gigId);
            Slot slot = new Slot
            {
                GigId = gigId,
                Compensation = Gig.DefaultCompensation,
                Perks = Gig.DefaultPerks,
                Genre = Gig.DefaultGenre,
                ArtistType = Gig.DefaultArtistType
            };
            return View(slot);
        }

        // POST: Slots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slot slot)
        {
            if (ModelState.IsValid)
            {
                db.Slots.Add(slot);
                db.SaveChanges();
                return RedirectToAction("GigView","Gigs", new {gigId = slot.GigId });
            }

            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", slot.GigId);
            return View(slot);
        }

        // GET: Slots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slot slot = db.Slots.Find(id);
            if (slot == null)
            {
                return HttpNotFound();
            }
            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", slot.GigId);
            return View(slot);
        }

        // POST: Slots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotId,Compensation,Genre,IsFilled,ArtistType,Perks,Order,Length,UseGigDefaults,GigId")] Slot slot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", slot.GigId);
            return View(slot);
        }

        // GET: Slots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slot slot = db.Slots.Find(id);
            if (slot == null)
            {
                return HttpNotFound();
            }
            return View(slot);
        }

        // POST: Slots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slot slot = db.Slots.Find(id);
            db.Slots.Remove(slot);
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
