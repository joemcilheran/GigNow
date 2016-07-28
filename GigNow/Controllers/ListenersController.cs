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


        // GET: Listeners/Create
        public ActionResult Create()
        {
            return View();
        }


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
                listenerViewModelVM.listener.address = listenerViewModelVM.address;
                listenerViewModelVM.listener.UserId = userId;
                listenerViewModelVM.address.zipcode = listenerViewModelVM.zipcode;
                listenerViewModelVM.zipcode.city = listenerViewModelVM.city;
                listenerViewModelVM.city.state = listenerViewModelVM.state;
                db.SaveChanges();
                return RedirectToAction("ListenerView", new {ListenerId = listenerViewModelVM.listener.ListenerID});
            }

            return View(listenerViewModelVM);
        }


        public ActionResult Edit(int? listenerId)
        {
            var Listener = db.Listeners.Find(listenerId);
            var Address = db.Addresses.Find(Listener.address.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.zipcode.ZipcodeId);
            var City = db.Cities.Find(Zipcode.city.CityId);
            var State = db.States.Find(City.state.StateId);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ListenerViewModelVM listenerViewModelVM)
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
                listenerViewModelVM.listener.address = listenerViewModelVM.address;
                listenerViewModelVM.listener.UserId = userId;
                listenerViewModelVM.address.zipcode = listenerViewModelVM.zipcode;
                listenerViewModelVM.zipcode.city = listenerViewModelVM.city;
                listenerViewModelVM.city.state = listenerViewModelVM.state;
                db.SaveChanges();
                return RedirectToAction("ListenerView", new { ListenerId = listenerViewModelVM.listener.ListenerID });
            }
           
            return View(listenerViewModelVM);
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
            var Address = db.Addresses.Find(Listener.address.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.zipcode.ZipcodeId);
            var City = db.Cities.Find(Zipcode.city.CityId);
            var State = db.States.Find(City.state.StateId);
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
            var gigIds = from gigRelationship in db.GigRelationships where gigRelationship.Listener.ListenerID == listenerId select gigRelationship.Gig.GigId;
            List<Gig> gigWatchList = new List<Gig>();
            
            foreach(Gig thisGig in db.Gigs.Where(x => gigIds.ToList().Contains(x.GigId)).ToList())
            {
                if (thisGig.Date >= DateTime.Today)
                {
                    gigWatchList.Add(thisGig);
                }
            }
            return gigWatchList;
        }
        public List<Venue> generateVenueWatchList(int? listenerId)
        {
            var venueIds = from venueRelationship in db.VenueRelationships where venueRelationship.Listener.ListenerID == listenerId select venueRelationship.Venue.VenueId;
            List<Venue> venueWatchList = new List<Venue>();
            venueWatchList = db.Venues.Where(x => venueIds.ToList().Contains(x.VenueId)).ToList();
            return venueWatchList;
        }
        public List<Artist> generateArtistWatchList(int? listenerId)
        {
            var artistIds = from artistRelationship in db.ArtistRelationships where artistRelationship.Listener.ListenerID == listenerId select artistRelationship.Artist.ArtistId;
            List<Artist> artistWatchList = new List<Artist>();
            artistWatchList = db.Artists.Where(x => artistIds.ToList().Contains(x.ArtistId)).ToList();
            return artistWatchList;
        }
    }
}
