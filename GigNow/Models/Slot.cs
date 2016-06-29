using System;
using System.Collections.Generic;
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
        public int Compensation { get; set; }
        public string Genre { get; set; }
        public bool IsFilled { get; set; }
        public string ArtistType { get; set; }
        public string Perks { get; set; }
        public string Order { get; set; }
        public int Length { get; set; }
        public bool UseGigDefaults { get; set; }
        [ForeignKey("Gig")]
        public int GigId { get; set; }
        public Gig Gig { get; set; }
    }
}