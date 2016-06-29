using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Gig
    {
        [Key]
        public int GigId { get; set; }
        public int Cover { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string DefaultGenre { get; set; }
        public int? DefaulCompensation { get; set; }
        public string Name { get; set; }
        public bool UseVenueDefaults { get; set; }
        [ForeignKey("Venue")]
        public int? VenueId { get; set; }
        public Venue Venue { get; set; }

    }
}