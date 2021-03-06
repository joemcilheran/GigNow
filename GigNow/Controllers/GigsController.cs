﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GigNow.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GigNow.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        SmsController Sms = new SmsController(); 
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        // GET: Gigs/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var venue = db.Venues.FirstOrDefault(x => x.UserId == userId);
            Gig gig = new Gig
            {
                Venue = venue,
                DefaultArtistType = venue.DefaultArtistType,
                DefaultCompensation = venue.DefaultCompensation,
                DefaultGenre = venue.DefaultGenre,
                DefaultPerks = venue.DefaultPerks,
                LoadInInsrtuctions = venue.LoadInInstructions
            };
            return View(gig);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gig gig)
        {
            if (ModelState.IsValid)
            {
                gig.Venue = db.Venues.Find(gig.Venue.VenueId);
                db.Gigs.Add(gig);
                db.SaveChanges();
                return RedirectToAction("GigView", new {gigId = gig.GigId, partial = "false" });
            }


            return View(gig);
        }

        //GET: Gigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gig gig = db.Gigs.Find(id);
            if (gig == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "Name", gig.Venue.VenueId);
            return View(gig);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GigId,Cover,Date,Time,DefaultGenre,DefaulCompensation,Name,UseVenueDefaults,VenueId")] Gig gig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "Name", gig.Venue.VenueId);
            return View(gig);
        }

        //GET: Gigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gig gig = db.Gigs.Find(id);
            if (gig == null)
            {
                return HttpNotFound();
            }
            return View(gig);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gig gig = db.Gigs.Find(id);
            db.Gigs.Remove(gig);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult GigView(int? gigId, string partial)
        {
            ViewBag.Partial = partial;    
            var Gig = db.Gigs.Find(gigId);
            var Venue = db.Venues.FirstOrDefault(x => x.VenueId == Gig.Venue.VenueId);
            ViewBag.PhotoId = (from photo in db.Photos where photo.Venue.VenueId == Venue.VenueId select photo.PhotoId).ToList()[0];
            MapsController MC = new MapsController();
            string Destination = MC.generateDestination(Venue);
            ViewBag.Map = MC.generateMap(Destination);
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var s = UserManager.GetRoles(userId);
                string role = s[0].ToString();
                if (userId == Venue.UserId)
                {
                    ViewBag.User = "Gig Admin";
                }
                else if (role == "Artist Manager")
                {
                    var artist = db.Artists.FirstOrDefault(x => x.UserId == userId);
                    string Origin = MC.generateOrigin(artist);
                    ViewBag.Route = MC.generateRoute(Origin, Destination);
                    ViewBag.User = "Artist";
                }
                else if (role == "Listener")
                {
                    ViewBag.User = "Listener";
                    var listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
                    string Origin = MC.generateOrigin(listener);
                    ViewBag.Route = MC.generateRoute(Origin, Destination);
                    var relationshipList = db.GigRelationships.Where(x => x.Listener.ListenerID == listener.ListenerID && x.Gig.GigId == gigId).ToList();
                    if (relationshipList.Count == 0)
                    {
                        ViewBag.Watched = "false";
                    }
                    else
                    {
                        ViewBag.Watched = "true";
                    }
                }
                else
                {
                    ViewBag.User = "other";
                }
            }
            else
            {
                ViewBag.User = "Visiter";
            }

            GigViewModelVM GVM = new GigViewModelVM
             {
                gig = Gig,
                venue = Venue,
                bill = db.Slots.Where(x => x.Gig.GigId == Gig.GigId).ToList()
            };
            return View(GVM);
        }
        public ActionResult ShowWatchList(List<Gig> watchList)
        {
            return View(watchList);
        }
        public ActionResult ShowGigList(List<Gig> gigList, string gigView)
        {
            ViewBag.gigView = gigView;
            return View(gigList);
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View(new List<Gig>());
        }
        [HttpPost]
        public ActionResult Search(string gigGenre, string gigCity, DateTime? date)
        {

            var gigSearchResultList = new List<Gig>();
            if (!string.IsNullOrWhiteSpace(gigGenre) && !string.IsNullOrWhiteSpace(gigCity) && date.HasValue)
            {
                var searchDate = date.GetValueOrDefault();
                var genreGigs = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig);               
                gigSearchResultList = genreGigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity && x.Date == searchDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(gigGenre) && string.IsNullOrWhiteSpace(gigCity) && !date.HasValue)
            {
                gigSearchResultList = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig).Where(x => x.Date >= DateTime.Today).ToList();
            }
            else if (string.IsNullOrWhiteSpace(gigGenre) && !string.IsNullOrWhiteSpace(gigCity) && !date.HasValue)
            {
                gigSearchResultList = db.Gigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity && x.Date >= DateTime.Today).ToList();
            }
            else if (string.IsNullOrWhiteSpace(gigGenre) && string.IsNullOrWhiteSpace(gigCity) && date.HasValue)
            {
                var searchDate = date.GetValueOrDefault();
                gigSearchResultList = db.Gigs.Where(x => x.Date == searchDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(gigGenre) && !string.IsNullOrWhiteSpace(gigCity) && !date.HasValue)
            {
                var genreGigs = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig);
                gigSearchResultList = genreGigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity && x.Date >= DateTime.Today).ToList();
            }
            else if (string.IsNullOrWhiteSpace(gigGenre) && !string.IsNullOrWhiteSpace(gigCity) && date.HasValue)
            {
                var searchDate = date.GetValueOrDefault();
                gigSearchResultList = db.Gigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity && x.Date == searchDate).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(gigGenre) && string.IsNullOrWhiteSpace(gigCity) && date.HasValue)
            {
                var searchDate = date.GetValueOrDefault();
                var genreGigs = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig);
                gigSearchResultList = genreGigs.Where(x => x.Date == searchDate).ToList();
            }
            var newList =  checkGig(gigSearchResultList);
            return View(newList);
        }
        public ActionResult Finish(int? gigId)
        {
            var thisGig = db.Gigs.Find(gigId);
            var connectedListeners = (from venueRelationship in db.VenueRelationships where venueRelationship.Venue.VenueId == thisGig.Venue.VenueId select venueRelationship.Listener).ToList();
            CreateGigNotifications(connectedListeners, thisGig);
            var slotList = db.Slots.Where(x => x.Gig.GigId == gigId).ToList();
            CreateSlotOpeningsNotification(slotList, thisGig); 
            return RedirectToAction("Venueview", "Venues", new { venueId =  thisGig.Venue.VenueId});
        }
        public void CreateGigNotifications(List<Listener> connectedListeners, Gig thisGig)
        {
            foreach (Listener thisListener in connectedListeners)
            {
                ListenerNotification listenerNotification = new ListenerNotification
                {
                    listener = thisListener,
                    gig = thisGig,
                    type = "gigNotification",
                    message = (thisGig.Venue.Name + " created a new gig on " + thisGig.Date.ToShortDateString() + " at " + thisGig.Time.ToShortTimeString()),
                    read = false
                };
                Sms.SendMessage(db.Users.Find(thisListener.UserId).PhoneNumber, listenerNotification.message);
                db.ListenerNotifications.Add(listenerNotification);
                db.SaveChanges();
            }
        }
        public void CreateSlotOpeningsNotification(List<Slot>slotList, Gig thisGig)
        {
            foreach (Slot thisSlot in slotList)
            {
                var artistsList = db.Artists.Where(x => x.Genre1 == thisSlot.Genre || x.Genre2 == thisSlot.Genre || x.Genre3 == thisSlot.Genre).Where(x => x.Type == thisSlot.ArtistType).ToList();
                foreach (Artist thisArtist in artistsList)
                {
                    ArtistNotification artistNotification = new ArtistNotification
                    {
                        artist = thisArtist,
                        slot = thisSlot,
                        type = "Slot Opening",
                        message = (thisSlot.Order+" slot available for " + thisGig.Name + " at " + thisGig.Venue.Name + " on " + thisGig.Date.ToShortDateString() + " at " + thisGig.Time.ToShortTimeString()),
                        read = false
                    };
                    Sms.SendMessage(db.Users.Find(thisArtist.UserId).PhoneNumber, artistNotification.message);
                    db.ArtistNotifications.Add(artistNotification);
                    db.SaveChanges();
                }
            }
        }
        public List<Gig> checkGig(List<Gig> giglist)
        {
            List<Gig> newList = new List<Gig>();
            foreach(Gig gig in giglist)
            {
                if (gig.Venue.Name == null)
                {
                    gig.Venue = db.Venues.Find(gig.Venue.VenueId);
                    db.SaveChanges();
                    
                    
                }
                newList.Add(gig);
            }
            return newList;
        }

    }
}
