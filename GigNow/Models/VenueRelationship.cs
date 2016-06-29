using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class VenueRelationship
    {
        [Key]
        public int VenueRelationshipId { get; set; }
        [ForeignKey("Listener")]
        public int? ListenerId { get; set; }
        public Listener Listener { get; set; }
        [ForeignKey("Venue")]
        public int? VenueId { get; set; }
        public Venue Venue { get; set; }

    }
}