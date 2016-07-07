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
    public class GigsController : Controller
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

        // GET: Gigs
        public ActionResult Index()
        {
            var gigs = db.Gigs.Include(g => g.Venue);
            return View(gigs.ToList());
        }

        // GET: Gigs/Details/5
        public ActionResult Details(int? id)
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

        // POST: Gigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gig gig)
        {
            if (ModelState.IsValid)
            {
                db.Gigs.Add(gig);
                db.SaveChanges();
                return RedirectToAction("GigView", new {gigId = gig.GigId });
            }

            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "Name", gig.Venue.VenueId);
            return View(gig);
        }

        // GET: Gigs/Edit/5
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

        // POST: Gigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Gigs/Delete/5
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

        // POST: Gigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gig gig = db.Gigs.Find(id);
            db.Gigs.Remove(gig);
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
        public ActionResult GigView(int? gigId)
        {
            
            var userId = User.Identity.GetUserId();
            var s = UserManager.GetRoles(userId);
            string role = s[0].ToString();
            var Gig = db.Gigs.Find(gigId);
            var Venue = db.Venues.FirstOrDefault(x => x.VenueId == Gig.Venue.VenueId);
            if(userId == Venue.UserId)
            {
                ViewBag.User = "Gig Admin";
            }
            else if(role == "Artist Manager")
            {
                ViewBag.User = "Artist";
            }
            else if(role == "Listener")
            {
                ViewBag.User = "Listener";
                var listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
                var relationshipList = db.GigRelationships.Where(x => x.Listener == listener).ToList();
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
            GigViewModelVM GVM = new GigViewModelVM
             {
                gig = Gig,
                venue = Venue,
                bill = db.Slots.Where(x => x.Gig == Gig).ToList()
            };
            return View(GVM);
        }
        //public ActionResult ShowWatchList(List<Gig> watchList)
        //{
        //    return View(watchList);
        //}
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
                var genreGigs = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig);               
                gigSearchResultList = genreGigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity && x.Date.ToShortDateString() == date.GetValueOrDefault().ToShortDateString()).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(gigGenre) && string.IsNullOrWhiteSpace(gigCity) && !date.HasValue)
            {
                gigSearchResultList = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig).ToList();
            }
            else if (string.IsNullOrWhiteSpace(gigGenre) && !string.IsNullOrWhiteSpace(gigCity) && !date.HasValue)
            {
                gigSearchResultList = db.Gigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity).ToList();
            }
            else if (string.IsNullOrWhiteSpace(gigGenre) && string.IsNullOrWhiteSpace(gigCity) && date.HasValue)
            {
                gigSearchResultList = db.Gigs.Where(x => x.Date.ToShortDateString() == date.GetValueOrDefault().ToShortDateString()).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(gigGenre) && !string.IsNullOrWhiteSpace(gigCity) && !date.HasValue)
            {
                var genreGigs = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig);
                gigSearchResultList = genreGigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity).ToList();
            }
            else if (string.IsNullOrWhiteSpace(gigGenre) && !string.IsNullOrWhiteSpace(gigCity) && date.HasValue)
            {
                gigSearchResultList = db.Gigs.Where(x => x.Venue.address.zipcode.city.Name == gigCity && x.Date.ToShortDateString() == date.GetValueOrDefault().ToShortDateString()).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(gigGenre) && string.IsNullOrWhiteSpace(gigCity) && date.HasValue)
            {
                var genreGigs = (from slot in db.Slots where slot.Genre == gigGenre select slot.Gig);
                gigSearchResultList = genreGigs.Where(x => x.Date.ToShortDateString() == date.GetValueOrDefault().ToShortDateString()).ToList();
            }
            return View(gigSearchResultList);
        }
    }
}
