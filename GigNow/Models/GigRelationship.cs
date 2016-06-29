using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class GigRelationship
    {
        [Key]
        public int GigRelationshipId { get; set; }
        [ForeignKey("Listener")]
        public int? ListenerId { get; set; }
        public Listener Listener { get; set; }
        [ForeignKey("Gig")]
        public int? GigId { get; set; }
        public Gig Gig { get; set; }
    }
}