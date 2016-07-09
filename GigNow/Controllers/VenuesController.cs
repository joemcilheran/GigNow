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
        // GET: Venues
        public ActionResult Index()
        {
            var venues = db.Venues.Include(v => v.address).Include(v => v.AppUser);
            return View(venues.ToList());
        }

        // GET: Venues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueViewModelVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new Photo
                    {
                        Name = System.IO.Path.GetFileName(upload.FileName),
                        Venue = venueViewModelVM.venue
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        photo.Data = reader.ReadBytes(upload.ContentLength);
                    }
                    db.Photos.Add(photo);
                    venueViewModelVM.photo = photo;

                }
                db.SaveChanges();
                return RedirectToAction("VenueView", new { venueId = venueViewModelVM.venue.VenueId });
            }

            return View(venueViewModelVM);
        }

        // GET: Venues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", venue.address.AddressId);
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VenueId,Name,AddressId,Capacity,StageSize,ContactName,DefaultArtistType,DefaultCompensation,DefaultGenre,DefaultPerks,FBLink,SiteLink,TwitterLink,ExtraLink,ExtraLink2,SoundSystem,LoadInInstructions,rating,UserId")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", venue.address.AddressId);
            return View(venue);
        }

        // GET: Venues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venue venue = db.Venues.Find(id);
            db.Venues.Remove(venue);
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
        public ActionResult AddFiles(int VenueId)
        {
            return View();
        }
        public ActionResult VenueView(int? VenueId)
        {
            var Venue = db.Venues.Find(VenueId);
            var googleVAddress = Venue.address.StreetAddress.Replace(' ', '+');
            var googleVCity = Venue.address.zipcode.city.Name.Replace(' ', '+');
            var Destination = (googleVAddress + "+" + Venue.address.Apt + ",+" + googleVCity + ",+" + Venue.address.zipcode.city.state.Name + "+" + Venue.address.zipcode.ZipCode);
            ViewBag.Map = ("https://www.google.com/maps/embed/v1/place?key=AIzaSyAqBwMAtQFkFgENx8Fn_0Stj3UOgIWysDc&q=" + Destination);
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
                    var googleAddress = artist.address.StreetAddress.Replace(' ', '+');
                    var googleCity = artist.address.zipcode.city.Name.Replace(' ', '+');
                    var Origin = (googleAddress + "+" + artist.address.Apt + ",+" + googleCity + ",+" + artist.address.zipcode.city.state.Name + "+" + artist.address.zipcode.ZipCode);
                    ViewBag.Route = ("https://www.google.com/maps/embed/v1/directions?key=AIzaSyAqBwMAtQFkFgENx8Fn_0Stj3UOgIWysDc&origin=" + Origin + "&destination=" + Destination);
                }
                else if (role == "Listener")
                {
                    ViewBag.User = "Listener";
                    var listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
                    var googleAddress = listener.address.StreetAddress.Replace(' ', '+');
                    var googleCity = listener.address.zipcode.city.Name.Replace(' ', '+');
                    var Origin = (googleAddress + "+" + listener.address.Apt + ",+" + googleCity + ",+" + listener.address.zipcode.city.state.Name + "+" + listener.address.zipcode.ZipCode);
                    ViewBag.Route = ("https://www.google.com/maps/embed/v1/directions?key=AIzaSyAqBwMAtQFkFgENx8Fn_0Stj3UOgIWysDc&origin=" + Origin + "&destination=" + Destination);
                    var relationshipList = db.VenueRelationships.Where(x => x.Listener == listener).ToList();
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
