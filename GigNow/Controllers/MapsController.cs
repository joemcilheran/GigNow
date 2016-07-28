using GigNow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigNow.Controllers
{
    public class MapsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string generateGigMapUrl(List<Gig> gigList, int? artistId)
        {
            var artist = db.Artists.Find(artistId);
            var googleAddress = artist.address.StreetAddress.Replace(' ', '+');
            var googleCity = artist.address.zipcode.city.Name.Replace(' ', '+');
            var artistAddress = (googleAddress + "+" + artist.address.Apt + ",+" + googleCity + ",+" + artist.address.zipcode.city.state.Name + "+" + artist.address.zipcode.ZipCode);
            string gigMapUrl = ("https://maps.googleapis.com/maps/api/staticmap?size=400x400&maptype=roadmap&markers=color:blue%7Clabel:A%7C" + artistAddress + "&markers=color:red");
            foreach (Gig thisGig in gigList)
            {
                var googleVAddress = thisGig.Venue.address.StreetAddress.Replace(' ', '+');
                var googleVCity = thisGig.Venue.address.zipcode.city.Name.Replace(' ', '+');
                var Destination = (googleVAddress + "+" + thisGig.Venue.address.Apt + ",+" + googleVCity + ",+" + thisGig.Venue.address.zipcode.city.state.Name + "+" + thisGig.Venue.address.zipcode.ZipCode);
                gigMapUrl = (gigMapUrl + "%7C" + Destination);
            }
            gigMapUrl = (gigMapUrl + "&key=AIzaSyAqBwMAtQFkFgENx8Fn_0Stj3UOgIWysDc");
            return gigMapUrl;
        }
        public string generateDestination(Venue Venue)
        {
            var googleVAddress = Venue.address.StreetAddress.Replace(' ', '+');
            var googleVCity = Venue.address.zipcode.city.Name.Replace(' ', '+');
            var Destination = (googleVAddress + "+" + Venue.address.Apt + ",+" + googleVCity + ",+" + Venue.address.zipcode.city.state.Name + "+" + Venue.address.zipcode.ZipCode);
            return Destination;
        }
        public string generateOrigin(Artist artist)
        {
            var googleAddress = artist.address.StreetAddress.Replace(' ', '+');
            var googleCity = artist.address.zipcode.city.Name.Replace(' ', '+');
            var Origin = (googleAddress + "+" + artist.address.Apt + ",+" + googleCity + ",+" + artist.address.zipcode.city.state.Name + "+" + artist.address.zipcode.ZipCode);
            return Origin;
        }
        public string generateOrigin(Listener listener)
        {
            var googleAddress = listener.address.StreetAddress.Replace(' ', '+');
            var googleCity = listener.address.zipcode.city.Name.Replace(' ', '+');
            var Origin = (googleAddress + "+" + listener.address.Apt + ",+" + googleCity + ",+" + listener.address.zipcode.city.state.Name + "+" + listener.address.zipcode.ZipCode);
            return Origin;
        }
        public string generateMap(string Destination)
        {
            string Map = ("https://www.google.com/maps/embed/v1/place?key=AIzaSyAqBwMAtQFkFgENx8Fn_0Stj3UOgIWysDc&q=" + Destination);
            return Map;
        }
        public string generateRoute(string Origin, string Destination)
        {
            string Route = ("https://www.google.com/maps/embed/v1/directions?key=AIzaSyAqBwMAtQFkFgENx8Fn_0Stj3UOgIWysDc&origin=" + Origin + "&destination=" + Destination);
            return Route;
        }

    }
}