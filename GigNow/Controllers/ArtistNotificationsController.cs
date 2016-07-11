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
    public class ArtistNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArtistNotifications
        public ActionResult Index()
        {
            return View(db.ArtistNotifications.ToList());
        }
        public ActionResult Inbox(int? artistId)
        {
            CheckGigs(artistId);
            var inbox = db.ArtistNotifications.Where(x => x.artist.ArtistId == artistId && x.read == false).ToList();
            return View(inbox);
        }
        public void CheckGigs(int? artistId)
        {
            var gigs = (from slot in db.Slots where slot.Artist.ArtistId == artistId select slot.Gig).ToList();
            foreach (Gig thisGig in gigs)
            {
                if (thisGig.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                {
                    var gigRemindersList = db.ArtistNotifications.Where(x => x.type == "Gig Reminder" && x.slot.Gig.GigId == thisGig.GigId && x.artist.ArtistId == artistId).ToList();
                    if (gigRemindersList.Count() == 0)
                    {
                        ArtistNotification artistNotification = new ArtistNotification
                        {
                            artist = db.Artists.Find(artistId),
                            slot = db.Slots.FirstOrDefault(x => x.Artist.ArtistId == artistId && x.Gig.GigId == thisGig.GigId),
                            type = "Gig Reminder",
                            read = false,
                            message = (thisGig.Name + "is Today!")
                        };
                        db.ArtistNotifications.Add(artistNotification);
                        db.SaveChanges();
                    }

                }
            }
        }

        // GET: ArtistNotifications/Details/5
        public ActionResult Details(int? ArtistNotificationid)
        {
            if (ArtistNotificationid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistNotification artistNotification = db.ArtistNotifications.Find(ArtistNotificationid);
            if (artistNotification == null)
            {
                return HttpNotFound();
            }
            return View(artistNotification);
        }

        // GET: ArtistNotifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistNotificationId")] ArtistNotification artistNotification)
        {
            if (ModelState.IsValid)
            {
                db.ArtistNotifications.Add(artistNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artistNotification);
        }

        // GET: ArtistNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistNotification artistNotification = db.ArtistNotifications.Find(id);
            if (artistNotification == null)
            {
                return HttpNotFound();
            }
            return View(artistNotification);
        }

        // POST: ArtistNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistNotificationId")] ArtistNotification artistNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artistNotification);
        }

        // GET: ArtistNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistNotification artistNotification = db.ArtistNotifications.Find(id);
            if (artistNotification == null)
            {
                return HttpNotFound();
            }
            return View(artistNotification);
        }

        // POST: ArtistNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistNotification artistNotification = db.ArtistNotifications.Find(id);
            db.ArtistNotifications.Remove(artistNotification);
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
        public ActionResult AcceptApplication(int? venueNotificationId)
        {
            var venueNotification = db.VenueNotifications.Find(venueNotificationId);
            venueNotification.read = true;
            db.SaveChanges();
            venueNotification.slot.Artist = venueNotification.artist;
            db.SaveChanges();
            ArtistNotification artistNotification = new ArtistNotification
            {
                artist = venueNotification.artist,
                slot = venueNotification.slot,
                type = "Application Accepted",
                read = false,
                message = (venueNotification.venue.Name+" has booked you for the "+venueNotification.slot.Order+" slot for "+venueNotification.slot.Gig.Name+" on "+venueNotification.slot.Gig.Date.ToShortDateString()+" at "+venueNotification.slot.Gig.Time.ToShortTimeString())
            };
            db.ArtistNotifications.Add(artistNotification);
            db.SaveChanges();
            var artistsListeners = (from artistRelationship in db.ArtistRelationships where artistRelationship.Artist.ArtistId == venueNotification.artist.ArtistId select artistRelationship.Listener).ToList();
            var gigsListeners = (from gigrelationship in db.GigRelationships where gigrelationship.Gig.GigId == venueNotification.slot.Gig.GigId select gigrelationship.Listener).ToList();
            GigUpdate(gigsListeners, venueNotification.slot.Gig);
            ArtistBooked(artistsListeners, venueNotification.slot.Gig, venueNotification.artist);
            return RedirectToAction("VenueView", "Venues", new { venueId = venueNotification.venue.VenueId });
        }
        public ActionResult AcceptCounterOffer(int? venueNotificationId)
        {
            var venueNotification = db.VenueNotifications.Find(venueNotificationId);
            venueNotification.read = true;
            db.SaveChanges();
            venueNotification.slot.Artist = venueNotification.artist;
            db.SaveChanges();
            ArtistNotification artistNotification = new ArtistNotification
            {
                artist = venueNotification.artist,
                slot = venueNotification.slot,
                type = "Counter-Offer Accepted",
                read = false,
                message = (venueNotification.venue.Name + " has accepted your counter-offer for the " + venueNotification.slot.Order + " slot for " + venueNotification.slot.Gig.Name + " on " + venueNotification.slot.Gig.Date.ToShortDateString() + " at " + venueNotification.slot.Gig.Time.ToShortTimeString())
            };
            db.ArtistNotifications.Add(artistNotification);
            db.SaveChanges();
            var artistsListeners = (from artistRelationship in db.ArtistRelationships where artistRelationship.Artist.ArtistId == venueNotification.artist.ArtistId select artistRelationship.Listener).ToList();
            var gigsListeners = (from gigrelationship in db.GigRelationships where gigrelationship.Gig.GigId == venueNotification.slot.Gig.GigId select gigrelationship.Listener).ToList();
            GigUpdate(gigsListeners, venueNotification.slot.Gig);
            ArtistBooked(artistsListeners, venueNotification.slot.Gig, venueNotification.artist);
            return RedirectToAction("VenueView", "Venues", new { venueId = venueNotification.venue.VenueId });
        }
        public ActionResult DeclineApplication(int? venueNotificationId)
        {
            var venueNotification = db.VenueNotifications.Find(venueNotificationId);
            venueNotification.read = true;
            db.SaveChanges();
            ArtistNotification artistNotification = new ArtistNotification
            {
                artist = venueNotification.artist,
                slot = venueNotification.slot,
                type = "Application Declined",
                read = false,
                message = (venueNotification.venue.Name + " has declined your application for the " + venueNotification.slot.Order + " slot for " + venueNotification.slot.Gig.Name + " on " + venueNotification.slot.Gig.Date.ToShortDateString() + " at " + venueNotification.slot.Gig.Time.ToShortTimeString())
            };
            db.ArtistNotifications.Add(artistNotification);
            db.SaveChanges();
            return RedirectToAction("VenueView", "Venues", new { venueId = venueNotification.venue.VenueId });
        }
        public ActionResult DeclineCounterOffer(int? venueNotificationId)
        {
            var venueNotification = db.VenueNotifications.Find(venueNotificationId);
            venueNotification.read = true;
            db.SaveChanges();
            ArtistNotification artistNotification = new ArtistNotification
            {
                artist = venueNotification.artist,
                slot = venueNotification.slot,
                type = "Counter-Offer Declined",
                read = false,
                message = (venueNotification.venue.Name + " has declined your counter-offer for the " + venueNotification.slot.Order + " slot for " + venueNotification.slot.Gig.Name + " on " + venueNotification.slot.Gig.Date.ToShortDateString() + " at " + venueNotification.slot.Gig.Time.ToShortTimeString())
            };
            db.ArtistNotifications.Add(artistNotification);
            db.SaveChanges();
            return RedirectToAction("VenueView", "Venues", new { venueId = venueNotification.venue.VenueId });
        }
        public void GigUpdate(List<Listener> listeners, Gig thisGig)
        {
            foreach(Listener thisListener in listeners)
            {
                ListenerNotification listenerNotification = new ListenerNotification
                {
                    gig = thisGig,
                    listener = thisListener,
                    type = "Gig Update",
                    read = false,
                    message = (thisGig.Venue.Name+ " has updated "+thisGig.Name)
                };
                db.ListenerNotifications.Add(listenerNotification);
                db.SaveChanges();
            }
        }
        public void ArtistBooked(List<Listener> listeners, Gig thisGig, Artist thisArtist)
        {
            foreach (Listener thisListener in listeners)
            {
                ListenerNotification listenerNotification = new ListenerNotification
                {
                    gig = thisGig,
                    listener = thisListener,
                    type = "Artist Booked",
                    read = false,
                    message = (thisArtist.Name +"is playing "+thisGig.Name+" at "+thisGig.Venue.Name+" on "+thisGig.Date.ToShortDateString()+" at "+thisGig.Time.ToShortTimeString())
                };
                db.ListenerNotifications.Add(listenerNotification);
                db.SaveChanges();
            }
        }
        public ActionResult MarkAsReadThenGig(int? artistNotificationId)
        {
            var artistNotification = db.ArtistNotifications.Find(artistNotificationId);
            artistNotification.read = true;
            db.SaveChanges();
            return RedirectToAction("GigView", "Gigs", new {gigId = artistNotification.slot.Gig.GigId, partial = "false"});
        }
        public ActionResult MarkAsReadThenArtist(int? artistNotificationId)
        {
            var artistNotification = db.ArtistNotifications.Find(artistNotificationId);
            artistNotification.read = true;
            db.SaveChanges();
            return RedirectToAction("ArtistView", "Artists", new { artistId = artistNotification.artist.ArtistId, partial = "false" });
        }
    }
}
