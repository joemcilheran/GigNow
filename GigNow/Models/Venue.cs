using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Venue Name")]
        public string Name { get; set; }
        public virtual Address address { get; set; }
        public int? Capacity { get; set; }
        [DisplayName("Stage Size")]
        public int? StageSize { get; set; }
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }
        [DisplayName("Default Type of Act")]
        public string DefaultArtistType { get; set; }
        [DisplayName("Default Compensation")]
        public int? DefaultCompensation { get; set; }
        [DisplayName("Default Genre")]
        public string DefaultGenre { get; set; }
        [DisplayName("Default Perks")]
        public string DefaultPerks { get; set; }
        [DisplayName("Facebook Link")]
        public string FBLink { get; set; }
        [DisplayName("Site Link")]
        public string SiteLink { get; set; }
        [DisplayName("Twitter Link")]
        public string TwitterLink { get; set; }
        public string ExtraLink { get; set; }
        public string ExtraLink2 { get; set; }
        [DisplayName("Sound System")]
        public bool SoundSystem { get; set; }
        [DisplayName("Load-in Instructions")]
        public string LoadInInstructions { get; set; }
        public int? rating { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }

    }
}