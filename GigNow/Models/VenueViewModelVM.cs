using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class VenueViewModelVM
    {
        public int Id { get; set; }
        public Venue venue { get; set; }
        public Photo photo { get; set; }
        public Address address { get; set; }
        public Zipcode zipcode { get; set; }
        public City city { get; set; }
        public State state { get; set; }
    }
}