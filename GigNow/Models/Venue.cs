using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigNow.Models
{ 
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        public string Name { get; set; }
        public virtual Address address { get; set; }
        public int? Capacity { get; set; }
        public int? StageSize { get; set; }
        public string ContactName { get; set; }
        public string DefaultArtistType { get; set; }
        public int? DefaultCompensation { get; set; }
        public string DefaultGenre { get; set; }
        public string DefaultPerks { get; set; }
        public string FBLink { get; set; }
        public string SiteLink { get; set; }
        public string TwitterLink { get; set; }
        public string ExtraLink { get; set; }
        public string ExtraLink2 { get; set; }
        public bool SoundSystem { get; set; }
        public string LoadInInstructions { get; set; }
        public int? rating { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }

    }
}