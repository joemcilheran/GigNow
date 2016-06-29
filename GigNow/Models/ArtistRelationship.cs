using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class ArtistRelationship
    {
        [Key]
        public int ArtistRelationshipId { get; set; }
        [ForeignKey("Listener")]
        public int? ListenerId { get; set; }
        public Listener Listener { get; set; }
        [ForeignKey("Artist")]
        public int? ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}