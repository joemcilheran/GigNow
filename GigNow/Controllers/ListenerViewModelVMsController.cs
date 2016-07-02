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
    public class ListenerViewModelVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ListenerViewModelVMs
        public ActionResult Index()
        {
            return View(db.ListenerViewModelVMs.ToList());
        }

        // GET: ListenerViewModelVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListenerViewModelVM listenerViewModelVM = db.ListenerViewModelVMs.Find(id);
            if (listenerViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(listenerViewModelVM);
        }

        // GET: ListenerViewModelVMs/Create
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
                db.ListenerViewModelVMs.Add(listenerViewModelVM);
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

        // GET: ListenerViewModelVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListenerViewModelVM listenerViewModelVM = db.ListenerViewModelVMs.Find(id);
            if (listenerViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(listenerViewModelVM);
        }

        // POST: ListenerViewModelVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListenerViewModel")] ListenerViewModelVM listenerViewModelVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listenerViewModelVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(listenerViewModelVM);
        }

        // GET: ListenerViewModelVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListenerViewModelVM listenerViewModelVM = db.ListenerViewModelVMs.Find(id);
            if (listenerViewModelVM == null)
            {
                return HttpNotFound();
            }
            return View(listenerViewModelVM);
        }

        // POST: ListenerViewModelVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListenerViewModelVM listenerViewModelVM = db.ListenerViewModelVMs.Find(id);
            db.ListenerViewModelVMs.Remove(listenerViewModelVM);
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
