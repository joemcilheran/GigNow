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
    public class ListenersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Listeners
        public ActionResult Index()
        {
            var listeners = db.Listeners.Include(l => l.address).Include(l => l.AppUser);
            return View(listeners.ToList());
        }

        // GET: Listeners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listener listener = db.Listeners.Find(id);
            if (listener == null)
            {
                return HttpNotFound();
            }
            return View(listener);
        }

        // GET: Listeners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListenerViewModelVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListenerViewModelVM listenerViewModelVM)
        {
            if (ModelState.IsValid)
            {
                db.Listeners.Add(listenerViewModelVM.listener);
                db.Addresses.Add(listenerViewModelVM.address);
                db.Zipcodes.Add(listenerViewModelVM.zipcode);
                db.Cities.Add(listenerViewModelVM.city);
                db.States.Add(listenerViewModelVM.state);
                db.SaveChanges();
                var userId = User.Identity.GetUserId();
                listenerViewModelVM.listener.AddressId = listenerViewModelVM.address.AddressId;
                listenerViewModelVM.listener.UserId = userId;
                listenerViewModelVM.address.ZipCodeId = listenerViewModelVM.zipcode.ZipcodeId;
                listenerViewModelVM.zipcode.CityId = listenerViewModelVM.city.CityId;
                listenerViewModelVM.city.StateId = listenerViewModelVM.state.StateId;
                db.SaveChanges();
                return RedirectToAction("ListenerView", new {ListerId = listenerViewModelVM.listener.ListenerID});
            }

            return View(listenerViewModelVM);
        }

        // GET: Listeners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listener listener = db.Listeners.Find(id);
            if (listener == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", listener.AddressId);
            
            return View(listener);
        }

        // POST: Listeners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListenerID,AddressId,UserId,Genre1,Genre2,Genre3")] Listener listener)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listener).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", listener.AddressId);
            
            return View(listener);
        }

        // GET: Listeners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listener listener = db.Listeners.Find(id);
            if (listener == null)
            {
                return HttpNotFound();
            }
            return View(listener);
        }

        // POST: Listeners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Listener listener = db.Listeners.Find(id);
            db.Listeners.Remove(listener);
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
        public ActionResult ListenerView(int? listenerId)
        {
            var userId = User.Identity.GetUserId();
            var Listener = db.Listeners.Find(listenerId);
            if (userId == Listener.UserId)
            {
                ViewBag.User = "Listener";
            }
            else
            {
                ViewBag.User = "Other";
            }
            var Address = db.Addresses.Find(Listener.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.ZipCodeId);
            var City = db.Cities.Find(Zipcode.CityId);
            var State = db.States.Find(City.StateId);
            ListenerViewModelVM LVM = new ListenerViewModelVM
            {
                GigWatchList = generateGigWatchList(Listener.ListenerID),
                ArtistWatchList = generateArtistWatchList(Listener.ListenerID),
                VenueWatchList = generateVenueWatchList(Listener.ListenerID),
                listener = Listener,
                address = Address,
                zipcode = Zipcode,
                city = City,
                state = State,
            };
            return View(LVM);
        }
        public List<Gig> generateGigWatchList(int? listenerId)
        {
            var gigIds = from gigRelationship in db.GigRelationships where gigRelationship.ListenerId == listenerId select gigRelationship.GigId;
            List<Gig> gigWatchList = new List<Gig>();
            gigWatchList = db.Gigs.Where(x => gigIds.ToList().Contains(x.GigId)).ToList();
            return gigWatchList;
        }
        public List<Venue> generateVenueWatchList(int? listenerId)
        {
            var venueIds = from venueRelationship in db.VenueRelationships where venueRelationship.ListenerId == listenerId select venueRelationship.VenueId;
            List<Venue> venueWatchList = new List<Venue>();
            venueWatchList = db.Venues.Where(x => venueIds.ToList().Contains(x.VenueId)).ToList();
            return venueWatchList;
        }
        public List<Artist> generateArtistWatchList(int? listenerId)
        {
            var artistIds = from artistRelationship in db.ArtistRelationships where artistRelationship.ListenerId == listenerId select artistRelationship.ArtistId;
            List<Artist> artistWatchList = new List<Artist>();
            artistWatchList = db.Artists.Where(x => artistIds.ToList().Contains(x.ArtistId)).ToList();
            return artistWatchList;
        }
    }
}
