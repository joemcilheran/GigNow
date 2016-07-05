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
                return RedirectToAction("VenueView", new {venueId = venueViewModelVM.venue.VenueId});
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
        public ActionResult VenueView()
        {
            var userId = User.Identity.GetUserId();
            var Venue = db.Venues.FirstOrDefault(x => x.UserId == userId);
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
