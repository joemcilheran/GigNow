using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class ArtistViewModelVM
    {
        public Artist artist { get; set; }
        public Photo photo { get; set; }
        public Address address { get; set; }
        public Zipcode zipcode { get; set; }
        public City city { get; set; }
        public State state { get; set; }
        public List<Gig> gigList { get; set; }
    }

}