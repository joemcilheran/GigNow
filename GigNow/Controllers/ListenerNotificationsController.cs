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
    public class ListenerNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ListenerNotifications
        public ActionResult Index()
        {
            return View(db.ListenerNotifications.ToList());
        }
        public ActionResult Inbox()
        {
            var userId = User.Identity.GetUserId();
            var listenerId = db.Listeners.FirstOrDefault(x => x.UserId == userId).ListenerID;
            CheckGigs(listenerId);
            var inbox = db.ListenerNotifications.Where(x => x.listener.ListenerID == listenerId && x.read == false).ToList();
            return View(inbox);
        }
        public void CheckGigs(int? listenerId)
        {
            var gigs = (from gigRelationship in db.GigRelationships where gigRelationship.Listener.ListenerID == listenerId select gigRelationship.Gig).ToList();
            foreach (Gig thisGig in gigs)
            {
                var gigRemindersList = db.ListenerNotifications.Where(x => x.type == "Gig Reminder" && x.gig.GigId == thisGig.GigId && x.listener.ListenerID == listenerId).ToList();
                if (gigRemindersList.Count() == 0)
                {
                    if (thisGig.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                    {
                        ListenerNotification listenerNotification = new ListenerNotification
                        {
                            listener = db.Listeners.Find(listenerId),
                            gig = thisGig,
                            type = "Gig Reminder",
                            read = false,
                            message = (thisGig.Name + " is Today!")
                        };
                        db.ListenerNotifications.Add(listenerNotification);
                        db.SaveChanges();
                    }
                }

            }
        }

        // GET: ListenerNotifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListenerNotification listenerNotification = db.ListenerNotifications.Find(id);
            if (listenerNotification == null)
            {
                return HttpNotFound();
            }
            return View(listenerNotification);
        }

        // GET: ListenerNotifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListenerNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListenerNotificationId")] ListenerNotification listenerNotification)
        {
            if (ModelState.IsValid)
            {
                db.ListenerNotifications.Add(listenerNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(listenerNotification);
        }

        // GET: ListenerNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListenerNotification listenerNotification = db.ListenerNotifications.Find(id);
            if (listenerNotification == null)
            {
                return HttpNotFound();
            }
            return View(listenerNotification);
        }

        // POST: ListenerNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListenerNotificationId")] ListenerNotification listenerNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listenerNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(listenerNotification);
        }

        // GET: ListenerNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListenerNotification listenerNotification = db.ListenerNotifications.Find(id);
            if (listenerNotification == null)
            {
                return HttpNotFound();
            }
            return View(listenerNotification);
        }

        // POST: ListenerNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListenerNotification listenerNotification = db.ListenerNotifications.Find(id);
            db.ListenerNotifications.Remove(listenerNotification);
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
        public ActionResult MarkAsRead(int? listenerNotificationId)
        {
            var listenerNotification = db.ListenerNotifications.Find(listenerNotificationId);
            listenerNotification.read = true;
            db.SaveChanges();
            return RedirectToAction("GigView", "Gigs", new { gigId = listenerNotification.gig.GigId, partial = "false" });
        }


    }
}
