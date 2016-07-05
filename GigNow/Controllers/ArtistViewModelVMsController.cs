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
    public class ArtistViewModelVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArtistViewModelVMs
        public ActionResult Index()
        {
            return View(db.ArtistViewModelVMs.ToList());
        }

        // GET: ArtistViewModelVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistViewModelVM artistViewModelVM = db.ArtistViewModelVMs.Find(id);
            if (artistViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(artistViewModelVM);
        }

        // GET: ArtistViewModelVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistViewModelVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtistViewModelVM artistViewModelVM, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                db.Artists.Add(artistViewModelVM.artist);
                db.Addresses.Add(artistViewModelVM.address);
                db.Zipcodes.Add(artistViewModelVM.zipcode);
                db.Cities.Add(artistViewModelVM.city);
                db.States.Add(artistViewModelVM.state);
                db.SaveChanges();
                artistViewModelVM.artist.AddressId = artistViewModelVM.address.AddressId;
                artistViewModelVM.artist.UserId = userId;
                artistViewModelVM.address.ZipCodeId = artistViewModelVM.zipcode.ZipcodeId;
                artistViewModelVM.zipcode.CityId = artistViewModelVM.city.CityId;
                artistViewModelVM.city.StateId = artistViewModelVM.state.StateId;
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new Photo
                    {
                        Name = System.IO.Path.GetFileName(upload.FileName),
                        ArtistId = artistViewModelVM.artist.ArtistId
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        photo.Data = reader.ReadBytes(upload.ContentLength);
                    }
                    db.Photos.Add(photo);
                    artistViewModelVM.photo = photo;

                }
                db.SaveChanges();
                return RedirectToAction("AddAudioandVideo","Artists", new {artist = artistViewModelVM.artist});
            }
            return View(artistViewModelVM);
        }

        // GET: ArtistViewModelVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistViewModelVM artistViewModelVM = db.ArtistViewModelVMs.Find(id);
            if (artistViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(artistViewModelVM);
        }

        // POST: ArtistViewModelVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] ArtistViewModelVM artistViewModelVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistViewModelVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artistViewModelVM);
        }

        // GET: ArtistViewModelVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistViewModelVM artistViewModelVM = db.ArtistViewModelVMs.Find(id);
            if (artistViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(artistViewModelVM);
        }

        // POST: ArtistViewModelVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistViewModelVM artistViewModelVM = db.ArtistViewModelVMs.Find(id);
            db.ArtistViewModelVMs.Remove(artistViewModelVM);
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
        public ActionResult ArtistView()
        {
            var userId = User.Identity.GetUserId();
            var Artist = db.Artists.FirstOrDefault(x => x.UserId == userId);
            var Address = db.Addresses.Find(Artist.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.ZipCodeId);
            var City = db.Cities.Find(Zipcode.CityId);
            var State = db.States.Find(City.StateId);
            ArtistViewModelVM AVM = new ArtistViewModelVM
            {
                Id = 1,
                photo = db.Photos.FirstOrDefault(x => x.VenueId == Artist.ArtistId),
                artist = Artist,
                address = Address,
                zipcode = Zipcode,
                city = City,
                state = State,
            };
            return View(AVM);
        }
    }
}
