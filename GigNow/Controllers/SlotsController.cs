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
    public class SlotsController : Controller
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
        // GET: Slots
        public ActionResult Index(List<Slot> bill)
        {
            var Gig = bill[0].Gig;
            var Venue = Gig.Venue;
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
                    ViewBag.User = "Artist";
                }
                else
                {
                    ViewBag.User = "Listener";
                }
            }

            else
            {
                ViewBag.User = "Visiter";
            }
            return View(bill);
        }

        // GET: Slots/Details/5
        public ActionResult Details(int? slotId)
        {
            if (slotId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slot slot = db.Slots.Find(slotId);
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
                Gig = Gig,
                Compensation = Gig.DefaultCompensation,
                Perks = Gig.DefaultPerks,
                Genre = Gig.DefaultGenre,
                ArtistType = Gig.DefaultArtistType
            };
            return View(slot);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slot slot)
        {
            if (ModelState.IsValid)
            {
                slot.Gig = db.Gigs.Find(slot.Gig.GigId);
                db.Slots.Add(slot);
                db.SaveChanges();
                return RedirectToAction("GigView","Gigs", new {gigId = slot.Gig.GigId, partial = "false" });
            }

            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", slot.Gig.GigId);
            return View(slot);
        }


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
            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", slot.Gig.GigId);
            return View(slot);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotId,Compensation,Genre,IsFilled,ArtistType,Perks,Order,Length,UseGigDefaults,GigId")] Slot slot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GigView", "Gigs", new { gigId = slot.Gig.GigId, partial = "false" });
            }
            ViewBag.GigId = new SelectList(db.Gigs, "GigId", "DefaultGenre", slot.Gig.GigId);
            return View(slot);
        }


        public ActionResult Delete(int? id)
        {

            Slot slot = db.Slots.Find(id);
            db.Slots.Remove(slot);
            db.SaveChanges();
            return RedirectToAction("GigView", "Gigs", new { gigId = slot.Gig.GigId, partial = "false" });
        }




    }
}
