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
using Microsoft.AspNet.Identity.Owin;
using Twilio;
using System.Threading.Tasks;


namespace GigNow.Controllers
{
    public class VenuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
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



        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VenueViewModelVM venueViewModelVM, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                db.Venues.Add(venueViewModelVM.venue);
                db.Addresses.Add(venueViewModelVM.address);
                db.Zipcodes.Add(venueViewModelVM.zipcode);
                db.Cities.Add(venueViewModelVM.city);
                db.States.Add(venueViewModelVM.state);
                db.SaveChanges();
                venueViewModelVM.venue.address = venueViewModelVM.address;
                venueViewModelVM.venue.UserId = userId;
                venueViewModelVM.address.zipcode = venueViewModelVM.zipcode;
                venueViewModelVM.zipcode.city = venueViewModelVM.city;
                venueViewModelVM.city.state = venueViewModelVM.state;
                PhotosController PC = new PhotosController();
                PC.CreateVenuePhoto(upload, venueViewModelVM.venue);
                db.SaveChanges();
                return RedirectToAction("VenueView", new { venueId = venueViewModelVM.venue.VenueId });
            }

            return View(venueViewModelVM);
        }


        public ActionResult Edit(int? venueId)
        {
            var Venue = db.Venues.Find(venueId);
            var Address = db.Addresses.Find(Venue.address.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.zipcode.ZipcodeId);
            var City = db.Cities.Find(Zipcode.city.CityId);
            var State = db.States.Find(City.state.StateId);
            VenueViewModelVM VVM = new VenueViewModelVM
            {
                gigList = generateGigList(Venue.VenueId),
                photo = db.Photos.FirstOrDefault(x => x.Venue.VenueId == Venue.VenueId),
                venue = Venue,
                address = Address,
                zipcode = Zipcode,
                city = City,
                state = State,
            };
            return View(VVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VenueViewModelVM venueViewModelVM, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                
                db.Venues.Add(venueViewModelVM.venue);
                db.Addresses.Add(venueViewModelVM.address);
                db.Zipcodes.Add(venueViewModelVM.zipcode);
                db.Cities.Add(venueViewModelVM.city);
                db.States.Add(venueViewModelVM.state);
                db.SaveChanges();
                venueViewModelVM.venue.address = venueViewModelVM.address;
                venueViewModelVM.address.zipcode = venueViewModelVM.zipcode;
                venueViewModelVM.zipcode.city = venueViewModelVM.city;
                venueViewModelVM.city.state = venueViewModelVM.state;
                PhotosController PC = new PhotosController();
                PC.CreateVenuePhoto(upload, venueViewModelVM.venue);
                db.SaveChanges();
                return RedirectToAction("VenueView", new { venueId = venueViewModelVM.venue.VenueId });
            }
            return View(venueViewModelVM);
        }




        public ActionResult VenueView(int? VenueId)
        {
            var Venue = db.Venues.Find(VenueId);
            MapsController MC = new MapsController();
            var Destination = MC.generateDestination(Venue);
            ViewBag.Map = MC.generateMap(Destination);
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var s = UserManager.GetRoles(userId);
                string role = s[0].ToString();
                if (Venue.UserId == userId)
                {
                    ViewBag.User = "Venue Admin";
                }
                else if (role == "Artist Manager")
                {
                    ViewBag.User = "Artist";
                    var artist = db.Artists.FirstOrDefault(x => x.UserId == userId);
                    string Origin = MC.generateOrigin(artist);
                    ViewBag.Route = MC.generateRoute(Origin, Destination);
                }
                else if (role == "Listener")
                {
                    ViewBag.User = "Listener";
                    var listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
                    string Origin = MC.generateOrigin(listener);
                    ViewBag.Route = MC.generateRoute(Origin, Destination);
                    var relationshipList = db.VenueRelationships.Where(x => x.Listener.ListenerID == listener.ListenerID && x.Venue.VenueId == VenueId).ToList();
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

            var Address = db.Addresses.Find(Venue.address.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.zipcode.ZipcodeId);
            var City = db.Cities.Find(Zipcode.city.CityId);
            var State = db.States.Find(City.state.StateId);
            VenueViewModelVM VVM = new VenueViewModelVM
            {
                gigList = generateGigList(Venue.VenueId),
                photo = db.Photos.FirstOrDefault(x => x.Venue.VenueId == Venue.VenueId),
                venue = Venue,
                address = Address,
                zipcode = Zipcode,
                city = City,
                state = State,
            };
            return View(VVM);
        }
        public ActionResult ShowWatchList(List<Venue> watchList)
        {
            return View(watchList);
        }
        public List<Gig> generateGigList(int? venueId)
        {
            var venue = db.Venues.Find(venueId);
            var gigList = db.Gigs.Where(x => x.Venue.VenueId == venue.VenueId && x.Date >= DateTime.Today).ToList();
            return gigList;
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View(new List<Venue>());
        }
        [HttpPost]
        public ActionResult Search(string city)
        {
            List<Venue> venueSearchResultList = new List<Venue>();
            if (!string.IsNullOrWhiteSpace(city))
            {
                venueSearchResultList = db.Venues.Where(x => x.address.zipcode.city.Name == city).OrderByDescending(x => x.rating).ToList();
            }
            return View(venueSearchResultList);
        }

    }
}
