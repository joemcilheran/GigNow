using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class ListenerViewModelVM
    {

        public Listener listener { get; set; }
        public Address address { get; set; }
        public Zipcode zipcode { get; set; }
        public City city { get; set; }
        public State state { get; set; }
        public List<Gig> GigWatchList { get; set; }
        public List<Venue> VenueWatchList { get; set; }
        public List<Artist> ArtistWatchList { get; set; }
    }
}