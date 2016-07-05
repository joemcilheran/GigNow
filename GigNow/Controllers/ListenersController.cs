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
    public class ListenersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Listeners
        public ActionResult Index()
        {
            var listeners = db.Listeners.Include(l => l.address).Include(l => l.AppUser);
            return View(listeners.ToList());
        }

        // GET: Listeners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listener listener = db.Listeners.Find(id);
            if (listener == null)
            {
                return HttpNotFound();
            }
            return View(listener);
        }

        // GET: Listeners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListenerViewModelVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListenerViewModelVM listenerViewModelVM)
        {
            if (ModelState.IsValid)
            {
                db.Listeners.Add(listenerViewModelVM.listener);
                db.Addresses.Add(listenerViewModelVM.address);
                db.Zipcodes.Add(listenerViewModelVM.zipcode);
                db.Cities.Add(listenerViewModelVM.city);
                db.States.Add(listenerViewModelVM.state);
                db.SaveChanges();
                var userId = User.Identity.GetUserId();
                listenerViewModelVM.listener.AddressId = listenerViewModelVM.address.AddressId;
                listenerViewModelVM.listener.UserId = userId;
                listenerViewModelVM.address.ZipCodeId = listenerViewModelVM.zipcode.ZipcodeId;
                listenerViewModelVM.zipcode.CityId = listenerViewModelVM.city.CityId;
                listenerViewModelVM.city.StateId = listenerViewModelVM.state.StateId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(listenerViewModelVM);
        }

        // GET: Listeners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listener listener = db.Listeners.Find(id);
            if (listener == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", listener.AddressId);
            
            return View(listener);
        }

        // POST: Listeners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListenerID,AddressId,UserId,Genre1,Genre2,Genre3")] Listener listener)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listener).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", listener.AddressId);
            
            return View(listener);
        }

        // GET: Listeners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listener listener = db.Listeners.Find(id);
            if (listener == null)
            {
                return HttpNotFound();
            }
            return View(listener);
        }

        // POST: Listeners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Listener listener = db.Listeners.Find(id);
            db.Listeners.Remove(listener);
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
