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
    public class VenueRelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Create(int? venueId)
        {
            var userId = User.Identity.GetUserId();
            var venue = db.Venues.Find(venueId);
            var Listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
            VenueRelationship venuerelationship = new VenueRelationship
            {
                Listener = Listener,
                Venue = venue
            };
            db.VenueRelationships.Add(venuerelationship);
            db.SaveChanges();
            return RedirectToAction("VenueView", "Venues", new { VenueId = venueId });
        }


    }
}
