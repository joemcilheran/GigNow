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
        public ActionResult Create(ArtistViewModelVM artistViewModelVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                db.ArtistViewModelVMs.Add(artistViewModelVM);
                artistViewModelVM.artist.AddressId = artistViewModelVM.address.AddressId;
                artistViewModelVM.artist.UserId = userId;
                artistViewModelVM.address.ZipCodeId = artistViewModelVM.zipcode.ZipcodeId;
                artistViewModelVM.zipcode.CityId = artistViewModelVM.city.CityId;
                artistViewModelVM.city.StateId = artistViewModelVM.state.StateId;
                db.SaveChanges();
                return RedirectToAction("AddFiles","Artists", new {ArtistId = artistViewModelVM.artist.ArtistId });
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
    }
}
