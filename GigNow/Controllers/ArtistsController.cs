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


        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }


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
                PhotosController PC = new PhotosController();
                PC.CreateArtistPhoto(upload, artistViewModelVM.artist);
                db.SaveChanges();
                return RedirectToAction("ArtistView", new { artistId = artistViewModelVM.artist.ArtistId, partial = "false" });
            }
            return View(artistViewModelVM);
        }

        //GET: Artists/Edit/5
        public ActionResult Edit(int? artistId)
        {
            var Artist = db.Artists.Find(artistId);
            var Address = db.Addresses.Find(Artist.address.AddressId);
            var Zipcode = db.Zipcodes.Find(Address.zipcode.ZipcodeId);
            var City = db.Cities.Find(Zipcode.city.CityId);
            var State = db.States.Find(City.state.StateId);
            ArtistViewModelVM AVM = new ArtistViewModelVM
            {

                photo = db.Photos.FirstOrDefault(x => x.Artist.ArtistId == Artist.ArtistId),
                artist = Artist,
                address = Address,
                zipcode = Zipcode,
                city = City,
                state = State,
            };
            return View(AVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArtistViewModelVM artistViewModelVM, HttpPostedFileBase upload)
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
                PhotosController PC = new PhotosController();
                PC.CreateArtistPhoto(upload, artistViewModelVM.artist);
                db.SaveChanges();
                return RedirectToAction("ArtistView", new { artistId = artistViewModelVM.artist.ArtistId, partial = "false" });
            }
            return View(artistViewModelVM);
        }



        public ActionResult ArtistView(int? ArtistId, string partial)
        {
            ViewBag.Partial = partial;
            var Artist = db.Artists.Find(ArtistId);
            var thisgigList = generateGigList(Artist.ArtistId);
            MapsController MP = new MapsController();
            ViewBag.MapUrl = MP.generateGigMapUrl(thisgigList, ArtistId);
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
                var artistsByGenre = db.Artists.Where(x => x.Genre1 == artistGenre || x.Genre2 == artistGenre || x.Genre3 == artistGenre);
                artistSearchResultList = artistsByGenre.Where(x => x.address.zipcode.city.Name == artistCity).ToList();
            }
            if (!string.IsNullOrWhiteSpace(artistGenre) && string.IsNullOrWhiteSpace(artistCity))
            {
                artistSearchResultList = db.Artists.Where(x => x.Genre1 == artistGenre || x.Genre2 == artistGenre || x.Genre3 == artistGenre).ToList();
            }
            if (string.IsNullOrWhiteSpace(artistGenre) && !string.IsNullOrWhiteSpace(artistCity))
            {
                artistSearchResultList = db.Artists.Where(x => x.address.zipcode.city.Name == artistCity).ToList();
            }
            return View(artistSearchResultList);
        }

        

    }
}
