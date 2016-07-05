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
    public class VenuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
                venueViewModelVM.venue.AddressId = venueViewModelVM.address.AddressId;
                venueViewModelVM.venue.UserId = userId;
                venueViewModelVM.address.ZipCodeId = venueViewModelVM.zipcode.ZipcodeId;
                venueViewModelVM.zipcode.CityId = venueViewModelVM.city.CityId;
                venueViewModelVM.city.StateId = venueViewModelVM.state.StateId;
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new Photo
                    {
                        Name = System.IO.Path.GetFileName(upload.FileName),
                        VenueId = venueViewModelVM.venue.VenueId
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
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", venue.AddressId);
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
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", venue.AddressId);
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
        public ActionResult VenueView(int VenueId)
        {
            var userId = User.Identity.GetUserId();
            var Venue = db.Venues.Find(VenueId);
            if(Venue.UserId == userId)
            {
                ViewBag.User = "Venue Admin";
            }
            var Address = db.Addresses.Find(Venue.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.ZipCodeId);
            var City = db.Cities.Find(Zipcode.CityId);
            var State = db.States.Find(City.StateId);
            VenueViewModelVM VVM = new VenueViewModelVM
            {
                Id = 1,
                photo = db.Photos.FirstOrDefault(x => x.VenueId == Venue.VenueId),
                venue = Venue,
                address = Address,
                zipcode = Zipcode,
                city = City,
                state = State,
            };
            return View(VVM);
        }
    }
}
