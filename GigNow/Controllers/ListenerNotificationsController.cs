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
                        SmsController Sms = new SmsController();
                        Sms.SendMessage(db.Users.Find(listenerNotification.listener.UserId).PhoneNumber, listenerNotification.message);
                        db.ListenerNotifications.Add(listenerNotification);
                        db.SaveChanges();
                    }
                }

            }
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
