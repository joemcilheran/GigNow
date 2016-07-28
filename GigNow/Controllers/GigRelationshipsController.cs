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
    public class GigRelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: GigRelationships/Create
        public ActionResult Create(int? gigId)
        {
            var gig = db.Gigs.Find(gigId);
            var userId = User.Identity.GetUserId();
            var Listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
            GigRelationship gigrelationship = new GigRelationship
            {
                Listener = Listener,
                Gig = gig
            };
            db.GigRelationships.Add(gigrelationship);
            db.SaveChanges();
            return RedirectToAction("GigView", "Gigs", new { GigId = gig.GigId, partial = "false" });
        }


    }
}
