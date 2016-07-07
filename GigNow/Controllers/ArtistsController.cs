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
using Microsoft.AspNet.Identity.Owin;

namespace GigNow.Controllers
{
    public class ArtistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
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

        // GET: Artists/Create
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
                return RedirectToAction("AddAudioandVideo", new { artist = artistViewModelVM.artist });
            }
            return View(artistViewModelVM);
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
        public ActionResult AddAudioandVideo(Artist artist)
        {
            return View(artist);
        }
        [HttpPost]
        public ActionResult AddAudioandVideo(HttpPostedFileBase audio1, HttpPostedFileBase audio2, HttpPostedFileBase audio3, HttpPostedFileBase video)
        {
            var userId = User.Identity.GetUserId();
            var artist = db.Artists.FirstOrDefault(x => x.UserId == userId);
            var artistId = artist.ArtistId;
            saveAudio(audio1, artistId);
            saveAudio(audio2, artistId);
            saveAudio(audio3, artistId);
            saveVideo(video, artistId);
            return RedirectToAction("ArtistView");
        }
        public void saveAudio(HttpPostedFileBase audio, int artistId)
        {
            if (audio != null && audio.ContentLength > 0)
            {
                Track track = new Track
                {
                    Name = System.IO.Path.GetFileName(audio.FileName),
                    ArtistId = artistId
                };
                using (var reader = new System.IO.BinaryReader(audio.InputStream))
                {
                    track.Data = reader.ReadBytes(audio.ContentLength);
                }
                db.Tracks.Add(track);
                db.SaveChanges();
            }
        }
        public void saveVideo(HttpPostedFileBase upload, int artistId)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                Video video = new Video
                {
                    Name = System.IO.Path.GetFileName(upload.FileName),
                    AtistId = artistId
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    video.Data = reader.ReadBytes(upload.ContentLength);
                }
                db.Videos.Add(video);
                db.SaveChanges();
            }
        }
        public ActionResult ArtistView(int? ArtistId)
        {
            var userId = User.Identity.GetUserId();
            var Artist = db.Artists.Find(ArtistId);
            var s = UserManager.GetRoles(userId);
            string role = s[0].ToString();
            if (userId == Artist.UserId)
            {
                ViewBag.User = "Artist Manager";
            }
            else if (role == "Listener")
            {
                ViewBag.User = "Listener";
                var listener = db.Listeners.FirstOrDefault(x => x.UserId == userId);
                var relationshipList = db.ArtistRelationships.Where(x => x.ListenerId == listener.ListenerID).ToList(); 
                if(relationshipList.Count == 0)
                {
                    ViewBag.Watched = "false";
                }
                else
                {
                    ViewBag.Watched = "true";
                }
            }
            else
            {
                ViewBag.User = "Other";
            }
            var Address = db.Addresses.Find(Artist.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.ZipCodeId);
            var City = db.Cities.Find(Zipcode.CityId);
            var State = db.States.Find(City.StateId);
            ArtistViewModelVM AVM = new ArtistViewModelVM
            {
                gigList = generateGigList(Artist.ArtistId),
                photo = db.Photos.FirstOrDefault(x => x.VenueId == Artist.ArtistId),
                artist = Artist,
                address = Address,
                zipcode = Zipcode,
                city = City,
                state = State,
            };
            return View(AVM);
        }
        public ActionResult ShowWatchList(List<Artist> watchList)
        {
            return View(watchList);
        }
        public List<Gig> generateGigList(int? artistId)
        {
            var gigIds = from slot in db.Slots where slot.ArtistId == artistId select slot.GigId;
            var gigList = db.Gigs.Where(x => gigIds.ToList().Contains(x.GigId)).ToList();
            return gigList;
        }

    }
}
