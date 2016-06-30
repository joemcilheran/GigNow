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
    public class ArtistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Artists
        public ActionResult Index()
        {
            var artists = db.Artists.Include(a => a.address).Include(a => a.AppUser);
            return View(artists.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }
        public ActionResult CreateArtist()
        { 
            return View();
        }
        [HttpPost]
        //public ActionResult CreateArtist(string Name, string ContactName, string Genre1, string Genre2, string Genre3, string Type, int? NumberofMembers, string StreetAddress, string Apt, int? Zip, string City, string State)
        //{
        //    //var userId = User.Identity.GetUserId();
        //    //Address address = new Address {Apt = Apt, StreetAddress = StreetAddress};
        //    //db.Addresses.Add(address);
        //    //db.SaveChanges();
        //    //Zipcode zipcode = new Zipcode {ZipCode = Zip, AddressId = address.AddressId };
        //    //db.Zipcodes.Add(zipcode);
        //    //db.SaveChanges();
        //    //City city = new City { Name = City, ZipCodeId = zipcode.ZipcodeId };
        //    //db.Cities.Add(city);
        //    //db.SaveChanges();
        //    //State state = new State { Name = State, CityId = city.CityId };
        //    //db.States.Add(state);
        //    //db.SaveChanges();
        //    //Artist artist = new Artist { Name = Name, ContactName = ContactName, Genre1 = Genre1, Genre2 = Genre2, Genre3 = Genre3, Type = Type, NumberOfMembers = NumberofMembers, UserId = userId, AddressId = address.AddressId };
        //    //db.Artists.Add(artist);
        //    //db.SaveChanges();
        //    return RedirectToAction("AddFiles",new { Artistid = artist.ArtistId });

        //}

        // GET: Artists/Create
        public ActionResult Create()
        {
           
            
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistId,Name,StreetAddress,City,ZipCode,State,ContactName,Genre1,Genre2,Genre3,Type,NumberOfMembers")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.AddressId);
           
            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.AddressId);
           
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistId,Name,AddressId,ContactName,Genre1,Genre2,Genre3,Type,NumberOfMembers,UserId")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.AddressId);
           
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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
        public ActionResult AddFiles(int Artistid)
        {
            return View();
        }
    }
}
