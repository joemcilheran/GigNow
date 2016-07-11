using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{
    public class Slot
    {
        [Key]
        public int SlotId { get; set; }
        public int? Compensation { get; set; }
        public string Genre { get; set; }
        [DisplayName("Type of Act")]
        public string ArtistType { get; set; }
        public string Perks { get; set; }
        public string Order { get; set; }
        public int? Length { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Gig Gig { get; set; }
    }
}