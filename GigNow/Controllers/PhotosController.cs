using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GigNow.Models;

namespace GigNow.Controllers
{
    public class PhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ShowPhoto(int PhotoId)
        {
            var photo = db.Photos.Find(PhotoId);
            return File(photo.Data, "image/jpg");
        }
        public void CreateArtistPhoto(HttpPostedFileBase upload, Artist artist)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var photo = new Photo
                {
                    Name = System.IO.Path.GetFileName(upload.FileName),
                    Artist = artist
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    photo.Data = reader.ReadBytes(upload.ContentLength);
                }
                db.Photos.Add(photo);
                db.SaveChanges();


            }
        }
        public void CreateVenuePhoto(HttpPostedFileBase upload, Venue venue)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var photo = new Photo
                {
                    Name = System.IO.Path.GetFileName(upload.FileName),
                    Venue = venue
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    photo.Data = reader.ReadBytes(upload.ContentLength);
                }
                db.Photos.Add(photo);
                db.SaveChanges();

            }
        }
    }
}
