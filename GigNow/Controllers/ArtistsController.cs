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
                artistViewModelVM.artist.address = artistViewModelVM.address;
                artistViewModelVM.artist.UserId = userId;
                artistViewModelVM.address.zipcode = artistViewModelVM.zipcode;
                artistViewModelVM.zipcode.city = artistViewModelVM.city;
                artistViewModelVM.city.state = artistViewModelVM.state;
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
                return RedirectToAction("ArtistView", new { artistId = artistViewModelVM.artist.ArtistId, partial = "false" });
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
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.address.AddressId);
           
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
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "StreetAddress", artist.address.AddressId);
           
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
        //public ActionResult AddAudioandVideo(Artist artist)
        //{
        //    return View(artist);
        //}
        //[HttpPost]
        //public ActionResult AddAudioandVideo(HttpPostedFileBase audio1, HttpPostedFileBase audio2, HttpPostedFileBase audio3, HttpPostedFileBase video)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var artist = db.Artists.FirstOrDefault(x => x.UserId == userId);
        //    var artistId = artist.ArtistId;
        //    saveAudio(audio1, artistId);
        //    saveAudio(audio2, artistId);
        //    saveAudio(audio3, artistId);
        //    saveVideo(video, artistId);
        //    return RedirectToAction("ArtistView");
        //}
        //public void saveAudio(HttpPostedFileBase audio, int artistId)
        //{
        //    var artist = db.Artists.Find(artistId);
        //    if (audio != null && audio.ContentLength > 0)
        //    {
        //        Track track = new Track
        //        {
        //            Name = System.IO.Path.GetFileName(audio.FileName),
        //            Artist = artist
        //        };
        //        using (var reader = new System.IO.BinaryReader(audio.InputStream))
        //        {
        //            track.Data = reader.ReadBytes(audio.ContentLength);
        //        }
        //        db.Tracks.Add(track);
        //        db.SaveChanges();
        //    }
        //}
        //public void saveVideo(HttpPostedFileBase upload, int artistId)
        //{
        //    var artist = db.Artists.Find(artistId);
        //    if (upload != null && upload.ContentLength > 0)
        //    {
        //        Video video = new Video
        //        {
        //            Name = System.IO.Path.GetFileName(upload.FileName),
        //            Artist = artist
        //        };
        //        using (var reader = new System.IO.BinaryReader(upload.InputStream))
        //        {
        //            video.Data = reader.ReadBytes(upload.ContentLength);
        //        }
        //        db.Videos.Add(video);
        //        db.SaveChanges();
        //    }
        //}
        public ActionResult ArtistView(int? ArtistId, string partial)
        {
            ViewBag.Partial = partial;
            var Artist = db.Artists.Find(ArtistId);
            var thisgigList = generateGigList(Artist.ArtistId);
            ViewBag.MapUrl = generateGigMapUrl(thisgigList, ArtistId);
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
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
                    var relationshipList = db.ArtistRelationships.Where(x => x.Listener.ListenerID == listener.ListenerID && x.Artist.ArtistId == ArtistId).ToList();
                    if (relationshipList.Count == 0)
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
            }
            else
            {
                ViewBag.User = "Visiter";
            }

            var Address = db.Addresses.Find(Artist.address.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.zipcode.ZipcodeId);
            var City = db.Cities.Find(Zipcode.city.CityId);
            var State = db.States.Find(City.state.StateId);
            ArtistViewModelVM AVM = new ArtistViewModelVM
            {
                gigList = thisgigList,
                photo = db.Photos.FirstOrDefault(x => x.Artist.ArtistId == Artist.ArtistId),
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
            var gigList = (from slot in db.Slots where slot.Artist.ArtistId == artistId select slot.Gig);
            var currentGigList = gigList.Where(x => x.Date >= DateTime.Today).ToList();
            return currentGigList;
        }
        public string generateGigMapUrl(List<Gig> gigList, int? artistId)
        {
            var artist = db.Artists.Find(artistId);
            var googleAddress = artist.address.StreetAddress.Replace(' ', '+');
            var googleCity = artist.address.zipcode.city.Name.Replace(' ', '+');
            var artistAddress = (googleAddress + "+" + artist.address.Apt + ",+" + googleCity + ",+" + artist.address.zipcode.city.state.Name + "+" + artist.address.zipcode.ZipCode);
            string gigMapUrl = ("https://maps.googleapis.com/maps/api/staticmap?size=400x400&maptype=roadmap&markers=color:blue%7Clabel:A%7C"+artistAddress+"&markers=color:red");
            foreach(Gig thisGig in gigList)
            {
                var googleVAddress = thisGig.Venue.address.StreetAddress.Replace(' ', '+');
                var googleVCity = thisGig.Venue.address.zipcode.city.Name.Replace(' ', '+');
                var Destination = (googleVAddress + "+" + thisGig.Venue.address.Apt + ",+" + googleVCity + ",+" + thisGig.Venue.address.zipcode.city.state.Name + "+" + thisGig.Venue.address.zipcode.ZipCode);
                gigMapUrl = (gigMapUrl + "%7C" + Destination);
            }
            gigMapUrl = (gigMapUrl+ "&key=AIzaSyAqBwMAtQFkFgENx8Fn_0Stj3UOgIWysDc");
            return gigMapUrl;
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View(new List<Artist>());
        }
        [HttpPost]
        public ActionResult Search(string artistGenre, string artistCity)
        {
            List<Artist> artistSearchResultList = new List<Artist>();
            if(!string.IsNullOrWhiteSpace(artistGenre) && !string.IsNullOrWhiteSpace(artistCity))
            {
                var artistsByGenre = db.Artists.Where(x => x.Genre1 == artistGenre || x.Genre1 == artistGenre || x.Genre3 == artistGenre);
                artistSearchResultList = artistsByGenre.Where(x => x.address.zipcode.city.Name == artistCity).ToList();
            }
            if (!string.IsNullOrWhiteSpace(artistGenre) && string.IsNullOrWhiteSpace(artistCity))
            {
                artistSearchResultList = db.Artists.Where(x => x.Genre1 == artistGenre || x.Genre1 == artistGenre || x.Genre3 == artistGenre).ToList();
            }
            if (string.IsNullOrWhiteSpace(artistGenre) && !string.IsNullOrWhiteSpace(artistCity))
            {
                artistSearchResultList = db.Artists.Where(x => x.address.zipcode.city.Name == artistCity).ToList();
            }
            return View(artistSearchResultList);
        }

    }
}
