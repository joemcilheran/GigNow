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
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public string DefaultArtistType { get; set; }
        public int? DefaultCompensation { get; set; }
        public string DefaultGenre { get; set; }
        public string DefaultPerks { get; set; }
        public string LoadInInsrtuctions { get; set; }
        [ForeignKey("Venue")]
        public int? VenueId { get; set; }
        public Venue Venue { get; set; }

    }
}