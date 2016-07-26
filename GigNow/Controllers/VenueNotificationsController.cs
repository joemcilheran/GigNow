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
    public class VenueNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueNotifications
        public ActionResult Index()
        {
            return View(db.VenueNotifications.ToList());
        }
        public ActionResult Inbox()
        {
            var userId = User.Identity.GetUserId();
            var Venue = db.Venues.FirstOrDefault(x => x.UserId == userId);
            var venueId = Venue.VenueId;
            ViewBag.Name = Venue.Name;
            CheckGigs(venueId);
            var inbox = db.VenueNotifications.Where(x => x.venue.VenueId == venueId && x.read == false).ToList();
            return View(inbox);
        }
        public void CheckGigs(int? venueId)
        {
            var gigs = db.Gigs.Where(x => x.Venue.VenueId == venueId).ToList();
            foreach(Gig thisGig in gigs)
            {
                if(thisGig.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                {
                    var gigRemindersList = db.VenueNotifications.Where(x => x.type == "Gig Reminder" && x.slot.Gig.GigId == thisGig.GigId).ToList();
                    if (gigRemindersList.Count() == 0)
                    {
                        VenueNotification venueNotification = new VenueNotification
                        {
                            venue = db.Venues.Find(venueId),
                            slot = db.Slots.First(x => x.Gig.GigId == thisGig.GigId),
                            type = "Gig Reminder",
                            read = false,
                            message = (thisGig.Name + " is Today!")
                        };
                        db.VenueNotifications.Add(venueNotification);
                        db.SaveChanges();
                    }


                }
            }
        }

        // GET: VenueNotifications/Details/5
        public ActionResult Details(int? venueNotificationId)
        {
            if (venueNotificationId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueNotification venueNotification = db.VenueNotifications.Find(venueNotificationId);
            if (venueNotification == null)
            {
                return HttpNotFound();
            }
            return View(venueNotification);
        }

        // GET: VenueNotifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VenueNotificationId")] VenueNotification venueNotification)
        {
            if (ModelState.IsValid)
            {
                db.VenueNotifications.Add(venueNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venueNotification);
        }

        // GET: VenueNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueNotification venueNotification = db.VenueNotifications.Find(id);
            if (venueNotification == null)
            {
                return HttpNotFound();
            }
            return View(venueNotification);
        }

        // POST: VenueNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VenueNotificationId")] VenueNotification venueNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venueNotification);
        }

        // GET: VenueNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueNotification venueNotification = db.VenueNotifications.Find(id);
            if (venueNotification == null)
            {
                return HttpNotFound();
            }
            return View(venueNotification);
        }

        // POST: VenueNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueNotification venueNotification = db.VenueNotifications.Find(id);
            db.VenueNotifications.Remove(venueNotification);
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
        public ActionResult MarkAsReadThenGig(int? venueNotificationId)
        {
            var venueNotification = db.VenueNotifications.Find(venueNotificationId);
            venueNotification.read = true;
            db.SaveChanges();
            return RedirectToAction("GigView", "Gigs", new { gigId = venueNotification.slot.Gig.GigId, partial = "false" });
        }
        public ActionResult MarkAsReadThenVenue(int? venueNotificationId)
        {
            var venueNotification = db.VenueNotifications.Find(venueNotificationId);
            venueNotification.read = true;
            db.SaveChanges();
            return RedirectToAction("VenueView", "Venues", new { venueId = venueNotification.venue.VenueId });
        }
        public ActionResult Apply(int? artistNotificationId)
        {
            var artistNotification = db.ArtistNotifications.Find(artistNotificationId);
            artistNotification.read = true;
            db.SaveChanges();
            VenueNotification venueNotification = new VenueNotification
            {
                venue = artistNotification.slot.Gig.Venue,
                message = (artistNotification.artist.Name + " applied to play the " + artistNotification.slot.Order + " slot at " + artistNotification.slot.Gig.Name + " on " + artistNotification.slot.Gig.Date.ToShortDateString()),
                type = "Slot Application",
                read = false,
                artist = artistNotification.artist,
                slot = artistNotification.slot

            };
            db.VenueNotifications.Add(venueNotification);
            db.SaveChanges();
            return RedirectToAction("ArtistView", "Artists", new { artistId = artistNotification.artist.ArtistId, partial = "false" });
        }
        [HttpGet]
        public ActionResult CounterOffer(int? artistNotificationId)
        {
            var artistNotification = db.ArtistNotifications.Find(artistNotificationId);
            artistNotification.read = true;
            db.SaveChanges();
            return View(artistNotification);    
        }
        [HttpPost]
        public ActionResult CounterOffer(int? counterOffer, int? slotId, int? artistId)
        {
            var thisSlot = db.Slots.Find(slotId);
            var thisArtist = db.Artists.Find(artistId);
            VenueNotification venueNotification = new VenueNotification
            {
                venue = thisSlot.Gig.Venue,
                artist = thisArtist,
                slot = thisSlot,
                type = "Counter-Offer",
                message = (thisArtist.Name + " submitted a counter-offer for the " + thisSlot.Order + " slot for " + thisSlot.Gig.Name + " on " + thisSlot.Gig.Date.ToShortDateString() + ". Original offer: $" + thisSlot.Compensation + ". Counter-offer: $" + counterOffer),
                read = false
            };
            db.VenueNotifications.Add(venueNotification);
            db.SaveChanges();
            return RedirectToAction("ArtistView", "Artists", new { artistId = artistId, partial = "false" });
        }
    }
}
