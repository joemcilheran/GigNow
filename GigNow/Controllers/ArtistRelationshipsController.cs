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
    public class ArtistRelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Create(int? artistId)
        {
            var artist = db.Artists.Find(artistId);
            var userId = User.Identity.GetUserId();
            var Listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
            ArtistRelationship artistRelationship = new ArtistRelationship
            {
                Artist = artist,
                Listener = Listener
            };
            db.ArtistRelationships.Add(artistRelationship);
            db.SaveChanges();
            return RedirectToAction("ArtistView", "Artists", new { ArtistId = artistId, partial = "false" });
        }



    }
}
