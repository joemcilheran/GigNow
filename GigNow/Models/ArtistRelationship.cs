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
        public virtual Listener Listener { get; set; }
        public virtual Artist Artist { get; set; }
    }
}