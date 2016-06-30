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
    public class VenueViewModelVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueViewModelVMs
        public ActionResult Index()
        {
            return View(db.VenueViewModelVMs.ToList());
        }

        // GET: VenueViewModelVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueViewModelVM venueViewModelVM = db.VenueViewModelVMs.Find(id);
            if (venueViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(venueViewModelVM);
        }

        // GET: VenueViewModelVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueViewModelVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VenueViewModelVM venueViewModelVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                db.VenueViewModelVMs.Add(venueViewModelVM);
                db.SaveChanges();
                venueViewModelVM.venue.AddressId = venueViewModelVM.address.AddressId;
                venueViewModelVM.venue.UserId = userId;
                venueViewModelVM.address.ZipCodeId = venueViewModelVM.zipcode.ZipcodeId;
                venueViewModelVM.zipcode.CityId = venueViewModelVM.city.CityId;
                venueViewModelVM.city.StateId = venueViewModelVM.state.StateId; 
                db.SaveChanges();              
                return RedirectToAction("AddFiles", "Venues", new {venueId = venueViewModelVM.venue.VenueId });
            }

            return View(venueViewModelVM);
        }

        // GET: VenueViewModelVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueViewModelVM venueViewModelVM = db.VenueViewModelVMs.Find(id);
            if (venueViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(venueViewModelVM);
        }

        // POST: VenueViewModelVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] VenueViewModelVM venueViewModelVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueViewModelVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venueViewModelVM);
        }

        // GET: VenueViewModelVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueViewModelVM venueViewModelVM = db.VenueViewModelVMs.Find(id);
            if (venueViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(venueViewModelVM);
        }

        // POST: VenueViewModelVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueViewModelVM venueViewModelVM = db.VenueViewModelVMs.Find(id);
            db.VenueViewModelVMs.Remove(venueViewModelVM);
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
    }
}
